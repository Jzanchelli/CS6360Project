using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiBendConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiBendConstraintsData m_Constraints;
        protected IBendConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeFloatList restBends = new ObiNativeFloatList();                 /**< one float per constraint: the rest bend distance.*/
        [HideInInspector] public ObiNativeVector2List bendingStiffnesses = new ObiNativeVector2List();    /**< two floats per constraint: max bending and compliance.*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.Bending; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiBendConstraintsBatch(ObiBendConstraintsData constraints = null, ObiBendConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiBendConstraintsBatch(constraints as ObiBendConstraintsData,this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.restBends.ResizeUninitialized(restBends.count);
            clone.bendingStiffnesses.ResizeUninitialized(bendingStiffnesses.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.restBends.CopyFrom(restBends);
            clone.bendingStiffnesses.CopyFrom(bendingStiffnesses);

            return clone;
        }

        public void AddConstraint(Vector3Int indices, float restBend)
        {
            RegisterConstraint();

            particleIndices.Add(indices[0]);
            particleIndices.Add(indices[1]);
            particleIndices.Add(indices[2]);
            restBends.Add(restBend);
            bendingStiffnesses.Add(Vector2.zero);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            restBends.Clear();
            bendingStiffnesses.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index * 3]);
            particles.Add(particleIndices[index * 3 + 1]);
            particles.Add(particleIndices[index * 3 + 2]);
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex * 3, destIndex * 3);
            particleIndices.Swap(sourceIndex * 3 + 1 , destIndex * 3 + 1);
            particleIndices.Swap(sourceIndex * 3 + 2, destIndex * 3 + 2);
            restBends.Swap(sourceIndex, destIndex);
            bendingStiffnesses.Swap(sourceIndex, destIndex);
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
                for (int i = 0; i < restBends.count; i++)
                {
                    particleIndices[i * 3] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 3]];
                    particleIndices[i * 3 + 1] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 3 + 1]];
                    particleIndices[i * 3 + 2] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 3 + 2]];
                    lambdas.Add(0);
                }

                // pass constraint data arrays to the solver:
                m_BatchImpl.SetBendConstraints(particleIndices, restBends, bendingStiffnesses, lambdas, m_ConstraintCount);
                m_BatchImpl.SetActiveConstraints(m_ActiveConstraintCount);
            }
        }

        public override void RemoveFromSolver()
        {
            if (m_Constraints != null && m_Constraints.implementation != null)
                m_Constraints.implementation.RemoveBatch(m_BatchImpl);

            if (m_BatchImpl != null)
                m_BatchImpl.Destroy();
        }

        public void SetParameters(float compliance, float maxBending)
        {
            for (int i = 0; i < bendingStiffnesses.count; i++)
                bendingStiffnesses[i] = new Vector2(maxBending, compliance);
        }
    }
}
