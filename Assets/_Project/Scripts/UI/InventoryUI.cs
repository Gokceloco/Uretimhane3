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

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MachinegunButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
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
    }
    public void ShotgunButtonPressed()
    {
        GameDirector.instance.player.weapon.WeaponButtonPressed(WeaponType.Shotgun);
        CloseInventory();
    }
}
