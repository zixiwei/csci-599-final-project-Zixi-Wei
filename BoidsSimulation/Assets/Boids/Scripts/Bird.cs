using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boids
{
    public class Bird : MonoBehaviour
    {

        #region Initialization

       
        private void Awake()
        {
            
            Rigidbody = GetComponent<Rigidbody>();
        }

        
        public void Initialize(Flock flock)
        {
            
            Rigidbody.velocity = transform.forward.normalized * flock.FlockSettings.MinSpeed;

          
            Flock = flock;
        }

        #endregion

        #region Fields/Properties

        private Rigidbody Rigidbody;

        
        private Flock Flock;

        #endregion

        #region Methods

        
        private void Update()
        {
            
            Vector3 acceleration = Vector3.zero;

         
            acceleration += NormalizeSteeringForce(ComputeCohisionForce())
                * Flock.FlockSettings.CohesionForceWeight;

            acceleration += NormalizeSteeringForce(ComputeSeperationForce())
                * Flock.FlockSettings.SeperationForceWeight;

          
            acceleration += NormalizeSteeringForce(ComputeAlignmentForce())
                * Flock.FlockSettings.AlignmentForceWeight;

           
            acceleration += NormalizeSteeringForce(ComputeCollisionAvoidanceForce()) 
                * Flock.FlockSettings.CollisionAvoidanceForceWeight;

            
            Vector3 velocity = Rigidbody.velocity;
            velocity += acceleration * Time.deltaTime;

            
            velocity = velocity.normalized * Mathf.Clamp(velocity.magnitude,
                Flock.FlockSettings.MinSpeed, Flock.FlockSettings.MaxSpeed);

            
            Rigidbody.velocity = velocity;

           
            transform.forward = Rigidbody.velocity.normalized;
        }

       
        private Vector3 NormalizeSteeringForce(Vector3 force)
        {
            return force.normalized * Mathf.Clamp(force.magnitude, 0, Flock.FlockSettings.MaxSteerForce);
        }

        private Vector3 ComputeCohisionForce()
        {
            
            if (Flock.Birds.Count == 1)
                return Vector3.zero;

            
            if (Flock.FlockSettings.UseCenterForCohesion)
            {
                
                Vector3 center = Flock.CenterPosition;

                float newCenterX = center.x * Flock.Birds.Count - transform.localPosition.x;
                float newCenterY = center.y * Flock.Birds.Count - transform.localPosition.y;
                float newCenterZ = center.z * Flock.Birds.Count - transform.localPosition.z;
                Vector3 newCenter = new Vector3(newCenterX, newCenterY, newCenterZ) / (Flock.Birds.Count - 1);

               
                return newCenter - transform.localPosition;
            }

            
            float centerX = 0, centerY = 0, centerZ = 0;
            int count = 0;
            foreach (Bird bird in Flock.Birds)
            {
                if (bird == this
                    || (bird.transform.position - transform.position).magnitude > Flock.FlockSettings.CohesionRadiusThreshold)
                    continue;

                centerX += bird.transform.localPosition.x;
                centerY += bird.transform.localPosition.y;
                centerZ += bird.transform.localPosition.z;
                count++;
            }

            
            return count == 0 
                ? Vector3.zero 
                : new Vector3(centerX, centerY, centerZ) / count;
        }

       
        private Vector3 ComputeSeperationForce()
        {
            
            Vector3 force = Vector3.zero;

           
            foreach (Bird bird in Flock.Birds)
            {
                if (bird == this
                    || (bird.transform.position - transform.position).magnitude > Flock.FlockSettings.SeperationRadiusThreshold)
                    continue;

                
                force += transform.position - bird.transform.position;
            }

            return force;
        }

        
        private Vector3 ComputeAlignmentForce()
        {
           
            Vector3 force = Vector3.zero;

            foreach (Bird bird in Flock.Birds)
            {
                if (bird == this
                    || (bird.transform.position - transform.position).magnitude > Flock.FlockSettings.AlignmentRadiusThreshold)
                    continue;

                force += bird.transform.forward;
            }

            return force;
        }

        
        private Vector3 ComputeCollisionAvoidanceForce()
        {
           
            if (!Physics.SphereCast(transform.position,
                Flock.FlockSettings.CollisionAvoidanceRadiusThreshold, 
                transform.forward, 
                out RaycastHit hitInfo,
                Flock.FlockSettings.CollisionAvoidanceRadiusThreshold))
                return Vector3.zero;

            
            return transform.position - hitInfo.point;
        }

        #endregion

    }
}
