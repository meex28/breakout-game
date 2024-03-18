using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    private Vector3 vel = Vector3.zero;

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        transform.position = smoothPosition;
    }
}
