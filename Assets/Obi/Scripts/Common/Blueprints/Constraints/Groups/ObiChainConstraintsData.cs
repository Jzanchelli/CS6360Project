using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiChainConstraintsData : ObiConstraints<ObiChainConstraintsBatch>
    {
        protected IConstraintsImpl<IChainConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IChainConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiChainConstraintsData(ObiActor actor = null, ObiChainConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiChainConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateChainConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiChainConstraintsBatch CreateBatch(ObiChainConstraintsBatch source = null)
        {
            return new ObiChainConstraintsBatch(this);
        }
    }
}
