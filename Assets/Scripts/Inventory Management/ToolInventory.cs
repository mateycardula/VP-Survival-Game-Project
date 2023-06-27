using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ToolInventory : Inventory
{

    
    private ToolSlot prevEquipped;
    [SerializeField] private GameObject primaryToolUI, secondaryToolUI;
    
    [SerializeField] private Tool toolHolder;
    [SerializeField] private InventoryInputManager invManager;

    // Start is called before the first frame update
    
    void Start()
    {
       
        itemCount = 0;
        inventoryCapacity = 2;
        slots = new ToolSlot[2];
        slots[0] = new ToolSlot();
        slots[1] = new ToolSlot();
        UpdateInterface();
    }
    

// Update is called once per frame
    void Update()
    {
        // if(GetEquippedToolID() != -1)
        // {
        //     if (!prevEquipped.Equals(slots[GetEquippedToolID()]))
        //     {
        //         toolHolder.UpdateToolModel(slots[GetEquippedToolID()].tool);
        //     }
        //
        //     prevEquipped = slots[GetEquippedToolID()];
        // }
        // else
        // {
        //     prevEquipped = new ToolSlot();
        // }
        
    }

    public override ItemScriptableObject CollectItem(ItemScriptableObject iso)
    {
        
        bool collected = false;
        int equippedToolID = GetEquippedToolID();

        //Ako e poln tool inventory proveri dali e nekoj od tool-ovite equipnat
        if (itemCount == inventoryCapacity)
        {
            if (equippedToolID != -1)
            {
                DropAndAddTool(iso, equippedToolID);
            }
            else DropAndAddTool(iso, 0);
            collected = true;
        }


        
        if (!collected)
        {
            int slotID = 0;
            foreach (ToolSlot toolSlot in slots)
            {
                if (toolSlot.tool == null)
                {
                    if (itemCount == 0) equippedToolID = 0;
                    collected = true;
                    
                    if (equippedToolID != -1)
                    {
                        AddTool(iso, slotID);
                    }
                    else
                    {
                        AddTool(iso, slotID);
                    }
                    break;
                    
                }
                slotID++;
            }
        }
        
        invManager.SetEquipped(equippedToolID);
        UpdateInterface();
        return iso;
    }

    protected override ItemScriptableObject DropItem(int itemSlotID)
    {
        //TODO: Kreiraj instanca od modelot pred covekot i aktiviraj useGravity od rigidBody
        ToolScriptableObject dropped = slots[itemSlotID].tool as ToolScriptableObject; 
        slots[itemSlotID] = new ToolSlot();
        itemCount--;
        return dropped;
    }

    private ToolScriptableObject GetEquippedTool()
    {
        foreach ( ToolSlot toolSlot in slots)
        {
            if (toolSlot.isEquipped == true) return toolSlot.tool as ToolScriptableObject;
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

    public void SetEquipped(int ID)
    {
        int id = ID - 1;
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (id == i) slots[i].isEquipped = true;
            else slots[i].isEquipped = false;
        }
        toolHolder.UpdateToolModel(slots[id].tool);
    }

    void AddTool(ItemScriptableObject iso, int id)
    {
        slots[id] = new ToolSlot(iso.name, iso as ToolScriptableObject);
        itemCount++; 
    }

    void DropAndAddTool(ItemScriptableObject iso, int id)
    {
        DropItem(id);
        slots[id] = new ToolSlot(iso.name, iso as ToolScriptableObject);
        itemCount++;      
    }
    
    void UpdateInterface()
    {
        primaryToolUI.GetComponent<Text>().text = "Empty 1";
        secondaryToolUI.GetComponent<Text>().text= "Empty 2";
        if(slots[0].tool != null)
        primaryToolUI.GetComponent<Text>().text = slots[0].tool.name;
        if(slots[1].tool != null)
        secondaryToolUI.GetComponent<Text>().text = slots[1].tool.name;
    }
}
