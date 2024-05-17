using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class DoorBehaviour : MonoBehaviour
{
    public bool isLocked { get; private set; }
    private Animator _animator;
    private GameObject _lock;
    public List<string> doorTagsToUnlockOnDefault = new List<string>() { "ExitChallengeRoomDoor" };

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _lock = gameObject.transform.Find("Padlock")?.gameObject;
        
        Lock();

        if (doorTagsToUnlockOnDefault.Contains(gameObject.tag))
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        isLocked = false;
        if (_animator != null)  _animator.SetBool("IsLocked", isLocked);
        if (_lock != null) _lock.SetActive(isLocked);
    }

    public void Lock()
    {
        isLocked = true;
        if (_animator != null) _animator.SetBool("IsLocked", isLocked);
        if (_lock != null) _lock.SetActive(isLocked);
    }
}
