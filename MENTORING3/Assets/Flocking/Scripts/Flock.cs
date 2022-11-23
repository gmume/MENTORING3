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
    private Boid[] flock;

    // Start is called before the first frame update
    void Start()
    {
        flock = new Boid[numberOfBoids];
        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 position = MathUtilities.Random(new Vector3(-40f, 40f, 40f), new Vector3(-40f, 40f, 40f));
            GameObject boid = Instantiate(boidPrefab, position, Quaternion.identity);
            Boid boidScript = boid.AddComponent(typeof(Boid)) as Boid;
            boidScript.Setup(initMaxForce, initMaxSpeed, initBoidsView);
            //Debug.Log("boid: "+boid);
            flock[i] = boidScript;
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
