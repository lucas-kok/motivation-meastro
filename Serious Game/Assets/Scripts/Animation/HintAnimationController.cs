using UnityEngine;

public class HintAnimationController : MonoBehaviour
{
    public string animationName;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnBecameVisible()
    {
        _animator.Play(animationName);
    }
}
