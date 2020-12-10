#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstAerodynamicConstraints : BurstConstraintsImpl<BurstAerodynamicConstraintsBatch>, IConstraintsImpl<IAerodynamicConstraintsBatchImpl>
    {
        public BurstAerodynamicConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Aerodynamics)
        {
        }

        public IAerodynamicConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstAerodynamicConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IAerodynamicConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstAerodynamicConstraintsBatch);
        }
    }
}
#endif
