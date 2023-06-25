using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private FPSController _controller;
    public bool isWalking, isJumping, isCrouching;
    
    private Mesh meshModel;

  

    // Start is called before the first frame update
    private Animator _animator;
    void Start()
    {
        isWalking = isJumping = isCrouching = false;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.speed > _controller.originalSpeed)
        {
            _animator.SetBool("Run", true);
            
            _animator.ResetTrigger("Idle");
            _animator.ResetTrigger("Walk");
        }
        else _animator.SetBool("Run", false);
        
        if(isWalking && _controller.speed <= _controller.originalSpeed)
        {
         
            _animator.SetTrigger("Walk");
            
            _animator.SetBool("Run", false);
            _animator.ResetTrigger("Idle");
        }
        
        if(!isWalking || isJumping || isCrouching)
        {
            _animator.SetTrigger("Idle");
            
            _animator.SetBool("Run", false);
            _animator.ResetTrigger("Walk");
            
       }
        
    }
}
