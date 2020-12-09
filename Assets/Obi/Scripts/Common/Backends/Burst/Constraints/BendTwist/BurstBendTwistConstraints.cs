#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstBendTwistConstraints : BurstConstraintsImpl<BurstBendTwistConstraintsBatch>, IConstraintsImpl<IBendTwistConstraintsBatchImpl>
    {
        public BurstBendTwistConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.BendTwist)
        {
        }

        public IBendTwistConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstBendTwistConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IBendTwistConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstBendTwistConstraintsBatch);
        }
    }
}
#endif