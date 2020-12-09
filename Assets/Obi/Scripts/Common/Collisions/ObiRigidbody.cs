using UnityEngine;
using System;
using System.Collections;

namespace Obi{

	/**
	 * Small helper class that lets you specify Obi-only properties for rigidbodies.
	 */

	[ExecuteInEditMode]
	[RequireComponent(typeof(Rigidbody))]
	public class ObiRigidbody : ObiRigidbodyBase
	{
		private Rigidbody unityRigidbody;

		public override void Awake(){
			unityRigidbody = GetComponent<Rigidbody>();
			base.Awake();
		}

		public override void UpdateIfNeeded(){

            var rb = ObiColliderWorld.GetInstance().rigidbodies[handle.index];

            velocity = rb.velocity = unityRigidbody.velocity;
            angularVelocity = rb.angularVelocity = unityRigidbody.angularVelocity;

            rb.FromRigidbody(unityRigidbody, false);

            ObiColliderWorld.GetInstance().rigidbodies[handle.index] = rb;

        }

		/**
		 * Reads velocities back from the solver.
		 */
		public override void UpdateVelocities(Vector3 linearDelta, Vector3 angularDelta)
        {

			// kinematic rigidbodies are passed to Obi with zero velocity, so we must ignore the new velocities calculated by the solver:
			if (Application.isPlaying && (unityRigidbody.isKinematic || !kinematicForParticles))
            {
                unityRigidbody.velocity += linearDelta;
                unityRigidbody.angularVelocity += angularDelta;
            }
		}
	}
}

