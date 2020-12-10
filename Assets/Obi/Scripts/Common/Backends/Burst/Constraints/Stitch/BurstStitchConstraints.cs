#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstStitchConstraints : BurstConstraintsImpl<BurstStitchConstraintsBatch>, IConstraintsImpl<IStitchConstraintsBatchImpl>
    {
        public BurstStitchConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Stitch)
        {
        }

        public IStitchConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstStitchConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IStitchConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstStitchConstraintsBatch);
        }
    }
}
#endif