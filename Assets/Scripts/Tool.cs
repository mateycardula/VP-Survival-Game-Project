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

    [FormerlySerializedAs("empty")] [SerializeField] private ToolScriptableObject punch; 
    // Start is called before the first frame update
  

    void Start()
    {
        UpdateToolModel();
    }

    public void UpdateToolModel(ItemScriptableObject tool = null)
    {
        if (!modelTool.Equals(null))
            GameObject.Destroy(modelTool);
        
        if (tool == null)
        {
            modelTool = Instantiate(punch.model);
            modelTool.transform.SetParent(transform);
            modelTool.transform.position = transform.position;
            return;
        }
        
        modelTool = Instantiate(tool.model);
        modelTool.transform.SetParent(transform);
        modelTool.transform.position = transform.position;
        modelTool.transform.rotation = transform.rotation;
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
