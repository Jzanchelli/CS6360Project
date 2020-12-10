using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Obi
{
    public interface IObiConstraintsBatch
    {
        int constraintCount
        {
            get;
        }

        int activeConstraintCount
        {
            get;
            set;
        }

        int initialActiveConstraintCount
        {
            get;
            set;
        }

        Oni.ConstraintType constraintType
        {
            get;
        }

        IntPtr oniBatch
        {
            get;
        }

        IConstraintsBatchImpl implementation
        {
            get;
        }

        IObiConstraints constraints
        {
            get;
        }

        IObiConstraintsBatch Clone(IObiConstraints constraints);

        void AddToSolver();
        void RemoveFromSolver();

        bool DeactivateConstraint(int constraintIndex);
        bool ActivateConstraint(int constraintIndex);
        void DeactivateAllConstraints();

        void SetEnabled(bool enabled);
        void SetDependency(IObiConstraintsBatch batch);
        void Clear();

        void GetParticlesInvolved(int index, List<int> particles);
        void ParticlesSwapped(int index, int newIndex);
    }

    public abstract class ObiConstraintsBatch : IObiConstraintsBatch
    {

        [NonSerialized] protected ObiConstraintsBatch m_Source;
        [NonSerialized] protected IObiConstraintsBatch m_Dependency;   /**< batch this one depends on. Batches sharing particles must be processed sequentially.*/

        [HideInInspector] [SerializeField] protected List<int> m_IDs = new List<int>();
        [HideInInspector] [SerializeField] protected List<int> m_IDToIndex = new List<int>();         /**< maps from constraint ID to constraint index. When activating/deactivating constraints, their order changes. That makes this
                                                         map necessary. All active constraints are at the beginning of the constraint arrays, in the 0, activeConstraintCount index range.*/

        [HideInInspector] [SerializeField] protected int m_ConstraintCount = 0;
        [HideInInspector] [SerializeField] protected int m_ActiveConstraintCount = 0;
        [HideInInspector] [SerializeField] protected int m_InitialActiveConstraintCount = 0;
        [HideInInspector] public ObiNativeIntList particleIndices = new ObiNativeIntList();  /**< particle indices, amount of them per constraint can be variable. */
        [HideInInspector] public ObiNativeFloatList lambdas = new ObiNativeFloatList();      /**< constraint lambdas */

        public int constraintCount
        {
            get { return m_ConstraintCount; }
        }

        public int activeConstraintCount
        {
            get { return m_ActiveConstraintCount; }
            set { m_ActiveConstraintCount = value; }
        }

        public virtual int initialActiveConstraintCount
        {
            get { return m_InitialActiveConstraintCount; }
            set { m_InitialActiveConstraintCount = value; }
        }

        public abstract Oni.ConstraintType constraintType
        {
            get;
        }

        public IntPtr oniBatch
        {
            get { return IntPtr.Zero; } // TODO: Remove.
        }

        public virtual IConstraintsBatchImpl implementation //TODO: should be abstract
        {
            get { return null; }
        }

        public virtual IObiConstraints constraints // TODO: should be abstract
        {
            get;
        }

        public ObiConstraintsBatch(ObiConstraintsBatch source)
        {
            this.m_Source = source;

            if (m_Source != null)
            {
                m_ConstraintCount = m_Source.m_ConstraintCount;
                m_ActiveConstraintCount = m_Source.m_ActiveConstraintCount;
                m_InitialActiveConstraintCount = m_Source.m_InitialActiveConstraintCount;

                m_IDs = new List<int>(m_Source.m_IDs);
                m_IDToIndex = new List<int>(m_Source.m_IDToIndex);
            }
        }

        public abstract IObiConstraintsBatch Clone(IObiConstraints constraints);
        protected abstract void SwapConstraints(int sourceIndex, int destIndex);
        public abstract void GetParticlesInvolved(int index, List<int> particles);
        public abstract void AddToSolver();
        public abstract void RemoveFromSolver();

        protected virtual void CopyConstraint(ObiConstraintsBatch batch, int constraintIndex) { }

        private void InnerSwapConstraints(int sourceIndex, int destIndex)
        {
            m_IDToIndex[m_IDs[sourceIndex]] = destIndex;
            m_IDToIndex[m_IDs[destIndex]] = sourceIndex;
            m_IDs.Swap(sourceIndex, destIndex);
            SwapConstraints(sourceIndex, destIndex);
        }

        /**
         * Registers a new constraint. Call this before adding a new contraint to the batch, so that the constraint is given an ID 
         * and the amount of constraints increased.
         */
        protected void RegisterConstraint()
        {
            m_IDs.Add(m_ConstraintCount);
            m_IDToIndex.Add(m_ConstraintCount);
            m_ConstraintCount++;
        }

        public virtual void Clear()
        {
            m_ConstraintCount = 0;
            m_ActiveConstraintCount = 0;
            m_IDs.Clear();
            m_IDToIndex.Clear();
        }

        /**
         * Given the id of a constraint, return its index in the constraint data arrays. Will return -1 if the constraint does not exist.
         */
        public int GetConstraintIndex(int constraintId)
        {
            if (constraintId < 0 || constraintId >= constraintCount)
                return -1;
            return m_IDToIndex[constraintId];
        }

        public bool IsConstraintActive(int index)
        {
            return index < m_ActiveConstraintCount;
        }

        public bool ActivateConstraint(int constraintIndex)
        {
            if (constraintIndex < m_ActiveConstraintCount)
                return false;

            InnerSwapConstraints(constraintIndex, m_ActiveConstraintCount);
            m_ActiveConstraintCount++;

            if (implementation != null)
                implementation.SetActiveConstraints(m_ActiveConstraintCount);

            return true;
        }

        public bool DeactivateConstraint(int constraintIndex)
        {
            if (constraintIndex >= m_ActiveConstraintCount)
                return false;

            m_ActiveConstraintCount--;
            InnerSwapConstraints(constraintIndex, m_ActiveConstraintCount);

            if (implementation != null)
                implementation.SetActiveConstraints(m_ActiveConstraintCount);

            return true;
        }

        public void DeactivateAllConstraints()
        {
            m_ActiveConstraintCount = 0;
            implementation.SetActiveConstraints(m_ActiveConstraintCount);
        }

        // Moves a constraint to another batch: First, copies it to the new batch. Then, removes it from this one.
        public void MoveConstraintToBatch(int constraintIndex,ObiConstraintsBatch destBatch)
        {
            destBatch.CopyConstraint(this,constraintIndex);
            RemoveConstraint(constraintIndex);
        }

        // Swaps the constraint with the last one and reduces the amount of constraints by one.
        public void RemoveConstraint(int constraintIndex)
        {
            SwapConstraints(constraintIndex, constraintCount - 1);
            m_IDs.RemoveAt(constraintCount - 1);
            m_IDToIndex.RemoveAt(constraintCount - 1);

            m_ConstraintCount--;
            m_ActiveConstraintCount = Mathf.Min(m_ActiveConstraintCount, m_ConstraintCount);

            if (implementation != null)
            {
                implementation.SetConstraintCount(m_ConstraintCount);
                implementation.SetActiveConstraints(m_ActiveConstraintCount);
            }
        }

        public void ParticlesSwapped(int index, int newIndex)
        {
            for (int i = 0; i < particleIndices.count; ++i)
            {
                if (particleIndices[i] == newIndex)
                    particleIndices[i] = index;
                else if (particleIndices[i] == index)
                    particleIndices[i] = newIndex;
            }
        }

        public void SetDependency(IObiConstraintsBatch dependency)
        {
            if (implementation != null)
            {
                m_Dependency = dependency;

                if (m_Dependency != null)
                    implementation.SetDependency(m_Dependency.implementation);
                else
                    implementation.SetDependency(null);
            }
        }

        public void SetEnabled(bool enabled)
        {
            if (implementation != null)
                implementation.enabled = enabled;
        }

    }
}
