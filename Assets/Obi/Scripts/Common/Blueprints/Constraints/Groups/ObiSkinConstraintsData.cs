using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiSkinConstraintsData : ObiConstraints<ObiSkinConstraintsBatch>
    {
        protected IConstraintsImpl<ISkinConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<ISkinConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiSkinConstraintsData(ObiActor actor = null, ObiSkinConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiSkinConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateSkinConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiSkinConstraintsBatch CreateBatch(ObiSkinConstraintsBatch source = null)
        {
            return new ObiSkinConstraintsBatch(this);
        }
    }
}
