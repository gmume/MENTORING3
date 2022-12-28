using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]

public class AvoidanceBehavior : FilteredFlockBehavior
{
    //move away from your neighbors
    public override Vector3 CalculateMove(Boid boid, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;

        //add all points together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(boid, context);
        foreach (Transform item in filteredContext)
        {
            Vector3 closestPoint = item.gameObject.GetComponent<Collider>().ClosestPoint(boid.transform.position);
            if (Vector3.SqrMagnitude(closestPoint - boid.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += boid.transform.position - closestPoint;
            }
        }
        if(nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
