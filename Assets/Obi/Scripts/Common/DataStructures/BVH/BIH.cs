using System;
using System.Collections.Generic;
using UnityEngine;

namespace Obi
{
    public class BIH
    {
    
        public static List<BIHNode> Build(ref IBounded[] elements)
        {
            List<BIHNode> nodes = new List<BIHNode>{ new BIHNode(0, elements.Length) };

            var queue = new Queue<int>();
            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                // get current node:
                int index = queue.Dequeue();
                var node = nodes[index];

                // if this node contains enough elements, split it:
                if (node.count > 1)
                {
                    int start = node.start;
                    int end = start + (node.count - 1);

                    // calculate bounding box of all elements:
                    Aabb b = elements[start].GetBounds();
                    for (int k = start + 1; k <= end; ++k)
                        b.Encapsulate(elements[k].GetBounds());

                    // determine split axis (longest one):
                    Vector3 size = b.size;
                    int axis = node.axis = (size.x > size.y) ?
                                                (size.x > size.z ? 0 : 2) :
                                                (size.y > size.z ? 1 : 2);

                    // place split plane at half the longest axis:
                    float pivot = b.min[axis] + size[axis] * 0.5f;

                    // sort elements using the split plane (Hoare's partition algorithm):
                    int i = start - 1;
                    int j = end + 1;
                    Aabb bi, bj;
                    while (true)
                    {
                        // iterate over left elements, while they're smaller than the pivot.
                        do
                        {
                            bi = elements[++i].GetBounds();
                            if (bi.center[axis] < pivot)
                                node.min = Mathf.Max(node.min, bi.max[axis]);
                        } while (bi.center[axis] < pivot);

                        // iterate over right elements, while they're larger than the pivot.
                        do
                        {
                            bj = elements[--j].GetBounds();
                            if (bj.center[axis] > pivot)
                                node.max = Mathf.Min(node.max, bj.min[axis]);
                        } while (bj.center[axis] > pivot);

                        // if element i is larger than the pivot, j smaller than the pivot, swap them.
                        if (i < j)
                        {
                            ObiUtils.Swap(ref elements[i], ref elements[j]);
                            node.min = Mathf.Max(node.min, bj.max[axis]);
                            node.max = Mathf.Min(node.max, bi.min[axis]);
                        }
                        else break;
                    }

                    // create two child nodes:
                    var minChild = new BIHNode(start, j - start + 1);
                    var maxChild = new BIHNode(j + 1, end - j);

                    // guard against cases where all elements are on one side of the split plane,
                    // due to all having the same or very similar bounds as the entire group.
                    if (minChild.count > 0 && maxChild.count > 0)
                    {
                        node.firstChild = nodes.Count;
                        nodes[index] = node;

                        queue.Enqueue(nodes.Count);
                        queue.Enqueue(nodes.Count + 1);

                        // append child nodes to list:
                        nodes.Add(minChild);
                        nodes.Add(maxChild);
                    }
                }
            }

            return nodes;
        }

        public static float DistanceToSurface(Triangle[] triangles,
                                              Vector3[] vertices,
                                              Vector3[] normals,
                                              BIHNode node,
                                              Vector3 point)
        {
            float minDistance = float.MaxValue;
            int sign = 1;

            for (int i = node.start; i < node.start + node.count; ++i)
            {
                Triangle t = triangles[i];
                Vector3 pointOnTri = ObiUtils.NearestPointOnTri(vertices[t.i1],
                                                                vertices[t.i2],
                                                                vertices[t.i3],
                                                                point);

                Vector3 pointToTri = point - pointOnTri;
                float sqrDistance = pointToTri.sqrMagnitude;

                if (sqrDistance < minDistance)
                {
                    Vector3 bary = Vector3.zero;
                    ObiUtils.BarycentricCoordinates(vertices[t.i1], vertices[t.i2], vertices[t.i3], pointOnTri, ref bary);

                    Vector3 interpolatedNormal = ObiUtils.BarycentricInterpolation(normals[t.i1],
                                                                                   normals[t.i2],
                                                                                   normals[t.i3], bary);

                    sign = ObiUtils.PureSign(Vector3.Dot(pointToTri, interpolatedNormal));
                    minDistance = sqrDistance;
                }
            }

            return Mathf.Sqrt(minDistance) * sign;
        }

