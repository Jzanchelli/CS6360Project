#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniPinConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IPinConstraintsBatchImpl>
    {

        public OniPinConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Pin)
        {
        }

        public IPinConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniPinConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IPinConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif