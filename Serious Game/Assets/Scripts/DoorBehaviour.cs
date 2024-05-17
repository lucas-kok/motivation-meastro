using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class DoorBehaviour : MonoBehaviour
{
    public bool isLocked { get; private set; }
    private Animator _animator;
    private GameObject _lock;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _lock = gameObject.transform.Find("Padlock")?.gameObject;
        Unlock();
    }

    public void Unlock()
    {
        isLocked = false;
        _animator.SetBool("IsLocked", isLocked);
        _lock.SetActive(isLocked);
    }

    public void Lock()
    {
        isLocked = true;
        _animator.SetBool("IsLocked", isLocked);
        _lock.SetActive(isLocked);
    }
}