        public static float DistanceToSurface(List<BIHNode> nodes,
                                              Triangle[] triangles,
                                              Vector3[] vertices,
                                              Vector3[] normals,
                                              Vector3 point)
        {
            if (nodes.Count > 0)
                return DistanceToSurface(nodes, triangles, vertices, normals, nodes[0], point);

            return float.MaxValue;
        }

        public static float DistanceToSurface(List<BIHNode> nodes,
                                              Triangle[] triangles,
                                              Vector3[] vertices,
                                              Vector3[] normals,
                                              BIHNode node,
                                              Vector3 point)
        {

            float MinSignedDistance(float d1, float d2)
            {
                return (Mathf.Abs(d1) < Mathf.Abs(d2)) ? d1 : d2;
            }

            if (node.firstChild >= 0)
            {
                /**
                 * If the current node is not a leaf, figure out which side of the split plane that contains the query point, and recurse down that side.
                 * You will get the index and distance to the closest triangle in that subtree.
                 * Then, check if the distance to the nearest triangle is closer to the query point than the distance between the query point and the split plane.
                 * If it is closer, there is no need to recurse down the other side of the KD tree and you can just return.
                 * Otherwise, you will need to recurse down the other way too, and return whichever result is closer.
                 */

                float si = float.MaxValue;
                float p = point[node.axis];

                // child nodes overlap:
                if (node.min > node.max)
                {
                    // CASE 1: we are in the overlapping zone: recurse down both.
                    if (p <= node.min && p >= node.max)
                    {
                        si = MinSignedDistance(DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild], point),
                                               DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild + 1], point));
                    }
                    // CASE 2: to the right of left pivot, that is: in the right child only.
                    else if (p > node.min)
                    {
                        si = DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild + 1], point);

                        // only recurse down left child if nearest surface in right child is furthest than left pivot.
                        if (Mathf.Abs(si) > Mathf.Abs(p - node.min))
                            si = MinSignedDistance(si, DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild], point));
                    }
                    // CASE 3: to the left of right pivot, that is: in the left child only.
                    else
                    {
                        si = DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild], point);

                        // only recurse down left child if nearest surface in right child is furthest than left pivot.
                        if (Mathf.Abs(si) > Mathf.Abs(node.max - p))
                            si = MinSignedDistance(si, DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild + 1], point));
                    }
                }
                // child nodes do not overlap
                else
                {
                    // CASE 4: we are in the middle. just pick up one child (I chose right), get minimum, and if the other child pivot is nearer, recurse down it too.
                    // Just like case 2.
                    if (p > node.min && p < node.max)
                    {
                        si = DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild + 1], point);

                        // only recurse down left child if nearest surface in right child is furthest than left pivot.
                        if (Mathf.Abs(si) > Mathf.Abs(p - node.min))
                            si = MinSignedDistance(si, DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild], point));
                    }
                    // CASE 5: in the left child. Just like case 3.
                    else if (p <= node.min)
                    {
                        si = DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild], point);

                        // only recurse down left child if nearest surface in right child is furthest than left pivot.
                        if (Mathf.Abs(si) > Mathf.Abs(node.max - p))
                            si = MinSignedDistance(si, DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild + 1], point));
                    }
                    // CASE 6: in the right child. Just like case 2
                    else if (p >= node.max)
                    {
                        si = DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild + 1], point);

                        // only recurse down left child if nearest surface in right child is furthest than left pivot.
                        if (Mathf.Abs(si) > Mathf.Abs(p - node.min))
                            si = MinSignedDistance(si, DistanceToSurface(nodes, triangles, vertices, normals, nodes[node.firstChild], point));
                    }
                }

                return si;
            }
            else
                return DistanceToSurface(triangles, vertices, normals, node, point);
        }

    }
}
