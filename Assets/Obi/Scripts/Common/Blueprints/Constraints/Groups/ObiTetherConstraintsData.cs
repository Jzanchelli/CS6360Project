using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiTetherConstraintsData : ObiConstraints<ObiTetherConstraintsBatch>
    {
        protected IConstraintsImpl<ITetherConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<ITetherConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiTetherConstraintsData(ObiActor actor = null, ObiTetherConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiTetherConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateTetherConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiTetherConstraintsBatch CreateBatch(ObiTetherConstraintsBatch source = null)
        {
            return new ObiTetherConstraintsBatch(this);
        }
    }
}
