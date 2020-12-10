using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiAerodynamicConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiAerodynamicConstraintsData m_Constraints;
        protected IAerodynamicConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeFloatList aerodynamicCoeffs = new ObiNativeFloatList();  /**< 3 floats per constraint: surface area, drag and lift.*/

        public override Oni.ConstraintType constraintType 
        {
            get { return Oni.ConstraintType.Aerodynamics; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiAerodynamicConstraintsBatch(ObiAerodynamicConstraintsData constraints = null, ObiAerodynamicConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiAerodynamicConstraintsBatch(constraints as ObiAerodynamicConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.aerodynamicCoeffs.ResizeUninitialized(aerodynamicCoeffs.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.aerodynamicCoeffs.CopyFrom(aerodynamicCoeffs);

            return clone;
        }

        public void AddConstraint(int index, float area, float drag, float lift)
        {
            RegisterConstraint();

            particleIndices.Add(index);
            aerodynamicCoeffs.Add(area);
            aerodynamicCoeffs.Add(drag);
            aerodynamicCoeffs.Add(lift);
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index]);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            aerodynamicCoeffs.Clear();
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex, destIndex);
            aerodynamicCoeffs.Swap(sourceIndex * 3, destIndex * 3);
            aerodynamicCoeffs.Swap(sourceIndex * 3 + 1, destIndex * 3 + 1);
            aerodynamicCoeffs.Swap(sourceIndex * 3 + 2, destIndex * 3 + 2);
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
                for (int i = 0; i < particleIndices.count; i++)
                {
                    particleIndices[i] = constraints.GetActor().solverIndices[m_Source.particleIndices[i]];
                }

                m_BatchImpl.SetAerodynamicConstraints(particleIndices, aerodynamicCoeffs, m_ConstraintCount);
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

        public void SetParameters(float drag, float lift)
        {
            for (int i = 0; i < particleIndices.count; i++)
            {
                aerodynamicCoeffs[i * 3 + 1] = drag;
                aerodynamicCoeffs[i * 3 + 2] = lift;
            }
        }
    }
}
