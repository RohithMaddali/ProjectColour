using System.Collections;
using System.Collections.Generic;
using AJ;
using UnityEngine;

namespace AJ
{
    [CreateAssetMenu(menuName = "Flock/Behaviour,Cohesion")]
    public class CohesionBehaviour : FilteredFlockBehaviour
    {
        private FlockBehaviour _flockBehaviourImplementation;

        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            //if no neighbours, return no adjustment
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
            return cohesionMove;

        }
        
    }
}