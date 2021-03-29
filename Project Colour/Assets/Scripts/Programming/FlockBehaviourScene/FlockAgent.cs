using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AJ
{
    [RequireComponent(typeof(Collider))]

    public class FlockAgent : MonoBehaviour
    {
        public Rigidbody rb;
        private Flock agentFlock;
        
        public Flock AgentFlock
        {
            get
            {
                return agentFlock;
            }
        }
        
        Collider agentCollider;
        public float force = 1000;
        public Collider AgentCollider
        {
            get
            {
                return agentCollider;
            }
        }

        public void Initialize(Flock flock)
        {
            agentFlock = flock;
        }

        // Start is called before the first frame update
        void Start()
        {
            agentCollider = GetComponent<Collider>();
        }

        public void Move(Vector3 velocity)
        {
            velocity = new Vector3(velocity.x, 0, velocity.z);
            transform.forward = velocity;
            rb.AddForce(velocity * force * Time.deltaTime);
            transform.position += velocity * Time.deltaTime;
        }
    }
}