#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstTetherConstraints : BurstConstraintsImpl<BurstTetherConstraintsBatch>, IConstraintsImpl<ITetherConstraintsBatchImpl>
    {
        public BurstTetherConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Distance)
        {
        }

        public ITetherConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstTetherConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(ITetherConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstTetherConstraintsBatch);
        }
    }
}
#endif