using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject boidPrefab;
    public int numberOfBoids = 30;
    private readonly Boid[] flock;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate prefab.
        for (int i = 0; i < numberOfBoids; i++)
        {
            //Quaternion rotation = Quaternion.identity;
            //rotation.eulerAngles = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f));

            //Instantiate(boidPrefab, new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f)), rotation);

            flock[i] = new Boid();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }


}
