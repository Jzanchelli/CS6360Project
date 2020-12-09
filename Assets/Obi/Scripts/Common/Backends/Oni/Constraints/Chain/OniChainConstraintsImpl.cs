#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniChainConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IChainConstraintsBatchImpl>
    {

        public OniChainConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Chain)
        {
        }

        public IChainConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniChainConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IChainConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif