using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiDistanceConstraintsBatch : ObiConstraintsBatch, IStructuralConstraintBatch
    {
        [NonSerialized] protected ObiDistanceConstraintsData m_Constraints; // TODO: not good to get a ref to it, cycle in Unity serializer.
        protected IDistanceConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeFloatList restLengths = new ObiNativeFloatList();                  /**< Rest distance for each constraint.*/
        [HideInInspector] public ObiNativeVector2List stiffnesses = new ObiNativeVector2List();              /**< 2 values for each constraint: compliance and slack.*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.Distance; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiDistanceConstraintsBatch(ObiDistanceConstraintsData constraints = null, ObiDistanceConstraintsBatch source = null):base(source)
        {
            m_Constraints = constraints;
        }

        public void AddConstraint(Vector2Int indices, float restLength)
        {
            RegisterConstraint();

            particleIndices.Add(indices[0]);
            particleIndices.Add(indices[1]);
            restLengths.Add(restLength);
            stiffnesses.Add(Vector2.zero);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            restLengths.Clear();
            stiffnesses.Clear();
        }

        public float GetRestLength(int index)
        {
            return restLengths[index];
        }

        public void SetRestLength(int index, float restLength)
        {
            restLengths[index] = restLength;
        }

        public ParticlePair GetParticleIndices(int index)
        {
            return new ParticlePair(particleIndices[index * 2],particleIndices[index * 2 + 1]);
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index * 2]);
            particles.Add(particleIndices[index * 2 + 1]);
        }

        protected override void CopyConstraint(ObiConstraintsBatch batch, int constraintIndex)
        {
            if (batch is ObiDistanceConstraintsBatch)
            {
                var db = batch as ObiDistanceConstraintsBatch;
                RegisterConstraint();
                particleIndices.Add(batch.particleIndices[constraintIndex * 2]);
                particleIndices.Add(batch.particleIndices[constraintIndex * 2 + 1]);
                restLengths.Add(db.restLengths[constraintIndex]);
                stiffnesses.Add(db.stiffnesses[constraintIndex]);
                ActivateConstraint(constraintCount - 1);
            }
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex * 2, destIndex * 2);
            particleIndices.Swap(sourceIndex * 2 + 1, destIndex * 2 + 1);
            restLengths.Swap(sourceIndex, destIndex);
            stiffnesses.Swap(sourceIndex, destIndex);
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiDistanceConstraintsBatch(constraints as ObiDistanceConstraintsData,this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.restLengths.ResizeUninitialized(restLengths.count);
            clone.stiffnesses.ResizeUninitialized(stiffnesses.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.restLengths.CopyFrom(restLengths);
            clone.stiffnesses.CopyFrom(stiffnesses);

            return clone;
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
                for (int i = 0; i < restLengths.count; i++)
                {
                    particleIndices[i * 2] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2]];
                    particleIndices[i * 2 + 1] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2 + 1]];
                    stiffnesses[i] = new Vector2(0, restLengths[i]);
                    lambdas.Add(0);
                }

                m_BatchImpl.SetDistanceConstraints(particleIndices, restLengths, stiffnesses, lambdas, m_ConstraintCount);
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

        public void SetParameters(float compliance, float slack, float stretchingScale)
        {
            for (int i = 0; i < stiffnesses.count; i++)
            {
                restLengths[i] = ((ObiDistanceConstraintsBatch)m_Source).restLengths[i] * stretchingScale;
                stiffnesses[i] = new Vector2(compliance, slack * restLengths[i]);
            }
        }

    }
}
