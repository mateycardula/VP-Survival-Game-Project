using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolSlot
{
    public bool isEquipped; 
    public ItemScriptableObject tool; //ItemScriptableObject koj go cuvame vo poleto
    public int count; //Broj od objektot vo edno pole od inventory-to
    public ToolSlot(ItemScriptableObject _tool, int _count = 1, bool _isEquipped = false)
    {
        
        isEquipped = _isEquipped;
        tool = _tool;
        count = _count;
    }

    public ToolSlot() //Go koristime za instanciranje na prazno pole (pr. posle drop ili consume)
    {
        isEquipped = false;
        tool = null;
        count = 0;
    }

}
