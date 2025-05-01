using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;

    private void Update()
    {
        transform.position = followObject.position;
    }
}
