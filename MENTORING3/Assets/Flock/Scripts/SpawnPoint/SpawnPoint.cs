using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public BoolVariable isSpawnPointVisible;

    private void OnEnable()
    {
        MeshRenderer visibleSphere = this.GetComponent<MeshRenderer>();
        if (isSpawnPointVisible.value)
        {
            SphereCollider triggerSphere = transform.parent.GetComponent<SphereCollider>();
            visibleSphere.enabled = true;
            float size = triggerSphere.radius * 2;
            transform.localScale = new Vector3(size, size, size);
        } else
        {
            visibleSphere.enabled = false;
        }
    }
}
