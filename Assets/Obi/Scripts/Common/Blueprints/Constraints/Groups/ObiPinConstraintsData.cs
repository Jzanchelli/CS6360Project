using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiPinConstraintsData : ObiConstraints<ObiPinConstraintsBatch>
    {
        protected IConstraintsImpl<IPinConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IPinConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiPinConstraintsData(ObiActor actor = null, ObiPinConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiPinConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreatePinConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiPinConstraintsBatch CreateBatch(ObiPinConstraintsBatch source = null)
        {
            return new ObiPinConstraintsBatch(this);
        }
    }
}
