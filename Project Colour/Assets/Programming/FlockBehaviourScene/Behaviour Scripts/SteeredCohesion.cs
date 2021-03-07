using System.Collections;
using System.Collections.Generic;
using AJ;
using UnityEngine;

namespace AJ
{
    [CreateAssetMenu(menuName = "Flock/Behaviour,SteeredCohesion")]
    public class SteeredCohesion : FilteredFlockBehaviour
    {
        Vector3 currentVelocity;
        
        //how long it takes the agent to get from it's current state to calculated state.
        public float agentSmoothTime = 0.5f;
        
        private FlockBehaviour _flockBehaviourImplementation;

        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            //if no neighbors, return no adjustments
            if (context.Count == 0)
                return Vector3.zero;
            
            //add all points together and average
            Vector3 cohesionMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                cohesionMove += (Vector3) item.position;
            }
            cohesionMove /= context.Count;

            //create offset from agent position
            cohesionMove -= (Vector3) agent.transform.position;
            cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
            return cohesionMove;

        }
        
    }
}