using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Doesn't have a Prefab yet.
public class Boid : MonoBehaviour
{
    private Vector3 position = new();
    private Quaternion rotation = Quaternion.identity;
    private Vector3 velocity;
    private Vector3 acceleration;
    private int maxForce; //Controls, how fast they align.
    private int maxSpeed;
    private int boidsView; //perseption

    // Start is called before the first frame update
    public void Start()
    {
        velocity = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
        acceleration = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
    }

    public void Setup(int initialMaxForce, int initialMaxSpeed, int initialBoidsView)
    {
        rotation.eulerAngles = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
        maxForce = initialMaxForce;
        maxSpeed = initialMaxSpeed;
        boidsView = initialBoidsView;
    }

    //Donut world
    public void Edges()
    {
        if (position.x > 300)
        {
            position.x = 0;
        }
        else if (position.x < 0)
        {
            position.x = 300;
        }

        if (position.y > 300)
        {
            position.y = 0;
        }
        else if (position.y < 0)
        {
            position.y = 300;
        }

        if (position.y > 300)
        {
            position.y = 0;
        }
        else if (position.y < 0)
        {
            position.y = 300;
        }
    }

    Vector3 Align(Boid[] flock)
    {
        Vector3 steering = new(); //Vector (force) to correct the direction
        int total = 0;

        foreach (Boid otherBoid in flock)
        {
            float distance = Vector3.Distance(position, otherBoid.position);
            if (otherBoid != this && distance <= boidsView)
            {
                steering += otherBoid.velocity;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
            steering = Vector3.ClampMagnitude(steering, maxSpeed);
            steering -= velocity;
            steering = Vector3.ClampMagnitude(steering, maxForce);
        }
        return steering;
    }

    Vector3 Cohesion(Boid[] flock)
    {
        Vector3 steering = new();
        int total = 0;

        foreach (Boid otherBoid in flock)
        {
            float distance = Vector3.Distance(position, otherBoid.position);
            if (otherBoid != this && distance <= boidsView)
            {
                steering += otherBoid.position;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
            steering -= position;
            steering = Vector3.ClampMagnitude(steering, maxSpeed);
            steering -= velocity;
            steering = Vector3.ClampMagnitude(steering, maxForce);
        }
        return steering;
    }

    Vector3 Separation(Boid[] flock)
    {
        Vector3 steering = new();
        int total = 0;

        foreach (Boid otherBoid in flock)
        {
            float distance = Vector3.Distance(position, otherBoid.position);
            if (otherBoid != this && distance <= boidsView)
            {
                Vector3 diff = position - otherBoid.position;
                diff /= distance * distance;
                steering += diff;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
            steering = Vector3.ClampMagnitude(steering, maxSpeed);
            steering -= velocity;
            steering = Vector3.ClampMagnitude(steering, maxForce);
        }
        return steering;
    }

    //Apply rules
    public void Flock(Boid[] flock)
    {
        Vector3 alignment = Align(flock);
        Vector3 cohesion = Cohesion(flock);
        Vector3 separation = Separation(flock);

        acceleration += alignment;
        acceleration += cohesion;
        acceleration += separation;
    }

    public void UpdateBoid()
    {
        position += velocity;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        acceleration *= 0; //Reset vector
    }
}
