#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniAerodynamicConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IAerodynamicConstraintsBatchImpl>
    {

        public OniAerodynamicConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.Aerodynamics)
        {
        }

        public IAerodynamicConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch =  new OniAerodynamicConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IAerodynamicConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif