using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float range;

    public int damage;

    private Transform _transform;
    private Weapon _weapon;

    public Color enemyImpactParticleColor;
    public Color groundImpactParticleColor;

    private void Awake()
    {
        _transform = transform;
    }
    public void StartBullet(Weapon weapon)
    {
        _weapon = weapon;
    }

    void Update()
    {
        _transform.position += _transform.forward * (Time.deltaTime * speed);
        if ((_transform.position - _weapon.transform.position).magnitude > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHit(damage);
            GameDirector.instance.fXManager.PlayBulletImpactFX(_transform.position, _transform.forward, enemyImpactParticleColor);
            Destroy(gameObject);
        }
        if (other.CompareTag("Ground"))
        {
            GameDirector.instance.fXManager.PlayBulletImpactFX(_transform.position, _transform.forward, groundImpactParticleColor);
            Destroy(gameObject);
        }
    }
}
