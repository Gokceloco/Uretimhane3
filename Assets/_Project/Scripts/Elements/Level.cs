using UnityEngine;

public class Level : MonoBehaviour
{
    public float levelTimeLimit;

    public void StopEnemies()
    {
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StopEnemy();
        }
    }
}
