#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstStretchShearConstraints : BurstConstraintsImpl<BurstStretchShearConstraintsBatch>, IConstraintsImpl<IStretchShearConstraintsBatchImpl>
    {
        public BurstStretchShearConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.StretchShear)
        {
        }

        public IStretchShearConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstStretchShearConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IStretchShearConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstStretchShearConstraintsBatch);
        }
    }
}
#endif