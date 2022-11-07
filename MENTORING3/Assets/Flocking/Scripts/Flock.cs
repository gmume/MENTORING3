using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject boidPrefab; //Not instantiated yet!!!
    public int numberOfBoids = 30;
    public int initMaxForce = 1;
    public int initMaxSpeed = 4;
    public int initBoidsView = 50;
    private readonly Boid[] flock;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfBoids; i++)
        {
            Boid boid = new();
            boid.Setup(boidPrefab, initMaxForce, initMaxSpeed, initBoidsView);
            flock[i] = boid;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Boid boid in this.flock)
        {
            boid.Edges();
            boid.Flock(this.flock);
            boid.UpdateBoid();
        }
    }
}
