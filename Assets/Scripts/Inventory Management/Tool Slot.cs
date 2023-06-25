using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolSlot
{
    public bool isEquipped;
    public string toolName;
    public ToolScriptableObject tool;

    public ToolSlot(string _name, ToolScriptableObject _tool, bool _isEquipped = false)
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
