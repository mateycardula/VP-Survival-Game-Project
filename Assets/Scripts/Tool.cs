using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Tool : MonoBehaviour
{
    
    public GameObject modelTool;
    //public ToolScriptableObject tool;
    [SerializeField] private GameObject leftHand, rightHand;
    [FormerlySerializedAs("empty")] [SerializeField] private ToolScriptableObject punch;
    public ItemScriptableObject toolItem;
    
    [SerializeField] private Attack attackScriptToSetTime;

    private Animator rightHandAnimator;
    // Start is called before the first frame update
  

    void Start()
    {
        rightHandAnimator = rightHand.GetComponent<Animator>();
        UpdateToolModel();
        modelTool.transform.position = transform.position;
    }

    public void UpdateToolModel(ItemScriptableObject tool = null, int id = -1)
    {
        if (!modelTool.Equals(null))
            GameObject.Destroy(modelTool);
        
        rightHandAnimator.Play("New State", 1);
        
        
        
        if (tool == null)
        {
            toolItem = punch;
            modelTool = Instantiate(punch.model);
            modelTool.transform.SetParent(transform);
            modelTool.transform.position = transform.position;
            if (modelTool.GetComponent<CapsuleCollider>().enabled) modelTool.GetComponent<CapsuleCollider>().enabled = false;
            modelTool.gameObject.GetComponent<Rigidbody>().useGravity = false;
            modelTool.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            attackScriptToSetTime.ResetSpamTimer(toolItem.OnLeftClickAnimationClip.length);
            //Component.Destroy();
            leftHand.SetActive(false);
            return;
        }
        
        
        modelTool = Instantiate(tool.model);
        modelTool.transform.SetParent(transform);
        modelTool.transform.position = transform.position;
        modelTool.transform.rotation = transform.rotation;
        if (modelTool.GetComponent<CapsuleCollider>().enabled) modelTool.GetComponent<CapsuleCollider>().enabled = false;
        modelTool.gameObject.GetComponent<Rigidbody>().useGravity = false;
        modelTool.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        toolItem = tool;
        attackScriptToSetTime.ResetSpamTimer(toolItem.OnLeftClickAnimationClip.length);

        if (tool is ToolScriptableObject)
        {
            if ((tool as ToolScriptableObject).isTwoHanded)
            {
                leftHand.SetActive(true);
            }
            else
            {
                leftHand.SetActive(false);
            }
        }
        else
        {
            leftHand.SetActive(false);
        }
    }

 

    // SetEquipped()
    // {
    //     
    // }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
