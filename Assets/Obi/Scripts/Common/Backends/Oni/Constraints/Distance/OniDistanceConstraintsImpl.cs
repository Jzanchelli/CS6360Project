#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniDistanceConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IDistanceConstraintsBatchImpl>
    {

        public OniDistanceConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Distance)
        {
        }

        public IDistanceConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniDistanceConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IDistanceConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif