#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstSkinConstraints : BurstConstraintsImpl<BurstSkinConstraintsBatch>, IConstraintsImpl<ISkinConstraintsBatchImpl>
    {
        public BurstSkinConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Skin)
        {
        }

        public ISkinConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstSkinConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(ISkinConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstSkinConstraintsBatch);
        }
    }
}
#endif