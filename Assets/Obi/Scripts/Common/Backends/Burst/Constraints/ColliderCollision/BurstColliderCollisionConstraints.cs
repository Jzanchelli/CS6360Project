#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstColliderCollisionConstraints : BurstConstraintsImpl<BurstColliderCollisionConstraintsBatch>, IConstraintsImpl<IColliderCollisionConstraintsBatchImpl>
    {
        public BurstColliderCollisionConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Collision)
        {
        }

        public IColliderCollisionConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstColliderCollisionConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }


        public void RemoveBatch(IColliderCollisionConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstColliderCollisionConstraintsBatch);
        }

        public override int GetConstraintCount()
        {
            return ((BurstSolverImpl)solver).colliderContacts.Length;
        }
        public override int GetActiveConstraintCount()
        {
            return ((BurstSolverImpl)solver).colliderContacts.Length;
        }
    }
}
#endif