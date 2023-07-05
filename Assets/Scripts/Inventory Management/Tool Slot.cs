using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolSlot
{
    [SerializeField] private Tool toolHolder; 
    public bool isEquipped;
    public ItemScriptableObject tool;
    public int count;
    public ToolSlot(ItemScriptableObject _tool, int _count = 1, bool _isEquipped = false)
    {
        
        isEquipped = _isEquipped;
        tool = _tool;
        count = _count;
    }

    public ToolSlot()
    {
        isEquipped = false;
        tool = null;
        count = 0;
    }

}
