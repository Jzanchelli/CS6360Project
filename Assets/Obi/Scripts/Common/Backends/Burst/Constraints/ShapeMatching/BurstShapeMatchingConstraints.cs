#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstShapeMatchingConstraints : BurstConstraintsImpl<BurstShapeMatchingConstraintsBatch>, IConstraintsImpl<IShapeMatchingConstraintsBatchImpl>
    {
        public BurstShapeMatchingConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.ShapeMatching)
        {
        }

        public IShapeMatchingConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstShapeMatchingConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IShapeMatchingConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstShapeMatchingConstraintsBatch);
        }
    }
}
#endif