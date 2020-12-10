using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiBendTwistConstraintsData : ObiConstraints<ObiBendTwistConstraintsBatch>
    {
        protected IConstraintsImpl<IBendTwistConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IBendTwistConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiBendTwistConstraintsData(ObiActor actor = null, ObiBendTwistConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiBendTwistConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateBendTwistConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiBendTwistConstraintsBatch CreateBatch(ObiBendTwistConstraintsBatch source = null)
        {
            return new ObiBendTwistConstraintsBatch(this);
        }
    }
}
