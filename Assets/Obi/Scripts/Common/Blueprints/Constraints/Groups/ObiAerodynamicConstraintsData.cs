using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiAerodynamicConstraintsData : ObiConstraints<ObiAerodynamicConstraintsBatch>
    {
        protected IConstraintsImpl<IAerodynamicConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IAerodynamicConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiAerodynamicConstraintsData(ObiActor actor = null, ObiAerodynamicConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor) 
        {
            return new ObiAerodynamicConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateAerodynamicConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiAerodynamicConstraintsBatch CreateBatch(ObiAerodynamicConstraintsBatch source = null)
        {
            return new ObiAerodynamicConstraintsBatch(this);
        }
    }
}
