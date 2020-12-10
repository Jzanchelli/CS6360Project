#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniBendTwistConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IBendTwistConstraintsBatchImpl>
    {

        public OniBendTwistConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.BendTwist)
        {
        }

        public IBendTwistConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniBendTwistConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IBendTwistConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif