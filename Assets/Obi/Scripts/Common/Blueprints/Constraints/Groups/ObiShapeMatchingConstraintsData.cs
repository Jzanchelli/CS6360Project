using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiShapeMatchingConstraintsData : ObiConstraints<ObiShapeMatchingConstraintsBatch>
    {
        protected IConstraintsImpl<IShapeMatchingConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IShapeMatchingConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiShapeMatchingConstraintsData(ObiActor actor = null, ObiShapeMatchingConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiShapeMatchingConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateShapeMatchingConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiShapeMatchingConstraintsBatch CreateBatch(ObiShapeMatchingConstraintsBatch source = null)
        {
            return new ObiShapeMatchingConstraintsBatch(this);
        }
    }
}
