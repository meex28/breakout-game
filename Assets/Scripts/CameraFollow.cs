using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    [SerializeField] private Vector3 minValue; 
    [SerializeField] private Vector3 maxValue;

    private Vector3 vel = Vector3.zero;

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;

        // camera bounding
        Vector3 boundPosition = new (
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z)
        );

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, boundPosition, ref vel, damping);
        transform.position = smoothPosition;
    }
}
