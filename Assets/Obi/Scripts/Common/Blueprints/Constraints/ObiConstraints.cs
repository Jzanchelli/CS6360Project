using UnityEngine;
using System.Collections.Generic;
using System;

namespace Obi
{
    public interface IObiConstraints
    {
        ObiActor GetActor();
        Oni.ConstraintType? GetConstraintType();

        IObiConstraintsBatch GetBatch(int i); 
        int GetBatchCount();
        void Clear();

        bool AddToSolver();
        bool RemoveFromSolver();
        void SetEnabled(bool enabled);

        int GetConstraintCount();
        int GetActiveConstraintCount();
        void DeactivateAllConstraints();

        IObiConstraints Clone(ObiActor actor);
    }

    [Serializable]
    public abstract class ObiConstraints<T> : IObiConstraints where T : class, IObiConstraintsBatch
    {
        [NonSerialized] protected ObiActor actor;
        [NonSerialized] protected ObiConstraints<T> source;

        [NonSerialized] protected bool inSolver;
        [HideInInspector] public List<T> batches = new List<T>();

        public ObiConstraints(ObiActor actor = null, ObiConstraints<T> source = null)
        {
            this.actor = actor;
            this.source = source;

            if (source != null)
            {
                foreach (T batch in source.batches)
                    AddBatch(batch.Clone(this) as T);
            }
        }

        public abstract IObiConstraints Clone(ObiActor actor);
        public virtual void CreateImplementation() { }// TODO: should be abstract
        public virtual void DestroyImplementation() { }// TODO: should be abstract

        public ObiActor GetActor()
        {
            return actor;
        }

        public IObiConstraintsBatch GetBatch(int i)
        {
            return (IObiConstraintsBatch) batches[i];
        }

        public int GetBatchCount()
        {
            return batches == null ? 0 : batches.Count;
        }

        public int GetConstraintCount()
        {
            int count = 0;
            if (batches == null) return count;

            foreach (T batch in batches)
                if (batch != null)
                    count += batch.constraintCount;

            return count;
        }

        public int GetActiveConstraintCount()
        {
            int count = 0;
            if (batches == null) return count;

            foreach (T batch in batches)
                if (batch != null)
                    count += batch.activeConstraintCount;

            return count;
        }

        public void DeactivateAllConstraints()
        {
            if (batches != null)
                foreach (T batch in batches)
                    if (batch != null)
                        batch.DeactivateAllConstraints();
        }

        public T GetFirstBatch()
        {
            return (batches != null && batches.Count > 0) ? batches[0] : null;
        }

        public Oni.ConstraintType? GetConstraintType()
        {
            if (batches != null && batches.Count > 0)
                return batches[0].constraintType;
            else return null;
        }

        public void Clear()
        {
            RemoveFromSolver();

            if (batches != null)
                batches.Clear();
        }

        public virtual T CreateBatch(T source = null)
        {
            return null;
        }

        public void AddBatch(T batch)
        {
            if (batch != null)
                batches.Add(batch);
        }

        public bool RemoveBatch(T batch)
        {
            if (batches == null || batch == null)
                return false;
            return batches.Remove(batch);
        }

        public bool AddToSolver()
        {

            if (inSolver || actor == null || actor.solver == null || batches == null)
                return false;

            inSolver = true;

            CreateImplementation();

            foreach (T batch in batches)
                batch.AddToSolver();

            GenerateBatchDependencies();

            // enable/disable all batches:
            SetEnabled(actor.isActiveAndEnabled);

            return true;

        }

        public bool RemoveFromSolver()
        {

            if (!inSolver || actor == null || actor.solver == null || batches == null)
                return false;

            DestroyImplementation();

            foreach (T batch in batches)
                batch.RemoveFromSolver();

            inSolver = false;

            return true;

        }

        private void GenerateBatchDependencies()
        {
            if (inSolver && batches != null)
            {
                T prevBatch = null;
                foreach (T batch in batches)
                {
                    if (batch != null)
                    {
                        // each batch depends on the previous one:
                        batch.SetDependency(prevBatch);
                        prevBatch = batch;
                    }
                }
            }
        }

        public void SetEnabled(bool enabled)
        {
            foreach (T batch in batches)
                if (batch != null)
                    batch.SetEnabled(enabled);
        }
    }
}
