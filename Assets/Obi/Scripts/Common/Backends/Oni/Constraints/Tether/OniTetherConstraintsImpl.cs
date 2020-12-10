#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniTetherConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<ITetherConstraintsBatchImpl>
    {

        public OniTetherConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Tether)
        {
        }

        public ITetherConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniTetherConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(ITetherConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif