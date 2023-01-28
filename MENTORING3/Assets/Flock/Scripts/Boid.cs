using UnityEngine;

[RequireComponent(typeof(Collider))] //Adds a collider to the Boid

public class Boid : MonoBehaviour
{
    Flock boidFlock;
    public Flock BoidFlock { get { return boidFlock; } }

    Collider boidCollider;
    public Collider BoidCollider { get { return boidCollider; } } //accessor to collider

    // Start is called before the first frame update
    void Start()
    {
        boidCollider = GetComponent<Collider>();
    }

    public void Initialize(Flock flock)
    {
        boidFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        transform.up = velocity; //The Boid shoud move in direction head forward
        transform.position += velocity * Time.deltaTime;
    }
}
