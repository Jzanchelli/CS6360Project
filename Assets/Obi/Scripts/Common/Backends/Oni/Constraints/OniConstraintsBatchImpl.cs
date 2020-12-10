#if (OBI_ONI_SUPPORTED)
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Obi
{
    public class OniConstraintsBatchImpl : IConstraintsBatchImpl
    {
        protected IConstraints m_Constraints;
        protected Oni.ConstraintType m_ConstraintType;
        protected IntPtr m_OniBatch;
        protected bool m_Enabled;

        public IntPtr oniBatch
        {
            get { return m_OniBatch; }
        }

        public Oni.ConstraintType constraintType
        {
            get { return m_ConstraintType; }
        }

        public IConstraints constraints
        {
            get { return m_Constraints; } 
        }

        public bool enabled
        {
            set
            {
                if (m_Enabled != value)
                {
                    m_Enabled = value;
                    Oni.EnableBatch(m_OniBatch, m_Enabled);
                }
            }
            get { return m_Enabled; }
        }

        public OniConstraintsBatchImpl(IConstraints constraints, Oni.ConstraintType type)
        {
            this.m_Constraints = constraints;
            this.m_ConstraintType = type;

            m_OniBatch = Oni.CreateBatch((int)type);
        }

        public void Destroy()
        {
            //Oni.DestroyBatch(m_OniBatch);

            // remove the constraint batch from the solver 
            // (no need to destroy it as its destruction is managed by the solver)
            // just reset the reference.
            m_OniBatch = IntPtr.Zero;
        }

        public void SetDependency(IConstraintsBatchImpl batch)
        {
            if (batch != null)
                Oni.SetDependency(m_OniBatch, ((OniConstraintsBatchImpl)batch).oniBatch);
            else
                Oni.SetDependency(m_OniBatch, IntPtr.Zero);
        }
        public void SetConstraintCount(int constraintCount)
        {
            Oni.SetConstraintCount(m_OniBatch, constraintCount);
        }
        public void SetActiveConstraints(int activeConstraintCount)
        {
            Oni.SetActiveConstraints(m_OniBatch, activeConstraintCount);
        }

        public int GetConstraintCount()
        {
            // TODO:
            return 0;
        }

        public int GetActiveConstraintCount()
        {
            // TODO:
            return 0;
        }

    }
}
#endif