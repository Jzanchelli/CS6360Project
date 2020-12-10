#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstPinConstraints : BurstConstraintsImpl<BurstPinConstraintsBatch>, IConstraintsImpl<IPinConstraintsBatchImpl>
    {
        public BurstPinConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Pin)
        {
        }

        public IPinConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstPinConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IPinConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstPinConstraintsBatch);
        }
    }
}
#endif