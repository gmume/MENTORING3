using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Boid boidPrefab;
    List<Boid> boids = new();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    const float BoidDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neightborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neightborRadius * neightborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Boid newBoid = Instantiate(
                boidPrefab,
                Random.insideUnitCircle * startingCount * BoidDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newBoid.name = "Boid " + i;
            newBoid.Initialize(this); //tell boid, that it's part of this flock
            boids.Add(newBoid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Boid boid in boids)
        {
            List<Transform> context = GetNearbyObjects(boid);

            //FOR DEMO ONLY
            //boid.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector3 move = behavior.CalculateMove(boid, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            boid.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(Boid boid)
    {
        List<Transform> context = new();
        Collider[] contextColliders = Physics.OverlapSphere(boid.transform.position, neightborRadius);
        foreach(Collider c in contextColliders)
        {
            if(c !=boid.BoidCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
