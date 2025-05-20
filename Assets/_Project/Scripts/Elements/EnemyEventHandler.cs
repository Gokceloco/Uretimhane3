using UnityEngine;

public class EnemyEventHandler : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    public void AttackPlayer()
    {
        print("In Attack Player");
        _enemy.AttackCompleted();
    }
}
