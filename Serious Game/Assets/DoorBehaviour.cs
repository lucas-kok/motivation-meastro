using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool isLocked { get; private set; }
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Unlock();
    }

    public void Unlock()
    {
        isLocked = false;
        _animator.SetBool("IsLocked", isLocked);
    }

    public void Lock()
    {
        isLocked = true;
        _animator.SetBool("IsLocked", isLocked);
    }
}
