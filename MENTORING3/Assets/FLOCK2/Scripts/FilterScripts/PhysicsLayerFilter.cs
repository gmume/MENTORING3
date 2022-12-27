using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new();
        foreach (Transform item in original)
        {
            if (mask == (mask | (1 << item.gameObject.layer)))
            {
                Debug.Log("entered if");
                filtered.Add(item);
                Debug.Log("filtered: " + filtered);
            }
        }
        return filtered;
    }
}