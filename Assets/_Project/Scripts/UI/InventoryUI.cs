using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private bool _isInventoryOpen;
    private CanvasGroup _canvasGroup;

    public CanvasGroup inventoryObjectsUI;

    public List<WeaponType> availableWeapons;
    public List<Button> weaponButtons;

    private bool _isShotGunCollected;

    private WeaponType _activeWeapon;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void RestartInventoryUI()
    {
        if (PlayerPrefs.GetInt("ShotgunCollected") == 1)
        {
            if (!availableWeapons.Contains(WeaponType.Shotgun))
            {
                availableWeapons.Add(WeaponType.Shotgun);
            }
            UpdateInventory();
            _isShotGunCollected = true;            
        }
    }

    private void Start()
    {
        availableWeapons.Add(WeaponType.Machinegun);
        MachinegunButtonPressed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && _activeWeapon != WeaponType.Machinegun)
        {
            MachinegunButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && _isShotGunCollected && _activeWeapon != WeaponType.Shotgun)
        {
            ShotgunButtonPressed();
        }
    }

    public void UpdateInventory()
    {
        foreach (var b in weaponButtons)
        {
            b.gameObject.SetActive(false);
        }
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            weaponButtons[i].gameObject.SetActive(true);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, .2f);
        CloseInventory();
        UpdateInventory();
    }

    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, .2f).OnComplete(()=>gameObject.SetActive(false)).SetUpdate(true);
    }
    public void InventoryButtonPressed()
    {
        if (!_isInventoryOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    private void CloseInventory()
    {
        _isInventoryOpen = false;
        inventoryObjectsUI.DOKill();
        inventoryObjectsUI.DOFade(0,.2f).OnComplete(()=>inventoryObjectsUI.gameObject.SetActive(false));
    }

    private void OpenInventory()
    {
        _isInventoryOpen = true;
        inventoryObjectsUI.gameObject.SetActive(true);
        inventoryObjectsUI.DOKill();
        inventoryObjectsUI.DOFade(1, .2f);
    }

    public void MachinegunButtonPressed()
    {
        GameDirector.instance.player.weapon.WeaponButtonPressed(WeaponType.Machinegun);
        CloseInventory();
        _activeWeapon = WeaponType.Machinegun;
    }
    public void ShotgunButtonPressed()
    {
        GameDirector.instance.player.weapon.WeaponButtonPressed(WeaponType.Shotgun);
        CloseInventory();
        _activeWeapon = WeaponType.Shotgun;
    }

    internal void WeaponCollected(WeaponType weaponType)
    {
        availableWeapons.Add(weaponType);
        UpdateInventory();
        if (weaponType == WeaponType.Shotgun)
        {
            _isShotGunCollected = true;
        }
    }
}
