#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniBendConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IBendConstraintsBatchImpl>
    {

        public OniBendConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Bending)
        {
        }

        public IBendConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniBendConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IBendConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif