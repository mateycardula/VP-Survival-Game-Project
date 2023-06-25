using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class ToolInventory : Inventory
{

    public ToolSlot[] slots;
    private ToolSlot prevEquipped;
    [SerializeField] private Tool toolHolder;

    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
        inventoryCapacity = 2;
        slots = new ToolSlot[2];
        slots[0] = new ToolSlot();
        slots[1] = new ToolSlot();
        slots[0].isEquipped = true;
        SetEquipped(0);
        prevEquipped = slots[0];
    }
    

// Update is called once per frame
    void Update()
    {
        if(GetEquippedToolID() != -1)
        {
            if (!prevEquipped.Equals(slots[GetEquippedToolID()]))
            {
                toolHolder.UpdateToolModel(slots[GetEquippedToolID()].tool);
            }
        
            prevEquipped = slots[GetEquippedToolID()];
        }
        else
        {
            prevEquipped = new ToolSlot();
        }
        
    }

    public override ItemScriptableObject CollectItem(ItemScriptableObject iso)
    {
        int equippedToolID = GetEquippedToolID();
        
        //Ako e poln tool inventory proveri dali e nekoj od tool-ovite equipnat
        if (itemCount == inventoryCapacity)
        {
          
            if (equippedToolID != -1)
            {
                DropItem(equippedToolID);
                slots[equippedToolID] = new ToolSlot(iso.name, iso as ToolScriptableObject, true);
                itemCount++;
                return iso; 
            }
            DropItem(0);
            slots[0] = new ToolSlot(iso.name, iso as ToolScriptableObject);
            itemCount++;
            return iso;
        }
        
        
        int slotID = 0;
        
        
        foreach (ToolSlot toolSlot in slots)
        {
            if (toolSlot.tool == null)
            {
                // if(equippedToolID == -1)
                // {
                //     slots[slotID] = new ToolSlot(iso.name, iso as ToolScriptableObject);
                //     return iso;
                // }
                slots[slotID] = new ToolSlot(iso.name, iso as ToolScriptableObject);
                itemCount++;
                return iso;
            }
            slotID++;
        }
        return iso;
    }

    protected override ItemScriptableObject DropItem(int itemSlotID)
    {
        //TODO: Kreiraj instanca od modelot pred covekot i aktiviraj useGravity od rigidBody
        ToolScriptableObject dropped = slots[itemSlotID].tool; 
        slots[itemSlotID] = new ToolSlot();
        itemCount--;
        return dropped;
    }

    private ToolScriptableObject GetEquippedTool()
    {
        foreach ( ToolSlot toolSlot in slots)
        {
            if (toolSlot.isEquipped == true) return toolSlot.tool;
        }
        return null;
    }

    private int GetEquippedToolID()
    {
        int toolID = 0;
        foreach ( ToolSlot toolSlot in slots)
        {
            if (toolSlot.isEquipped == true) return toolID;
            toolID++;
        }

        return -1; 
    }

    private void SetEquipped(int slotID)
    {
        slots[slotID].isEquipped = false;
    }
}
