#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniShapeMatchingConstraintsImpl : OniConstraintsImpl, IConstraintsImpl<IShapeMatchingConstraintsBatchImpl>
    {

        public OniShapeMatchingConstraintsImpl(OniSolverImpl solver) : base(solver, Oni.ConstraintType.ShapeMatching)
        {
        }

        public IShapeMatchingConstraintsBatchImpl CreateConstraintsBatch()
        {
            var batch = new OniShapeMatchingConstraintsBatchImpl(this);
            Oni.AddBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
            return batch;
        }

        public void RemoveBatch(IShapeMatchingConstraintsBatchImpl batch)
        {
            Oni.RemoveBatch(((OniSolverImpl)solver).oniSolver, ((OniConstraintsBatchImpl)batch).oniBatch);
        }
    }
}
#endif