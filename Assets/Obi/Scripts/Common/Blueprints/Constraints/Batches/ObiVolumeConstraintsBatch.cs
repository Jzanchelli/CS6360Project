using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiVolumeConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiVolumeConstraintsData m_Constraints;
        protected IVolumeConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeIntList firstTriangle = new ObiNativeIntList();               /**< index of first triangle for each constraint.*/
        [HideInInspector] public ObiNativeFloatList restVolumes = new ObiNativeFloatList();             /**< rest volume for each constraint.*/
        [HideInInspector] public ObiNativeVector2List pressureStiffness = new ObiNativeVector2List();   /**< 2 floats per constraint: pressure and stiffness.*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.Volume; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiVolumeConstraintsBatch(ObiVolumeConstraintsData constraints = null, ObiVolumeConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiVolumeConstraintsBatch(constraints as ObiVolumeConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.firstTriangle.ResizeUninitialized(firstTriangle.count);
            clone.restVolumes.ResizeUninitialized(restVolumes.count);
            clone.pressureStiffness.ResizeUninitialized(pressureStiffness.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.firstTriangle.CopyFrom(firstTriangle);
            clone.restVolumes.CopyFrom(restVolumes);
            clone.pressureStiffness.CopyFrom(pressureStiffness);

            return clone;
        }

        public void AddConstraint(int[] triangles, float restVolume)
        {
            RegisterConstraint();

            firstTriangle.Add((int)particleIndices.count / 3);
            particleIndices.AddRange(triangles);
            restVolumes.Add(restVolume);
            pressureStiffness.Add(new Vector2(1,0));
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            firstTriangle.Clear();
            restVolumes.Clear();
            pressureStiffness.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            //TODO.
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            firstTriangle.Swap(sourceIndex, destIndex);
            restVolumes.Swap(sourceIndex, destIndex);
            pressureStiffness.Swap(sourceIndex, destIndex);
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

                for (int i = 0; i < restVolumes.count; i++)
					lambdas.Add(0);

				m_BatchImpl.SetVolumeConstraints(particleIndices, firstTriangle, restVolumes, pressureStiffness, lambdas, m_ConstraintCount);
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

        public void SetParameters(float compliance, float pressure)
        {
            Vector2 p = new Vector2(pressure, compliance);
            for (int i = 0; i < pressureStiffness.count; i++)
                pressureStiffness[i] = p;
        }
    }
}
