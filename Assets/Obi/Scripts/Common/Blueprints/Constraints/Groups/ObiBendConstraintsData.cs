using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiBendConstraintsData : ObiConstraints<ObiBendConstraintsBatch>
    {
        protected IConstraintsImpl<IBendConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IBendConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiBendConstraintsData(ObiActor actor = null, ObiBendConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiBendConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateBendConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiBendConstraintsBatch CreateBatch(ObiBendConstraintsBatch source = null)
        {
            return new ObiBendConstraintsBatch(this);
        }
    }
}
