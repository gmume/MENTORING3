using System;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public int numberOfBoids;
    public float initMaxForce;
    public float initMaxSpeed;
    public int initBoidsView;
    public GameObject boidPrefab;
    private Boid[] flock;
    private  float interval = 0.03f;
    private float nextUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        flock = new Boid[numberOfBoids];
        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 position = MathUtilities.Random(new Vector3(-5f, 5f, 5f), new Vector3(-5f, 5f, 5f));
            GameObject boid = Instantiate(boidPrefab, position, Quaternion.identity);
            Boid boidScript = boid.AddComponent(typeof(Boid)) as Boid;
            boidScript.Setup(initMaxForce, initMaxSpeed, initBoidsView, boid);
            flock[i] = boidScript;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            //Debug.Log("TimeElapsed: "+TimeElapsed);
            foreach (Boid boid in flock)
            {
                boid.Edges();
                boid.Flock(flock);
                boid.UpdateBoid();
            }
            nextUpdate += interval;
        }
    }
}
