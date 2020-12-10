using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiDistanceConstraintsData : ObiConstraints<ObiDistanceConstraintsBatch>
    {
        protected IConstraintsImpl<IDistanceConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IDistanceConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiDistanceConstraintsData(ObiActor actor = null, ObiDistanceConstraintsData source = null) : base(actor, source)
        {

        }
        
        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiDistanceConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateDistanceConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiDistanceConstraintsBatch CreateBatch(ObiDistanceConstraintsBatch source = null)
        {
            return new ObiDistanceConstraintsBatch(this);
        }
    }
}
