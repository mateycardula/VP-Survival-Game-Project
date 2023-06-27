using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolSlot
{
    [SerializeField] private Tool toolHolder; 
    public bool isEquipped;
    public string toolName;
    public ItemScriptableObject tool;

    public ToolSlot(string _name, ItemScriptableObject _tool, bool _isEquipped = false)
    {
        
        isEquipped = _isEquipped;
        toolName = _name;
        tool = _tool;
    }

    public ToolSlot()
    {
        isEquipped = false;
        toolName = "Empty";
        tool = null;
    }

}
