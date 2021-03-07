using System.Collections;
using System.Collections.Generic;
using AJ;
using UnityEngine;

namespace AJ
{
    [CreateAssetMenu(menuName = "Flock/Behaviour,Alignment")]
    public class AlignmentBehaviour : FilteredFlockBehaviour
    {
        private FlockBehaviour _flockBehaviourImplementation;

        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            //if no neighbours, maintain current alignment
            if (context.Count == 0)
                return agent.transform.forward;
            
            Vector3 alignmentMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                alignmentMove += (Vector3) item.transform.transform.forward;
            }
            alignmentMove /= context.Count;
            return alignmentMove;
        }
    }
}