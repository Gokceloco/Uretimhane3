using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    private PlayerAnimator _playerAnimator;
    public int startHealth;
    private int _currentHealth;

    private PlayerNavigator _playerNavigator;

    private bool _isDead;

    public GameObject interactingObject;
    public float touchDistance;
    public LayerMask interactableLayerMask;

    private bool _haveKey;

    public Weapon weapon;

    public Grenade grenadePrefab;
    public float grenadeSpawnUpOffset;
    public float grenadeSpawnLeftOffset;

    public float grenadeCoolDown;
    private float _lastGrenadeThrowTime;

    private void Awake()
    {
        _playerNavigator = GetComponent<PlayerNavigator>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * touchDistance);
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out var hit, touchDistance, interactableLayerMask))
        {
            interactingObject = hit.transform.gameObject;
        }
        else
        {
            interactingObject = null;
        }
        if (Input.GetKeyDown(KeyCode.E) && interactingObject != null)
        {
            ExecuteInteractAction();
        }
        if (Input.GetMouseButtonDown(1) && Time.time - _lastGrenadeThrowTime > grenadeCoolDown)
        {
            _playerAnimator.PlayThrowGrenadeAnimation();
            Invoke(nameof(ThrowGrenade), .6f);
            _lastGrenadeThrowTime = Time.time;
        }
        gameDirector.greandeCoolDownUI.UpdateCoolDownImage((Time.time - _lastGrenadeThrowTime) / grenadeCoolDown);

        if (transform.position.y < -10 && gameDirector.gameState == GameState.GamePlay)
        {
            gameDirector.LevelFailed(0);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GetHit();
        }
    }

    private void ThrowGrenade()
    {
        var newGrenade = Instantiate(grenadePrefab);
        newGrenade.transform.position = transform.position + Vector3.up * grenadeSpawnUpOffset - transform.right * grenadeSpawnLeftOffset;
        newGrenade.StartGrenade();
    }

    private void ExecuteInteractAction()
    {
        var door = interactingObject.GetComponent<Door>();
        if (door != null)
        {
            door.DoorInteracted(_haveKey);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            _haveKey = true;
        }
        if (other.CompareTag("WeaponCollectable"))
        {
            if (other.GetComponent<WeaponCollectable>().weaponType == WeaponType.Shotgun
                && PlayerPrefs.GetInt("ShotgunCollected") == 0)
            {
                gameDirector.inventoryUI.WeaponCollected(other.GetComponent<WeaponCollectable>().weaponType);
                if (other.GetComponent<WeaponCollectable>().weaponType == WeaponType.Shotgun)
                {
                    PlayerPrefs.SetInt("ShotgunCollected", 1);
                }
            }            
            other.gameObject.SetActive(false);
        }
    }

    internal void RestartPlayer()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _isDead = false;
        _currentHealth = startHealth;
        gameDirector.playerHealthUI.UpdateHealth(1);
        gameObject.SetActive(true);
        _playerNavigator.ResetPosition();
        _playerAnimator.PlayIdleAnimation();
    }

    internal void GetHit()
    {
        if (_isDead)
        {
            return;
        }
        _currentHealth -= 1;
        if (_currentHealth <= 0)
        {
            Die();
        }
        gameDirector.audioManager.PlayGetHitSFX();
        gameDirector.cameraHolder.ShakeCamera(.5f, .5f);
        gameDirector.playerHealthUI.UpdateHealth((float)_currentHealth / startHealth);
        gameDirector.playerHitUI.PopPlayerHitUI();
    }

    private void Die()
    {
        _isDead = true;
        gameDirector.LevelFailed(2);
        _playerAnimator.PlayFallBackAnimation();
    }

    public void UseKey()
    {
        _haveKey = false;
    }

    public void TimeIsUp()
    {
        _isDead = true;
        _playerAnimator.PlayFallDownAnimation();
    }
}
