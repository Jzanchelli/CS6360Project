#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstBendConstraints : BurstConstraintsImpl<BurstBendConstraintsBatch>, IConstraintsImpl<IBendConstraintsBatchImpl>
    {
        public BurstBendConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Bending)
        {
        }

        public IBendConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstBendConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IBendConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstBendConstraintsBatch);
        }
    }
}
#endif