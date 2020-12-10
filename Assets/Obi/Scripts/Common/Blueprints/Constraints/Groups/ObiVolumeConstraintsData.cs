using UnityEngine;
using System.Collections;
using System;

namespace Obi
{
    [Serializable]
    public class ObiVolumeConstraintsData : ObiConstraints<ObiVolumeConstraintsBatch>
    {
        protected IConstraintsImpl<IVolumeConstraintsBatchImpl> m_Implementation;

        public IConstraintsImpl<IVolumeConstraintsBatchImpl> implementation
        {
            get { return m_Implementation; }
        }

        public ObiVolumeConstraintsData(ObiActor actor = null, ObiVolumeConstraintsData source = null) : base(actor, source)
        {

        }

        public override IObiConstraints Clone(ObiActor actor)
        {
            return new ObiVolumeConstraintsData(actor, this);
        }

        public override void CreateImplementation()
        {
            m_Implementation = GetActor().solver.implementation.CreateVolumeConstraints();
        }

        public override void DestroyImplementation()
        {
            GetActor().solver.implementation.DestroyConstraintsGroup(m_Implementation);
        }

        public override ObiVolumeConstraintsBatch CreateBatch(ObiVolumeConstraintsBatch source = null)
        {
            return new ObiVolumeConstraintsBatch(this);
        }
    }
}
