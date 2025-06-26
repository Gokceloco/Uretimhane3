using UnityEngine;

public class Token : MonoBehaviour
{
    public DragManager dragManager;
    private void OnMouseDown()
    {
        dragManager.SetPickedUpObject(this);
    }
}
