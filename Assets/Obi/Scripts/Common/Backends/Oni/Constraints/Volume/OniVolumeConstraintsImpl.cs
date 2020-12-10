#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniVolumeConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IVolumeConstraintsBatchImpl>
    {

        public OniVolumeConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Volume)
        {
        }

        public IVolumeConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniVolumeConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IVolumeConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif