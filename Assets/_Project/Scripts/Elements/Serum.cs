using UnityEngine;

public class Serum : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameDirector.instance.Win();
            gameObject.SetActive(false);
        }
    }
}
