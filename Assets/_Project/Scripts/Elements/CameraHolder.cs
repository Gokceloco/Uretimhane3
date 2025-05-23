using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;
    public float offsetByLookDirection;

    private Vector3 _vel;
    public float smoothTime;

    private void FixedUpdate()
    {
        var targetPos = followObject.position + followObject.forward * offsetByLookDirection;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _vel, smoothTime);
    }
}
