using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiStretchShearConstraintsData : ObiConstraints<ObiStretchShearConstraintsBatch>
    {
        protected IConstraintsImpl<IStretchShearConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IStretchShearConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiStretchShearConstraintsData(ObiActor actor = null, ObiStretchShearConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiStretchShearConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateStretchShearConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiStretchShearConstraintsBatch CreateBatch(ObiStretchShearConstraintsBatch source = null)
        {
            return new ObiStretchShearConstraintsBatch(this);
        }
    }
}
