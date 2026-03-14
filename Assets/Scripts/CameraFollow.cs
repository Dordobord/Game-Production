using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField]private float minX = 2f;
    [SerializeField]private float maxX = 12f;

    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    private float z = -10f;

    void Start()
    {
        z = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float clampX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampX, clampY, z);
    }
}
