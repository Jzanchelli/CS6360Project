using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiSkinConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiSkinConstraintsData m_Constraints;
        protected ISkinConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeVector4List skinPoints = new ObiNativeVector4List();                /**< skin constraint anchor points, in solver space.*/
        [HideInInspector] public ObiNativeVector4List skinNormals = new ObiNativeVector4List();               /**< normal vector for each skin constraint, in solver space.*/
        [HideInInspector] public ObiNativeFloatList skinRadiiBackstop = new ObiNativeFloatList();             /**< 3 floats per constraint: skin radius, backstop sphere radius, and backstop sphere distance.*/
        [HideInInspector] public ObiNativeFloatList skinCompliance = new ObiNativeFloatList();                /**< one compliance value per skin constraint.*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.Skin; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiSkinConstraintsBatch(ObiSkinConstraintsData constraints = null, ObiSkinConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiSkinConstraintsBatch(constraints as ObiSkinConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.skinPoints.ResizeUninitialized(skinPoints.count);
            clone.skinNormals.ResizeUninitialized(skinNormals.count);
            clone.skinRadiiBackstop.ResizeUninitialized(skinRadiiBackstop.count);
            clone.skinCompliance.ResizeUninitialized(skinCompliance.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.skinPoints.CopyFrom(skinPoints);
            clone.skinNormals.CopyFrom(skinNormals);
            clone.skinRadiiBackstop.CopyFrom(skinRadiiBackstop);
            clone.skinCompliance.CopyFrom(skinCompliance);

            return clone;
        }

        public void AddConstraint(int index, Vector4 point, Vector4 normal, float radius, float collisionRadius, float backstop, float stiffness)
        {
            RegisterConstraint();

            particleIndices.Add(index);
            skinPoints.Add(point);
            skinNormals.Add(normal);
            skinRadiiBackstop.Add(radius);
            skinRadiiBackstop.Add(collisionRadius);
            skinRadiiBackstop.Add(backstop);
            skinCompliance.Add(stiffness);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            skinPoints.Clear();
            skinNormals.Clear();
            skinRadiiBackstop.Clear();
            skinCompliance.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index]);
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex, destIndex);
            skinPoints.Swap(sourceIndex, destIndex);
            skinNormals.Swap(sourceIndex, destIndex);
            skinRadiiBackstop.Swap(sourceIndex * 3, destIndex * 3);
            skinRadiiBackstop.Swap(sourceIndex * 3+1, destIndex * 3+1);
            skinRadiiBackstop.Swap(sourceIndex * 3+2, destIndex * 3+2);
            skinCompliance.Swap(sourceIndex, destIndex);
        }

        public override void AddToSolver()
        {
            // create and add the implementation:
            if (m_Constraints != null && m_Constraints.implementation != null)
            {
                m_BatchImpl = m_Constraints.implementation.CreateConstraintsBatch();
            }

            if (m_BatchImpl != null)
            {
                lambdas.Clear();
                for (int i = 0; i < skinCompliance.count; i++)
                {
                    particleIndices[i] = constraints.GetActor().solverIndices[m_Source.particleIndices[i]];
                    lambdas.Add(0);
                }

                m_BatchImpl.SetSkinConstraints(particleIndices, skinPoints, skinNormals, skinRadiiBackstop, skinCompliance, lambdas, m_ConstraintCount);
                m_BatchImpl.SetActiveConstraints(m_ActiveConstraintCount);
            }

            /*for (int i = 0; i < skinCompliance.count; i++)
            {
                particleIndices[i] = constraints.GetActor().solverIndices[m_Source.particleIndices[i]];
            }*/

            // pass constraint data arrays to the solver:
            //Oni.SetSkinConstraints(batch, particleIndices.GetIntPtr(), skinPoints.GetIntPtr(), skinNormals.GetIntPtr(), skinRadiiBackstop.GetIntPtr(), skinCompliance.GetIntPtr(), m_ConstraintCount);
            //Oni.SetActiveConstraints(batch, m_ActiveConstraintCount);
        }

        public override void RemoveFromSolver()
        {
            if (m_Constraints != null && m_Constraints.implementation != null)
                m_Constraints.implementation.RemoveBatch(m_BatchImpl);

            if (m_BatchImpl != null)
                m_BatchImpl.Destroy();
        }
    }
}
