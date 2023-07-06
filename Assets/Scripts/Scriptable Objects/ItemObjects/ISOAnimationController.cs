using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ISOAnimationController : MonoBehaviour
{
    [SerializeField] private Tool toolObject;
    private AnimationClip clip;

    private Animator animator; 
    //public GameObject leftHand;
    
    // Start is called before the first frame update
    void Start()
    {
        //clip = toolObject.toolItem.OnLeftClickAnimationClip;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<Animator>().SetLayerWeight(1, 1);
            GetComponent<Animator>().Play(toolObject.toolItem.OnLeftClickAnimationClip.name, 1);
            //GetComponent<Animator>().SetLayerWeight(1, 0);
        }
    }
}
