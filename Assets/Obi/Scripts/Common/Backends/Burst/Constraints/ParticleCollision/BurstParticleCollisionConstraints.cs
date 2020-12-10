#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;
using Unity.Jobs;

namespace Obi
{
    public class BurstParticleCollisionConstraints : BurstConstraintsImpl<BurstParticleCollisionConstraintsBatch>, IConstraintsImpl<IParticleCollisionConstraintsBatchImpl>
    {
        public BurstParticleCollisionConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.ParticleCollision)
        {
        }

        public IParticleCollisionConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstParticleCollisionConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }

        public void RemoveBatch(IParticleCollisionConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstParticleCollisionConstraintsBatch);
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