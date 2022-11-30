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
    private float maxForce; //Controls, how fast they align.
    private float maxSpeed;
    private int boidsView; //perseption
    private GameObject boidRef;

    // Start is called before the first frame update
    public void Start()
    {
        velocity = MathUtilities.Random(new Vector3(-1.1f, 1.1f, 1.1f), new Vector3(-1.1f, 1.1f, 1.1f));
        acceleration = MathUtilities.Random(new Vector3(-1.1f, 1.1f, 1.1f), new Vector3(-1.1f, 1.1f, 1.1f));
    }

    public void Setup(float initialMaxForce, float initialMaxSpeed, int initialBoidsView, GameObject boid)
    {
        rotation.eulerAngles = MathUtilities.Random(new Vector3(-5f, 5f, 5f), new Vector3(-5f, 5f, 5f));
        maxForce = initialMaxForce;
        maxSpeed = initialMaxSpeed;
        boidsView = initialBoidsView;
        boidRef = boid;
        boidRef.transform.Translate(position);

        //Debugging!!!
        //Component[] components = boidRef.GetComponents(typeof(Component));
        //foreach (Component component in components)
        //{
        //    Debug.Log("Component: "+component.ToString());
        //}
    }

    //Donut world
    public void Edges()
    {
        //Debug.Log("entred Edges");
        if (position.x > 5)
        {
            position.x = 0;
        }
        else if (position.x < 0)
        {
            position.x = 5;
        }

        if (position.y > 5)
        {
            position.y = 0;
        }
        else if (position.y < 0)
        {
            position.y = 5;
        }

        if (position.y > 5)
        {
            position.y = 0;
        }
        else if (position.y < 0)
        {
            position.y = 5;
        }
    }

    Vector3 Align(Boid[] flock)
    {
        //Debug.Log("entred Align");
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
        //Debug.Log("entred Cohesion");
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
        //Debug.Log("entred Separation");
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
        //Debug.Log("entred Flock");
        Vector3 alignment = Align(flock);
        Vector3 cohesion = Cohesion(flock);
        Vector3 separation = Separation(flock);

        acceleration += alignment;
        acceleration += cohesion;
        acceleration += separation;
    }

    public void UpdateBoid()
    {
        //Debug.Log("entred UpdateBoid");
        boidRef.transform.Translate(position);
        position += velocity;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        acceleration *= 0; //Reset vector
    }
}
