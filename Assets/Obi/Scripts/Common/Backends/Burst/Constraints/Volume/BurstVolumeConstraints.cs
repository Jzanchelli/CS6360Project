#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstVolumeConstraints : BurstConstraintsImpl<BurstVolumeConstraintsBatch>, IConstraintsImpl<IVolumeConstraintsBatchImpl>
    {
        public BurstVolumeConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Volume)
        {
        }

        public IVolumeConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstVolumeConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IVolumeConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstVolumeConstraintsBatch);
        }
    }
}
#endif