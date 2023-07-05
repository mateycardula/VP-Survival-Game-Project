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
    [SerializeField] private GameObject rightHand;
    [FormerlySerializedAs("empty")] [SerializeField] private ToolScriptableObject punch;

    private ItemScriptableObject toolItem;
    // Start is called before the first frame update
  

    void Start()
    {
        UpdateToolModel();
        modelTool.transform.position = transform.position;
    }

    public void UpdateToolModel(ItemScriptableObject tool = null, int id = -1)
    {
        if (!modelTool.Equals(null))
            GameObject.Destroy(modelTool);
        
        if (tool == null)
        {
            toolItem = punch;
            modelTool = Instantiate(punch.model);
            modelTool.transform.SetParent(transform);
            modelTool.transform.position = transform.position;
            if (modelTool.GetComponent<BoxCollider>().enabled) modelTool.GetComponent<BoxCollider>().enabled = false;
            modelTool.gameObject.GetComponent<Rigidbody>().useGravity = false;
            modelTool.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //Component.Destroy();
            rightHand.SetActive(false);
            return;
        }
        
        
        modelTool = Instantiate(tool.model);
        modelTool.transform.SetParent(transform);
        modelTool.transform.position = transform.position;
        modelTool.transform.rotation = transform.rotation;
        if (modelTool.GetComponent<BoxCollider>().enabled) modelTool.GetComponent<BoxCollider>().enabled = false;
        modelTool.gameObject.GetComponent<Rigidbody>().useGravity = false;
        modelTool.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    
        if (tool is ToolScriptableObject)
        {
            if ((tool as ToolScriptableObject).isTwoHanded)
            {
                rightHand.SetActive(true);
            }
            else
            {
                rightHand.SetActive(false);
            }
        }
        else
        {
            rightHand.SetActive(false);
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
