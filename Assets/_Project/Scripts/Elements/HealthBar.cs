using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform fillBarPrent;
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    public void UpdateHealthBar(float ratio)
    {
        if (ratio >= 1)
        {
            gameObject.SetActive(false);
        }
        else if (ratio <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        fillBarPrent.localScale = new Vector3(ratio, 1, 1);
    }

    private void Update()
    {
        transform.LookAt(_cameraTransform.position);
    }
}
