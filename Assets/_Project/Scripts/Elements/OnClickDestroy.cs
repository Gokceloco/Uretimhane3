using UnityEngine;

public class OnClickDestroy : MonoBehaviour
{
    private void OnMouseDown()
    {
        gameObject.SetActive(false);
    }
}
