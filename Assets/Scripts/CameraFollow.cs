using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform target;
    [SerializeField]private float minX = 2f;
    [SerializeField]private float maxX = 12f;

    private float y = -3.25f;
    private float z = -10f;

    void Start()
    {
        y = transform.position.y;
        z = transform.position.z;
    }

    void LateUpdate()
    {
        float clampCam = Mathf.Clamp(target.position.x, minX, maxX);
        transform.position = new Vector3(clampCam, y, z);
    }
}
