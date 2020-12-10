using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiChainConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiChainConstraintsData m_Constraints;
        protected IChainConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeIntList firstParticle = new ObiNativeIntList();           /**< index of the first particle for each constraint.*/
        [HideInInspector] public ObiNativeIntList numParticles = new ObiNativeIntList();            /**< number of particles for each constraint.*/
        [HideInInspector] public ObiNativeVector2List lengths = new ObiNativeVector2List();         /**< min/max lenghts for each constraint.*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.Chain; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiChainConstraintsBatch(ObiChainConstraintsData constraints = null, ObiChainConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiChainConstraintsBatch(constraints as ObiChainConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.firstParticle.ResizeUninitialized(firstParticle.count);
            clone.numParticles.ResizeUninitialized(numParticles.count);
            clone.lengths.ResizeUninitialized(lengths.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.firstParticle.CopyFrom(firstParticle);
            clone.numParticles.CopyFrom(numParticles);
            clone.lengths.CopyFrom(lengths);

            return clone;
        }

        public void AddConstraint(int[] indices, float restLength, float stretchStiffness, float compressionStiffness)
        {
            RegisterConstraint();

            firstParticle.Add((int)particleIndices.count);
            numParticles.Add((int)indices.Length);
            particleIndices.AddRange(indices);
            lengths.Add(new Vector2(restLength, restLength));
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            firstParticle.Clear();
            numParticles.Clear();
            lengths.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            //TODO.
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            firstParticle.Swap(sourceIndex, destIndex);
            numParticles.Swap(sourceIndex, destIndex);
            lengths.Swap(sourceIndex, destIndex);
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

                m_BatchImpl.SetChainConstraints(particleIndices, lengths, firstParticle, numParticles, m_ConstraintCount);
                m_BatchImpl.SetActiveConstraints(m_ActiveConstraintCount);
            }

            /*for (int i = 0; i < particleIndices.count; i++)
                particleIndices[i] = constraints.GetActor().solverIndices[m_Source.particleIndices[i]];*/

            // pass constraint data arrays to the solver:
            //Oni.SetChainConstraints(batch, particleIndices.GetIntPtr(), lengths.GetIntPtr(), firstParticle.GetIntPtr(), numParticles.GetIntPtr(), m_ConstraintCount);
            //Oni.SetActiveConstraints(batch, m_ActiveConstraintCount);
        }

        public override void RemoveFromSolver()
        {
            if (m_Constraints != null && m_Constraints.implementation != null)
                m_Constraints.implementation.RemoveBatch(m_BatchImpl);

            if (m_BatchImpl != null)
                m_BatchImpl.Destroy();
        }

        public void SetParameters(float tightness)
        {
            for (int i = 0; i < constraintCount; i++)
                lengths[i] = new Vector2(lengths[i].y * tightness, lengths[i].y);
        }
    }
}
