using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    private Rigidbody rb;
    private float yRotate = 0.0f;
    [SerializeField] private Camera cameraLook;
    [SerializeField] public float speed = 5.0f, originalSpeed;
    [SerializeField] private float sensitivity = 60.0f;

    [SerializeField] private Animator _animator;
    //Player Components
    [SerializeField] private PlayerAnimationController _animationController;
    private CapsuleCollider playerCollider;

    private MeshFilter _meshFilter;
    //booleans
    public bool isCrouching = false, isRunning = false, isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        originalSpeed = speed;
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _animationController.isCrouching = isCrouching;
        _animationController.isWalking = isWalking;
        CheckGroundStatus();
        float forward = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X"), mouseY = Input.GetAxis("Mouse Y");

        float jump = Input.GetAxis("Jump");

        //Movement
        if (forward > 0.0f)
        {
            rb.position += rb.transform.forward * (forward * speed * Time.deltaTime);
        }

        if (forward < 0.0f)
        {
            rb.position += rb.transform.forward * (forward * speed * Time.deltaTime) / 2;
        }

        if (horizontal != 0.0f && !isRunning)
        {
            rb.position += rb.transform.right * (horizontal * originalSpeed * Time.deltaTime)/2;
        }
        
        if (horizontal != 0.0f || forward != 0.0f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }




        if (Input.GetAxis("Run") != 0.0f && !isCrouching && forward>0.0f)
        {
            isRunning = true;   
            if(speed<=originalSpeed*2)
            speed = speed +  originalSpeed * Input.GetAxis("Run") * Time.deltaTime * 2;
        }
        else
        {
            
            isRunning = false;
            speed = originalSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCrouching)
            {
                PlayerCrouch();
            }
            else
            {
                if(CheckGroundStatus())
                    rb.AddForce(Vector3.up * 6, ForceMode.VelocityChange);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerCrouch();
        }
        
        

        //Mouse movement horizontal
        // if(mouseX != 0.0f)
        transform.Rotate(Vector3.up* (mouseX * sensitivity * Time.deltaTime), Space.Self);

        //Mouse movement vertical
        yRotate -= mouseY * sensitivity * Time.deltaTime;
        yRotate = Mathf.Clamp (yRotate, -60.0f, 60.0f);        
        cameraLook.transform.eulerAngles = new Vector3 (yRotate, transform.eulerAngles.y, transform.eulerAngles.z); 
        
        
        
    }

    private void PlayerCrouch()
    {
        isCrouching = !isCrouching;
        if (isCrouching)
        {
            playerCollider.height = 1.0f;
            return;
        }
        playerCollider.height = 2.0f;
    }
    bool CheckGroundStatus()
    {
        RaycastHit hit;
        Vector3 groundCheck = transform.TransformDirection(Vector3.down) * 1.2f;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(landingRay, out hit, 1.2f))
        {
            _animationController.isJumping = false;
            Debug.DrawRay(transform.position, groundCheck, Color.green);
            return true;
        }
        else
        {
            _animationController.isJumping = true;
            Debug.DrawRay(transform.position, groundCheck, Color.red);

            return false;
        }
    }
    
    
}