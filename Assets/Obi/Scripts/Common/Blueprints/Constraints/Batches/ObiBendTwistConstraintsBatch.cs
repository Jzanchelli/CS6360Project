using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiBendTwistConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiBendTwistConstraintsData m_Constraints;
        protected IBendTwistConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeQuaternionList restDarbouxVectors = new ObiNativeQuaternionList();                /**< Rest darboux vector for each constraint.*/
        [HideInInspector] public ObiNativeVector3List stiffnesses = new ObiNativeVector3List();                             /**< 3 compliance values for each constraint, one for each local axis (x,y,z).*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.BendTwist; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiBendTwistConstraintsBatch(ObiBendTwistConstraintsData constraints = null, ObiBendTwistConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiBendTwistConstraintsBatch(constraints as ObiBendTwistConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.restDarbouxVectors.ResizeUninitialized(restDarbouxVectors.count);
            clone.stiffnesses.ResizeUninitialized(stiffnesses.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.restDarbouxVectors.CopyFrom(restDarbouxVectors);
            clone.stiffnesses.CopyFrom(stiffnesses);

            return clone;
        }

        public void AddConstraint(Vector2Int indices, Quaternion restDarboux)
        {
            RegisterConstraint();

            particleIndices.Add(indices[0]);
            particleIndices.Add(indices[1]);
            restDarbouxVectors.Add(restDarboux);
            stiffnesses.Add(Vector3.zero);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            restDarbouxVectors.Clear();
            stiffnesses.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index * 2]);
            particles.Add(particleIndices[index * 2 + 1]);
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex * 3, destIndex * 3);
            particleIndices.Swap(sourceIndex * 3 + 1, destIndex * 3 + 1);
            particleIndices.Swap(sourceIndex * 3 + 2, destIndex * 3 + 2);
            restDarbouxVectors.Swap(sourceIndex, destIndex);
            stiffnesses.Swap(sourceIndex, destIndex);
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
                for (int i = 0; i < restDarbouxVectors.count; i++)
                {
                    particleIndices[i * 2] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2]];
                    particleIndices[i * 2 + 1] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2 + 1]];
                    lambdas.Add(0);
                    lambdas.Add(0);
                    lambdas.Add(0);
                }

                m_BatchImpl.SetBendTwistConstraints(particleIndices, restDarbouxVectors, stiffnesses, lambdas, m_ConstraintCount);
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

        public void SetParameters(float torsionCompliance, float bend1Compliance, float bend2Compliance)
        {
            for (int i = 0; i < stiffnesses.count; i++)
            {
                stiffnesses[i] = new Vector3(torsionCompliance, bend1Compliance, bend2Compliance);
            }
        }
    }
}
