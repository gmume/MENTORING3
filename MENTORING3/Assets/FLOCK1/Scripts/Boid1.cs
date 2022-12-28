using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid1 : MonoBehaviour
{
    private Vector3 position = new();
    private Quaternion rotation = Quaternion.identity;
    private Vector3 velocity;
    private Vector3 acceleration;
    private float maxForce; //Controls, how fast they align.
    private float maxSpeed;
    private int boidsView; //perseption
    private GameObject boidRef;
    private string boidName = "boid";

    // Start is called before the first frame update
    public void Start()
    {
        //Debug.Log("entred boid Start");
        velocity = MathUtilities.Random(new Vector3(-1.1f, 1.1f, 1.1f), new Vector3(-1.1f, 1.1f, 1.1f));
        acceleration = MathUtilities.Random(new Vector3(-1.1f, 1.1f, 1.1f), new Vector3(-1.1f, 1.1f, 1.1f));
    }

    public void Setup(float initialMaxForce, float initialMaxSpeed, int initialBoid1sView, GameObject boid, int boidNo)
    {
        rotation.eulerAngles = MathUtilities.Random(new Vector3(-5f, 5f, 5f), new Vector3(-5f, 5f, 5f));
        maxForce = initialMaxForce;
        maxSpeed = initialMaxSpeed;
        boidsView = initialBoid1sView;
        boidRef = boid;
        boidRef.transform.Translate(position);
        boidName += boidNo;
        //Debug.Log(boidName + " position: " + position);

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

    Vector3 Align(Boid1[] flock)
    {
        //Debug.Log("entred Align");
        Vector3 steering = new(); //Vector (force) to correct the direction
        int total = 0;

        foreach (Boid1 otherBoid1 in flock)
        {
            float distance = Vector3.Distance(position, otherBoid1.position);
            if (otherBoid1 != this && distance <= boidsView)
            {
                steering += otherBoid1.velocity;
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

    Vector3 Cohesion(Boid1[] flock)
    {
        //Debug.Log("entred Cohesion");
        Vector3 steering = new();
        int total = 0;

        foreach (Boid1 otherBoid1 in flock)
        {
            float distance = Vector3.Distance(position, otherBoid1.position);
            if (otherBoid1 != this && distance <= boidsView)
            {
                steering += otherBoid1.position;
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

    Vector3 Separation(Boid1[] flock)
    {
        //Debug.Log("entred Separation");
        Vector3 steering = new();
        int total = 0;

        foreach (Boid1 otherBoid1 in flock)
        {
            float distance = Vector3.Distance(position, otherBoid1.position);
            if (otherBoid1 != this && distance <= boidsView)
            {
                Vector3 diff = position - otherBoid1.position;
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
    public void Flock1(Boid1[] flock)
    {
        //Debug.Log("entred Flock1");
        Vector3 alignment = Align(flock);
        Vector3 cohesion = Cohesion(flock);
        Vector3 separation = Separation(flock);

        acceleration += alignment;
        acceleration += cohesion;
        acceleration += separation;
        Debug.Log(boidName + " acceleration: " + acceleration);
    }

    public void UpdateBoid1()
    {
        Debug.Log("entred UpdateBoid1");
        Debug.Log(boidName+ " position before: "+position);
        boidRef.transform.Translate(position);
        position += velocity;
        Debug.Log(boidName + " position after: " + position);
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        acceleration *= 0; //Reset vector
    }
}
