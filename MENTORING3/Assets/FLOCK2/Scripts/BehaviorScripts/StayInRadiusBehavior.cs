using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay in Radius")]

public class StayInRadiusBehavior : FlockBehavior
{
    public Vector3 center;
    public float radius = 15f;

    public override Vector3 CalculateMove(Boid boid, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = center - boid.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }
}
