#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniStretchShearConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IStretchShearConstraintsBatchImpl>
    {

        public OniStretchShearConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.StretchShear)
        {
        }

        public IStretchShearConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniStretchShearConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IStretchShearConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif