using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Obi
{
    [Serializable]
    public class ObiStretchShearConstraintsBatch : ObiConstraintsBatch, IStructuralConstraintBatch
    {
        [NonSerialized] protected ObiStretchShearConstraintsData m_Constraints;
        protected IStretchShearConstraintsBatchImpl m_BatchImpl;   /**< pointer to constraint batch implementation.*/

        [HideInInspector] public ObiNativeIntList orientationIndices = new ObiNativeIntList();                      /**< index of particle orientation for each constraint.*/
        [HideInInspector] public ObiNativeFloatList restLengths = new ObiNativeFloatList();                         /**< rest distance for each constraint.*/
        [HideInInspector] public ObiNativeQuaternionList restOrientations = new ObiNativeQuaternionList();          /**< rest orientation for each constraint.*/
        [HideInInspector] public ObiNativeVector3List stiffnesses = new ObiNativeVector3List();                     /**< 3 compliance values per constraint, one for each local axis (x,y,z).*/

        public override Oni.ConstraintType constraintType
        {
            get { return Oni.ConstraintType.StretchShear; }
        }

        public override IObiConstraints constraints
        {
            get { return m_Constraints; }
        }

        public override IConstraintsBatchImpl implementation
        {
            get { return m_BatchImpl; }
        }

        public ObiStretchShearConstraintsBatch(ObiStretchShearConstraintsData constraints = null, ObiStretchShearConstraintsBatch source = null) : base(source)
        {
            m_Constraints = constraints;
        }

        public override IObiConstraintsBatch Clone(IObiConstraints constraints)
        {
            var clone = new ObiStretchShearConstraintsBatch(constraints as ObiStretchShearConstraintsData, this);

            clone.particleIndices.ResizeUninitialized(particleIndices.count);
            clone.orientationIndices.ResizeUninitialized(orientationIndices.count);
            clone.restLengths.ResizeUninitialized(restLengths.count);
            clone.restOrientations.ResizeUninitialized(restOrientations.count);
            clone.stiffnesses.ResizeUninitialized(stiffnesses.count);

            clone.particleIndices.CopyFrom(particleIndices);
            clone.orientationIndices.CopyFrom(orientationIndices);
            clone.restLengths.CopyFrom(restLengths);
            clone.restOrientations.CopyFrom(restOrientations);
            clone.stiffnesses.CopyFrom(stiffnesses);

            return clone;
        }

        public void AddConstraint(Vector2Int indices, int orientationIndex, float restLength, Quaternion restOrientation)
        {
            RegisterConstraint();

            particleIndices.Add(indices[0]);
            particleIndices.Add(indices[1]);
            orientationIndices.Add(orientationIndex);
            restLengths.Add(restLength);
            restOrientations.Add(restOrientation);
            stiffnesses.Add(Vector3.zero);
        }

        public override void Clear()
        {
            base.Clear();
            particleIndices.Clear();
            orientationIndices.Clear();
            restLengths.Clear();
            restOrientations.Clear();
            stiffnesses.Clear();
        }

        public float GetRestLength(int index)
        {
            return restLengths[index];
        }

        public void SetRestLength(int index, float restLength)
        {
            restLengths[index] = restLength;
        }

        public ParticlePair GetParticleIndices(int index)
        {
            return new ParticlePair(particleIndices[index * 2], particleIndices[index * 2 + 1]);
        }

        public override void GetParticlesInvolved(int index, List<int> particles)
        {
            particles.Add(particleIndices[index * 2]);
            particles.Add(particleIndices[index * 2 + 1]);
        }

        protected override void SwapConstraints(int sourceIndex, int destIndex)
        {
            particleIndices.Swap(sourceIndex * 2 , destIndex * 2);
            particleIndices.Swap(sourceIndex * 2 + 1, destIndex * 2 + 1);
            orientationIndices.Swap(sourceIndex, destIndex);
            restLengths.Swap(sourceIndex, destIndex);
            restOrientations.Swap(sourceIndex, destIndex);
            stiffnesses.Swap(sourceIndex, destIndex);
        }

        public override void AddToSolver()
        {
            // create and add the implementation:
            if (m_Constraints != null && m_Constraints.implementation != null)
            {
                m_BatchImpl = m_Constraints.implementation.CreateConstraintsBatch();
            }

            if (m_BatchImpl != null)
            {
                lambdas.Clear();
                for (int i = 0; i < restLengths.count; i++)
                {
                    particleIndices[i * 2] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2]];
                    particleIndices[i * 2 + 1] = constraints.GetActor().solverIndices[m_Source.particleIndices[i * 2 + 1]];
                    orientationIndices[i] = constraints.GetActor().solverIndices[((ObiStretchShearConstraintsBatch)m_Source).orientationIndices[i]];
                    lambdas.Add(0);
                    lambdas.Add(0);
                    lambdas.Add(0);
                }

                m_BatchImpl.SetStretchShearConstraints(particleIndices, orientationIndices, restLengths, restOrientations, stiffnesses, lambdas, m_ConstraintCount);
                m_BatchImpl.SetActiveConstraints(m_ActiveConstraintCount);
            }
        }

        public override void RemoveFromSolver()
        {
            if (m_Constraints != null && m_Constraints.implementation != null)
                m_Constraints.implementation.RemoveBatch(m_BatchImpl);

            if (m_BatchImpl != null)
                m_BatchImpl.Destroy();
        }

        public void SetParameters(float stretchCompliance, float shear1Compliance, float shear2Compliance)
        {
            for (int i = 0; i < stiffnesses.count; i++)
            {
                stiffnesses[i] = new Vector3(stretchCompliance, shear1Compliance, shear2Compliance);
            }
        }
    }
}
