#if (OBI_BURST && OBI_MATHEMATICS && OBI_COLLECTIONS)
using System;

namespace Obi
{
    public class BurstColliderFrictionConstraints : BurstConstraintsImpl<BurstColliderFrictionConstraintsBatch>, IConstraintsImpl<IColliderFrictionConstraintsBatchImpl>
    {
        public BurstColliderFrictionConstraints(BurstSolverImpl solver) : base(solver, Oni.ConstraintType.Friction)
        {
        }

        public IColliderFrictionConstraintsBatchImpl CreateConstraintsBatch()
        {
            var dataBatch = new BurstColliderFrictionConstraintsBatch(this);
            batches.Add(dataBatch);
            return dataBatch;
        }


        public void RemoveBatch(IColliderFrictionConstraintsBatchImpl batch)
        {
            batches.Remove(batch as BurstColliderFrictionConstraintsBatch);
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

