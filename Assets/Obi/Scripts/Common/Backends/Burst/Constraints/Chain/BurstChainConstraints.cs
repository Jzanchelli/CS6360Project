#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstChainConstraints : BurstConstraintsImpl<BurstChainConstraintsBatch>, IConstraintsImpl<IChainConstraintsBatchImpl>
    {
        public BurstChainConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Chain)
        {
        }

        public IChainConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstChainConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IChainConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstChainConstraintsBatch);
        }
    }
}
#endif