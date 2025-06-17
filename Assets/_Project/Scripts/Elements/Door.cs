using DG.Tweening;
using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked;

    public Transform leftDoor;
    public Transform rightDoor;

    private Player _player;
    private bool _isDoorOpen;

    public float doorCloseDistance;

    private void Start()
    {
        _player = GameDirector.instance.player;
    }

    private void Update()
    {
        if (_isDoorOpen && Vector3.Distance(transform.position, _player.transform.position) > doorCloseDistance)
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        leftDoor.DOKill();
        leftDoor.DOLocalMoveX(-1.3f, .2f);
        rightDoor.DOKill();
        rightDoor.DOLocalMoveX(1.3f, .2f);
        _isDoorOpen = true;
    }

    public void CloseDoor()
    {
        leftDoor.DOKill();
        leftDoor.DOLocalMoveX(0, .2f);
        rightDoor.DOKill();
        rightDoor.DOLocalMoveX(0, .2f);
        _isDoorOpen = false;
    }

    public void DoorInteracted(bool haveKey)
    {
        if (_isDoorOpen)
        {
            CloseDoor();
        }
        else
        {
            if (!isLocked)
            {
                OpenDoor();
            }
            else if (isLocked && haveKey)
            {
                OpenDoor();
                isLocked = false;
                _player.UseKey();
            }
            else if (isLocked)
            {
                GameDirector.instance.messageUI.Show("DOOR IS LOCKED!", 3f);
            }
        }
    }
}
