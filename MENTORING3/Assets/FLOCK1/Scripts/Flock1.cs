using System;
using UnityEngine;

public class Flock11 : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public int numberOfBoid1s;
    public float initMaxForce;
    public float initMaxSpeed;
    public int initBoid1sView;
    public GameObject boidPrefab;
    private Boid1[] flock;
    private readonly float interval = 0.03f;
    private float nextUpdate = 0;
    private int boidNo = 1;

    // Start is called before the first frame update
    void Start()
    {
        flock = new Boid1[numberOfBoid1s];
        for (int i = 0; i < numberOfBoid1s; i++)
        {
            Vector3 position = MathUtilities.Random(new Vector3(-5f, 5f, 5f), new Vector3(-5f, 5f, 5f));
            GameObject boid = Instantiate(boidPrefab, position, Quaternion.identity);
            Boid1 boidScript = boid.AddComponent(typeof(Boid1)) as Boid1;
            boidScript.Setup(initMaxForce, initMaxSpeed, initBoid1sView, boid, boidNo);
            flock[i] = boidScript;
            boidNo += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("entred Update");
        if (Time.time >= nextUpdate)
        {
            //Debug.Log("TimeElapsed: "+TimeElapsed);
            foreach (Boid1 boid in flock)
            {
                //boid.Edges();
                boid.Flock1(flock);
                boid.UpdateBoid1();
            }
            nextUpdate += interval;
        }
    }
}
