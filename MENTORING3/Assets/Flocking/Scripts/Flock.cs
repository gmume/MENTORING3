using System;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public int numberOfBoids = 30;
    public int initMaxForce = 1;
    public int initMaxSpeed = 4;
    public int initBoidsView = 50;
    public GameObject boidPrefab;
    public Boid[] flock;

    // Start is called before the first frame update
    void Start()
    {
        flock = new Boid[numberOfBoids];
        for (int i = 0; i < numberOfBoids; i++)
        {
            Boid boid = gameObject.AddComponent(typeof(Boid)) as Boid;
            boid.Setup(boidPrefab, initMaxForce, initMaxSpeed, initBoidsView);
            //Debug.Log("boid: "+boid);
            flock[i] = boid;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Boid boid in flock)
        {
            boid.Edges();
            boid.Edges();
            boid.Flock(flock);
            boid.UpdateBoid();
        }
    }
}
