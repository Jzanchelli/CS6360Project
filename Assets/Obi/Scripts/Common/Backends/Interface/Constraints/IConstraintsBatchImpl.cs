
using UnityEngine;
using System.Collections;

namespace Obi
{
    public interface IConstraintsBatchImpl
    {
        Oni.ConstraintType constraintType
        {
            get;
        }

        IConstraints constraints
        {
            get;
        }

        bool enabled
        {
            set;
            get;
        }

        void Destroy();
        void SetDependency(IConstraintsBatchImpl batch);
        void SetConstraintCount(int constraintCount);
        void SetActiveConstraints(int activeConstraintCount);
        int GetConstraintCount();
        int GetActiveConstraintCount();
    }
}
