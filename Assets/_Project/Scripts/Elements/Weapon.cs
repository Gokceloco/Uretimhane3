using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Transform shootStartTransform;

    public Bullet bulletPrefab;

    public float attackRateForMachinegun;
    public float attackRateForShotgun;
    public int shotgunBulletCount;
    public float spreadForShotgun;

    private float _attackTimer;

    public ParticleSystem muzzlePS;
    public Light weaponShootLight;

    public ParticleSystem shellPS;

    public WeaponType weaponType;

    public GameObject machinegunMesh;
    public GameObject shotgunMesh;
    public PlayerAnimator playerAnimator;

    private void Update()
    {
        if (GameDirector.instance.gameState != GameState.GamePlay)
        {
            return;
        }
        if (Input.GetMouseButton(0) && _attackTimer > attackRateForMachinegun 
            && !EventSystem.current.IsPointerOverGameObject() && weaponType == WeaponType.Machinegun)
        {
            ShootForMachinegun();
        }

        if (Input.GetMouseButtonUp(0) && _attackTimer > attackRateForShotgun
            && !EventSystem.current.IsPointerOverGameObject() && weaponType == WeaponType.Shotgun)
        {
            ShootForShotgun();
        }

        if (_attackTimer < attackRateForMachinegun + 1)
        {
            _attackTimer += Time.deltaTime;
        }
    }

    private void ShootForShotgun()
    {
        for (int i = 0; i < shotgunBulletCount; i++)
        {
            var spread = new Vector3(Random.Range(-spreadForShotgun, spreadForShotgun),
                Random.Range(-spreadForShotgun, spreadForShotgun) * .5f,
                0);
            var newBullet = Instantiate(bulletPrefab);
            var newBulletTransform = newBullet.transform;
            newBulletTransform.position = shootStartTransform.position;
            newBulletTransform.LookAt(newBulletTransform.position + shootStartTransform.forward + spread);
            newBullet.StartBullet(this);
        }
        _attackTimer = 0;
        weaponShootLight.DOKill();
        weaponShootLight.intensity = 0;
        weaponShootLight.DOIntensity(50, .1f).SetLoops(2, LoopType.Yoyo);
        muzzlePS.Play();
        GameDirector.instance.cameraHolder.ShakeCamera(.5f, .5f);
        shellPS.Play();
        GameDirector.instance.audioManager.PlayShotgunShootSFX();
    }

    private void ShootForMachinegun()
    {
        var newBullet = Instantiate(bulletPrefab);
        var newBulletTransform = newBullet.transform;
        newBulletTransform.position = shootStartTransform.position;
        newBulletTransform.LookAt(newBulletTransform.position + shootStartTransform.forward);
        newBullet.StartBullet(this);
        _attackTimer = 0;
        GameDirector.instance.audioManager.PlayMachinegunShootSFX();

        weaponShootLight.DOKill();
        weaponShootLight.intensity = 0;
        weaponShootLight.DOIntensity(50,.1f).SetLoops(2, LoopType.Yoyo);
        muzzlePS.Play();

        GameDirector.instance.cameraHolder.ShakeCamera(.2f,.2f);
        shellPS.Play();
    }

    public void WeaponButtonPressed(WeaponType wType)
    {
        weaponType = wType;
        playerAnimator.PlayDrawAnimation();

        if (weaponType == WeaponType.Machinegun)
        {
            shotgunMesh.SetActive(false);
            machinegunMesh.SetActive(true);
        }
        else if (weaponType == WeaponType.Shotgun)
        {
            shotgunMesh.SetActive(true);
            machinegunMesh.SetActive(false);
        }
    }

}

public enum WeaponType
{
    Machinegun,
    Shotgun,
}