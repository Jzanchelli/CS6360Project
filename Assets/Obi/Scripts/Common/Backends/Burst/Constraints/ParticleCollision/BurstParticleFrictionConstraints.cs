#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstParticleFrictionConstraints : BurstConstraintsImpl<BurstParticleFrictionConstraintsBatch>, IConstraintsImpl<IParticleFrictionConstraintsBatchImpl>
    {
        public BurstParticleFrictionConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.ParticleFriction)
        {
        }

        public IParticleFrictionConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstParticleFrictionConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IParticleFrictionConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstParticleFrictionConstraintsBatch);
        }

        public override int GetConstraintCount()
        {
            return ((BurstSolverImpl)solver).particleContacts.Length;
        }
        public override int GetActiveConstraintCount()
        {
            return ((BurstSolverImpl)solver).particleContacts.Length;
        }
    }
}
#endif