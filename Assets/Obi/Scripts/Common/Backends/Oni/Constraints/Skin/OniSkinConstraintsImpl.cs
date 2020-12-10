#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniSkinConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<ISkinConstraintsBatchImpl>
    {

        public OniSkinConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Skin)
        {
        }

        public ISkinConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniSkinConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(ISkinConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif