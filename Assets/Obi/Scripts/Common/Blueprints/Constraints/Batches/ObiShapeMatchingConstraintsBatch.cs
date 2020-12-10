using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiShapeMatchingConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiShapeMatchingConstraintsData m_Constraints;
        protected IShapeMatchingConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        public ObiNativeIntList firstIndex = new ObiNativeIntList();             /**< index of the first particle in each constraint.*/
        public ObiNativeIntList numIndices = new ObiNativeIntList();             /**< amount of particles in each constraint.*/
        public ObiNativeIntList explicitGroup = new ObiNativeIntList();          /**< whether the constraint is implicit (0) or explicit (>0).*/
        public ObiNativeFloatList materialParameters = new ObiNativeFloatList(); /**< 5 floats per constraint: stiffness, plastic yield, creep, recovery and max deformation.*/

        public ObiNativeVector4List restComs = new ObiNativeVector4List();       /**< rest center of mass for each constraint.*/
        public ObiNativeVector4List coms = new ObiNativeVector4List();           /**< current center of mass for each constraint.*/
        public ObiNativeQuaternionList orientations = new ObiNativeQuaternionList();    /**< current best-match orientation for each constraint.*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.ShapeMatching; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiShapeMatchingConstraintsBatch(ObiShapeMatchingConstraintsData constraints = null, ObiShapeMatchingConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiShapeMatchingConstraintsBatch(constraints as ObiShapeMatchingConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.firstIndex.ResizeUninitialized(firstIndex.count);
            clone.numIndices.ResizeUninitialized(numIndices.count);
            clone.explicitGroup.ResizeUninitialized(explicitGroup.count);
            clone.materialParameters.ResizeUninitialized(materialParameters.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.firstIndex.CopyFrom(firstIndex);
            clone.numIndices.CopyFrom(numIndices);
            clone.explicitGroup.CopyFrom(explicitGroup);
            clone.materialParameters.CopyFrom(materialParameters);

            clone.restComs.ResizeUninitialized(constraintCount);
            clone.coms.ResizeUninitialized(constraintCount);
            clone.orientations.ResizeUninitialized(constraintCount);

            return clone;
        }

        public void AddConstraint(int[] indices, bool isExplicit)
        {
            RegisterConstraint();

            firstIndex.Add((int)particleIndices.count);
            numIndices.Add((int)indices.Length);
            explicitGroup.Add(isExplicit ? 1 : 0);
            particleIndices.AddRange(indices);
            materialParameters.AddRange(new float[] { 1, 1, 1, 1, 1 });
        }

        public override void Clear()
        {
            base.Clear();
            firstIndex.Clear();
            numIndices.Clear();
            explicitGroup.Clear();
            particleIndices.Clear();
            materialParameters.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            int first = firstIndex[index];
            int num = numIndices[index];
            for (int i = first; i < first + num; ++i) 
                particles.Add(particleIndices[i]);
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            firstIndex.Swap(sourceIndex, destIndex);
            numIndices.Swap(sourceIndex, destIndex);
            explicitGroup.Swap(sourceIndex, destIndex);

            for (int i = 0; i < 5; ++i)
                materialParameters.Swap(sourceIndex * 5 + i, destIndex * 5 + i);

            restComs.Swap(sourceIndex, destIndex);
            coms.Swap(sourceIndex, destIndex);
            orientations.Swap(sourceIndex, destIndex);
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

                for (int i = 0; i < particleIndices.count; i++)
                    particleIndices[i] = constraints.GetActor().solverIndices[m_Source.particleIndices[i]];

                for (int i = 0; i < orientations.count; i++)
                    orientations[i] = constraints.GetActor().actorLocalToSolverMatrix.rotation;

                for (int i = 0; i < firstIndex.count; i++)
                    lambdas.Add(0);

                m_BatchImpl.SetShapeMatchingConstraints(particleIndices, firstIndex, numIndices, explicitGroup,
                                                        materialParameters, restComs, coms, orientations, lambdas, m_ConstraintCount);
                m_BatchImpl.SetActiveConstraints(m_ActiveConstraintCount);

                m_BatchImpl.CalculateRestShapeMatching();
            }

            /*for (int i = 0; i < particleIndices.count; i++)
            {
                particleIndices[i] = constraints.GetActor().solverIndices[m_Source.particleIndices[i]];
            }

            for (int i = 0; i < orientations.count; i++)
                orientations[i] = constraints.GetActor().actorLocalToSolverMatrix.rotation;*/

            // pass constraint data arrays to the solver:
            //Oni.SetShapeMatchingConstraints(batch, particleIndices.GetIntPtr(), firstIndex.GetIntPtr(), numIndices.GetIntPtr(), explicitGroup.GetIntPtr(),
            //materialParameters.GetIntPtr(), restComs.GetIntPtr(), coms.GetIntPtr(), orientations.GetIntPtr(), m_ConstraintCount);
            //Oni.SetActiveConstraints(batch, m_ActiveConstraintCount);

            //Oni.CalculateRestShapeMatching(constraints.GetActor().solver.OniSolver, batch);
        }

        public void RecalculateRestShapeMatching()
        {
            if (m_BatchImpl != null)
                m_BatchImpl.CalculateRestShapeMatching();
        }

        public override void RemoveFromSolver()
        {
            if(m_Constraints != null && m_Constraints.implementation != null)
                m_Constraints.implementation.RemoveBatch(m_BatchImpl);

            if (m_BatchImpl != null)
                m_BatchImpl.Destroy();
        }

        public void SetParameters(float stiffness, float yield, float creep, float recovery, float maxDeformation)
        {
            for (int i = 0; i < explicitGroup.count; i++)
            {
                materialParameters[i * 5] = stiffness;
                materialParameters[i * 5 + 1] = yield;
                materialParameters[i * 5 + 2] = creep;
                materialParameters[i * 5 + 3] = recovery;
                materialParameters[i * 5 + 4] = maxDeformation;
            }
        }

    }
}
