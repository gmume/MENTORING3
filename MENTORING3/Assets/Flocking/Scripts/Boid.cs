using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 position = new();
    private Quaternion rotation = Quaternion.identity;
    private Vector3 velocity = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
    private Vector3 acceleration = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
    private Vector3 maxForce; //Controls, how fast they align.
    private Vector3 maxSpeed;
    private float boidsView; //perseption

    // Start is called before the first frame update
    void Setup(Vector3 initialMaxForce, Vector3 initialMaxSpeed, float initialBoidsView)
    {
        this.position = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
        this.rotation.eulerAngles = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
        this.maxForce = initialMaxForce;
        this.maxSpeed = initialMaxSpeed;
        this.boidsView = initialBoidsView;
    }

    Vector3 Align(Boid[] flock)
    {
        Vector3 steering = new(); //Vector (force) to correct the direction
        float total = 0f;
        
        foreach(Boid otherBoid in flock)
        {
            float distance = Vector3.Distance(this.position, otherBoid.position);
            if(otherBoid != this && distance <= this.boidsView)
            {
                steering += otherBoid.velocity;
                total++;
            }
        }

        if(total > 0)
        {
            steering /= total;
            steering = Vector3.ClampMagnitude(steering, this.maxSpeed.magnitude);
            steering -= this.velocity;
            steering = Vector3.ClampMagnitude(steering, this.maxForce.magnitude);
        }
        return steering;
    }

    Vector3 Cohesion(Boid[] flock)
    {
        Vector3 steering = new();
        float total = 0f;

        foreach(Boid otherBoid in flock)
        {
            float distance = Vector3.Distance(this.position, otherBoid.position);
            if (otherBoid != this && distance <= this.boidsView)
            {
                steering += otherBoid.position;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
            steering -= this.position;
            steering = Vector3.ClampMagnitude(steering, this.maxSpeed.magnitude);
            steering -= this.velocity;
            steering = Vector3.ClampMagnitude(steering, this.maxForce.magnitude);
        }
        return steering;
    }

    Vector3 Separation(Boid[] flock)
    {
        Vector3 steering = new();
        float total = 0f;

        foreach (Boid otherBoid in flock)
        {
            float distance = Vector3.Distance(this.position, otherBoid.position);
            if (otherBoid != this && distance <= this.boidsView)
            {
                Vector3 diff = this.position - otherBoid.position;
                diff /= distance * distance;
                steering += diff;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
            steering = Vector3.ClampMagnitude(steering, this.maxSpeed.magnitude);
            steering -= this.velocity;
            steering = Vector3.ClampMagnitude(steering, this.maxForce.magnitude);
        }
        return steering;
    }

    //Apply rules
    void Flock(Boid[] flock)
    {
        Vector3 alignment = this.Align(flock);
        Vector3 cohesion = this.Cohesion(flock);
        Vector3 separation = this.Separation(flock);

        this.acceleration += alignment;
        this.acceleration += cohesion;
        this.acceleration += separation;
    }

    void UpdateBoid()
    {
        this.position += this.velocity;
        this.velocity += this.acceleration;
        this.velocity = Vector3.ClampMagnitude(this.velocity, this.maxSpeed.magnitude);
        this.acceleration *= 0; //Reset vector
    }
}
