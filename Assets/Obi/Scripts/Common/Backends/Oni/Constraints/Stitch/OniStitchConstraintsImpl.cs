#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniStitchConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IStitchConstraintsBatchImpl>
    {

        public OniStitchConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Stitch)
        {
        }

        public IStitchConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniStitchConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IStitchConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif