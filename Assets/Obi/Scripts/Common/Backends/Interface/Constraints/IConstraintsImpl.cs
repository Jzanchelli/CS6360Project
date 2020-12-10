
using UnityEngine;
using System.Collections;

namespace Obi
{
    public interface IConstraints
    {
        Oni.ConstraintType constraintType
        {
            get;
        }

        ISolverImpl solver
        {
            get;
        }

        int GetConstraintCount();
        int GetActiveConstraintCount();
    }

    public interface IConstraintsImpl<T> : IConstraints where T : IConstraintsBatchImpl
    {
        T CreateConstraintsBatch();
        void RemoveBatch(T batch);
    }
}
