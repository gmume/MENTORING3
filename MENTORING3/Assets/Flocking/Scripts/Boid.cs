using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 position = new Vector3();
    private Quaternion rotation = Quaternion.identity;
    private Vector3 velocity = new Vector3(Random.Range(-40.0f, 40.0f), Random.Range(-40.0f, 40.0f), Random.Range(-40.0f, 40.0f));
    private Vector3 acceleration = new Vector3(Random.Range(-40.0f, 40.0f), Random.Range(-40.0f, 40.0f), Random.Range(-40.0f, 40.0f));

    // Start is called before the first frame update
    void Start()
    {
        this.position = MathUtilities.Random(ref this.position, new Vector3(), new Vector3());
        this.rotation.eulerAngles = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
