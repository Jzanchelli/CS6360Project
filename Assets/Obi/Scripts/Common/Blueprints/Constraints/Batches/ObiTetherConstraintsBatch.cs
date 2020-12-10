using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiTetherConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiTetherConstraintsData m_Constraints;
        protected ITetherConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeVector2List maxLengthsScales = new ObiNativeVector2List();     /**< 2 floats per constraint: maximum length and tether scale.*/
        [HideInInspector] public ObiNativeFloatList stiffnesses = new ObiNativeFloatList();              /**< compliance value for each constraint. */

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.Tether; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiTetherConstraintsBatch(ObiTetherConstraintsData constraints = null, ObiTetherConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiTetherConstraintsBatch(constraints as ObiTetherConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.maxLengthsScales.ResizeUninitialized(maxLengthsScales.count);
            clone.stiffnesses.ResizeUninitialized(stiffnesses.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.maxLengthsScales.CopyFrom(maxLengthsScales);
            clone.stiffnesses.CopyFrom(stiffnesses);

            return clone;
        }

        public void AddConstraint(Vector2Int indices, float maxLength, float scale)
        {
            RegisterConstraint();

            particleIndices.Add(indices[0]);
            particleIndices.Add(indices[1]);
            maxLengthsScales.Add(new Vector2(maxLength, scale));
            stiffnesses.Add(0);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            maxLengthsScales.Clear();
            stiffnesses.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index * 2]);
            particles.Add(particleIndices[index * 2 + 1]);
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex * 2, destIndex * 2);
            particleIndices.Swap(sourceIndex * 2 + 1, destIndex * 2 + 1);
            maxLengthsScales.Swap(sourceIndex, destIndex);
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
                for (int i = 0; i < stiffnesses.count; i++)
                {
                    particleIndices[i * 2] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2]];
                    particleIndices[i * 2 + 1] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2 + 1]];
                    lambdas.Add(0);
                }

                m_BatchImpl.SetTetherConstraints(particleIndices, maxLengthsScales, stiffnesses, lambdas, m_ConstraintCount);
                m_BatchImpl.SetActiveConstraints(m_ActiveConstraintCount);
            }

            /*for (int i = 0; i < stiffnesses.count; i++)
            {
                particleIndices[i * 2] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2]];
                particleIndices[i * 2 + 1] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2 + 1]];
            }*/

            // pass constraint data arrays to the solver:
            //Oni.SetTetherConstraints(batch, particleIndices.GetIntPtr(), maxLengthsScales.GetIntPtr(), stiffnesses.GetIntPtr(), m_ConstraintCount);
            //Oni.SetActiveConstraints(batch, m_ActiveConstraintCount);
        }

        public override void RemoveFromSolver()
        {
            if (m_Constraints != null && m_Constraints.implementation != null)
                m_Constraints.implementation.RemoveBatch(m_BatchImpl);

            if (m_BatchImpl != null)
                m_BatchImpl.Destroy();
        }

        public void SetParameters(float compliance, float scale)
        {
            for (int i = 0; i < stiffnesses.count; i++)
            {
                stiffnesses[i] = compliance;
                maxLengthsScales[i] = new Vector2(maxLengthsScales[i].x, scale);
            }
        }

    }
}
