using UnityEngine;
using System;

namespace Obi
{
    public class NullSolverImpl : ISolverImpl
    {

        public void Destroy()
        {
        }

        public void InitializeFrame(Vector4 translation, Vector4 scale, Quaternion rotation)
        {
        }

        public void UpdateFrame(Vector4 translation, Vector4 scale, Quaternion rotation, float deltaTime)
        {
        }

        public void ApplyFrame(float worldLinearInertiaScale, float worldAngularInertiaScale, float deltaTime)
        {
        }

        public int GetDeformableTriangleCount()
        {
            return 0;
        }
        public void SetDeformableTriangles(int[] indices, int num, int destOffset)
        {
           
        }
        public int RemoveDeformableTriangles(int num, int sourceOffset)
        {
            return 0;
        }

        public void ParticleCountChanged(ObiSolver solver)
        {
        }


        public void ParticleCapacityChanged(ObiSolver solver)
        {

        }

        public void SetRigidbodyArrays(ObiSolver solver)
        {
        }

        public void SetActiveParticles(int[] indices, int num)
        {
        }

        public void ResetForces()
        {
        }

        public void GetBounds(ref Vector3 min, ref Vector3 max)
        {
        }

        public void SetParameters(Oni.SolverParameters parameters)
        {
        }

        public int GetConstraintCount(Oni.ConstraintType type)
        {
            return 0;
        }

        public void GetCollisionContacts(Oni.Contact[] contacts, int count)
        {
        }

        public void GetParticleCollisionContacts(Oni.Contact[] contacts, int count)
        {
        }

        public void SetConstraintGroupParameters(Oni.ConstraintType type, ref Oni.ConstraintParameters parameters)
        {
        }

        public IConstraintsImpl<IDistanceConstraintsBatchImpl> CreateDistanceConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IBendConstraintsBatchImpl> CreateBendConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IVolumeConstraintsBatchImpl> CreateVolumeConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IAerodynamicConstraintsBatchImpl> CreateAerodynamicConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IStretchShearConstraintsBatchImpl> CreateStretchShearConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IBendTwistConstraintsBatchImpl> CreateBendTwistConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IChainConstraintsBatchImpl> CreateChainConstraints()
        {
            return null;
        }

        public IConstraintsImpl<ITetherConstraintsBatchImpl> CreateTetherConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IStitchConstraintsBatchImpl> CreateStitchConstraints()
        {
            return null;
        }

        public IConstraintsImpl<ISkinConstraintsBatchImpl> CreateSkinConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IShapeMatchingConstraintsBatchImpl> CreateShapeMatchingConstraints()
        {
            return null;
        }

        public IConstraintsImpl<IPinConstraintsBatchImpl> CreatePinConstraints()
        {
            return null;
        }

        public void DestroyConstraintsGroup(IConstraints group)
        {
        }

        public IObiJobHandle CollisionDetection(float stepTime)
        {
            return null;
        }

        public IObiJobHandle Substep(float substepTime)
        {
            return null;
        }

        public void ApplyInterpolation(ObiNativeVector4List startPositions, ObiNativeQuaternionList startOrientations, float stepTime, float unsimulatedTime)
        {
        }

        public void InterpolateDiffuseProperties(ObiNativeVector4List properties, ObiNativeVector4List diffusePositions, ObiNativeVector4List diffuseProperties, ObiNativeIntList neighbourCount, int diffuseCount)
        {
        }

        public int GetParticleGridSize()
        {
            return 0;
        }
        public void GetParticleGrid(ObiNativeAabbList cells)
        {
        }

    }
}
