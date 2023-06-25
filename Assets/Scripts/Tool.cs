using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tool : MonoBehaviour
{
    
    public GameObject modelTool;
    //public ToolScriptableObject tool;

    [SerializeField] private ToolScriptableObject empty; 
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
            modelTool = Instantiate(empty.model);
            modelTool.transform.SetParent(transform);
            modelTool.transform.position = transform.position;
            return;
        }
        
        modelTool = Instantiate(tool.model);
        modelTool.transform.SetParent(transform);
        modelTool.transform.position = transform.position;
        modelTool.transform.rotation = transform.rotation;
    }
    
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
