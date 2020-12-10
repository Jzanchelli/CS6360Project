#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstDistanceConstraints : BurstConstraintsImpl<BurstDistanceConstraintsBatch>, IConstraintsImpl<IDistanceConstraintsBatchImpl>
    {
        public BurstDistanceConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Distance)
        {
        }

        public IDistanceConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstDistanceConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IDistanceConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstDistanceConstraintsBatch);
        }
    }
}
#endif