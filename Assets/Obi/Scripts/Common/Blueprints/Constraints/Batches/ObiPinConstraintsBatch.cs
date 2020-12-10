using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Obi
{
    [Serializable]
    public class ObiPinConstraintsBatch : ObiConstraintsBatch
    {
        [NonSerialized] protected ObiPinConstraintsData m_Constraints;
        protected IPinConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public List<ObiColliderHandle> pinBodies = new List<ObiColliderHandle>();                         /**< Pin bodies.*/

        [HideInInspector] public ObiNativeIntList colliderIndices = new ObiNativeIntList();
        [HideInInspector] public ObiNativeVector4List offsets = new ObiNativeVector4List();                         /**< Offset expressed in the attachment's local space.*/
        [HideInInspector] public ObiNativeQuaternionList restDarbouxVectors = new ObiNativeQuaternionList();        /**< Rest Darboux vector for each constraint.*/
        [HideInInspector] public ObiNativeFloatList stiffnesses = new ObiNativeFloatList();                         /**< Stiffnesses of pin constraits. 2 float per constraint (positional and rotational stiffness).*/
        [HideInInspector] public ObiNativeFloatList breakThresholds = new ObiNativeFloatList();                     /**< one float per constraint: break threshold. */

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.Pin; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiPinConstraintsBatch(ObiPinConstraintsData constraints = null, ObiPinConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiPinConstraintsBatch(constraints as ObiPinConstraintsData, this);

            // careful here: since IntPtr is not serializable and the pinBodies array can be null, use offsets count instead.
            clone.pinBodies.Capacity = offsets.count;
            clone.pinBodies.Clear();

            if (pinBodies != null)
            {
                for (int i = 0; i < offsets.count; ++i)
                    clone.pinBodies.Add(pinBodies[i]);
            }
            else
            {
                for (int i = 0; i < offsets.count; ++i)
                    clone.pinBodies.Add(new ObiColliderHandle());
            }
                

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.offsets.ResizeUninitialized(offsets.count);
            clone.restDarbouxVectors.ResizeUninitialized(restDarbouxVectors.count);
            clone.stiffnesses.ResizeUninitialized(stiffnesses.count);
            clone.breakThresholds.ResizeUninitialized(breakThresholds.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.offsets.CopyFrom(offsets);
            clone.restDarbouxVectors.CopyFrom(restDarbouxVectors);
            clone.stiffnesses.CopyFrom(stiffnesses);
            clone.breakThresholds.CopyFrom(breakThresholds);

            return clone;
        }

        public void AddConstraint(int index, ObiColliderBase body, Vector3 offset, Quaternion restDarboux)
        {
            RegisterConstraint();

            particleIndices.Add(index);
            pinBodies.Add(body != null ? body.Handle : new ObiColliderHandle());
            colliderIndices.Add(body != null ? body.Handle.index : -1);
            offsets.Add(offset);
            restDarbouxVectors.Add(restDarboux);
            stiffnesses.Add(0);
            stiffnesses.Add(0);
            breakThresholds.Add(float.PositiveInfinity);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            pinBodies.Clear();
            colliderIndices.Clear();
            offsets.Clear();
            restDarbouxVectors.Clear();
            stiffnesses.Clear();
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index]);
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex, destIndex);
            pinBodies.Swap(sourceIndex, destIndex);
            colliderIndices.Swap(sourceIndex, destIndex);
            offsets.Swap(sourceIndex, destIndex);
            restDarbouxVectors.Swap(sourceIndex, destIndex);
            stiffnesses.Swap(sourceIndex * 2, destIndex * 2);
            stiffnesses.Swap(sourceIndex * 2 + 1, destIndex * 2 + 1);
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
                {
                    if (m_Source != null)
                        particleIndices[i] = constraints.GetActor().solverIndices[m_Source.particleIndices[i]];

                    stiffnesses[i * 2] = 0;
                    stiffnesses[i * 2 + 1] = 0;
                    lambdas.Add(0);
                    lambdas.Add(0);
                    lambdas.Add(0);
                    lambdas.Add(0);
                }

                UpdateColliderIndices();

                m_BatchImpl.SetPinConstraints(particleIndices, colliderIndices, offsets, restDarbouxVectors, stiffnesses, lambdas, m_ConstraintCount);
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

        public void UpdateColliderIndices()
        {
            if (m_BatchImpl != null)
            {
                for (int i = 0; i < pinBodies.Count; ++i)
                    colliderIndices[i] = pinBodies[i] != null ? pinBodies[i].index : -1;
            }
        }

        public void BreakConstraints(float stepTime)
        {

            float sqrTime = stepTime * stepTime;
            for (int i = 0; i < constraintCount; i++)
            {
                if (-lambdas[i * 4 + 3] / sqrTime > breakThresholds[i])
                    DeactivateConstraint(i);
            }

        }
    }
}
