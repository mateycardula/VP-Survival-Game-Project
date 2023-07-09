using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class ToolInventory : Inventory
{
    private bool isOpenned = false;
    private ToolSlot prevEquipped;
    [SerializeField] private GameObject[] toolSpritesUI = new GameObject[2];
    [SerializeField] private Tool toolHolder;
    [SerializeField] private GameObject toolHolderEmptyObject;

    [SerializeField] private GameObject inventoryPanel, buildingPanel;
    
   
    // Start is called before the first frame update
    
    void Start()
    {
        itemCount = 0;
        inventoryCapacity = 2;
        slots = new ToolSlot[2];
        slots[0] = new ToolSlot();
        slots[1] = new ToolSlot();
        invManager.SetEquipped(-1);
        UpdateInterface();
    }
    

// Update is called once per frame
    void Update()
    {
        int selectedId = GetEquippedToolID();
        bool isBuildMode = (GetEquippedToolID()!= -1 && (slots[GetEquippedToolID()].tool as ToolScriptableObject).isBuildingTool);
        
        if(!isBuildMode) buildingPanel.SetActive(false);
        
        if (Input.GetKeyDown(KeyCode.B) && isBuildMode)
        {
            isOpenned = !isOpenned;
            if (isOpenned)
            {
                inventoryPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                buildingPanel.SetActive(true);
                //selectedId = -1;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                buildingPanel.SetActive(false);
            }
        }
        
        
        
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

    public override ItemScriptableObject DropItem(int itemSlotID, bool shouldConsume = false)
    {
        //TODO: Kreiraj instanca od modelot pred covekot i aktiviraj useGravity od rigidBody
        ToolScriptableObject dropped = slots[itemSlotID].tool as ToolScriptableObject;
        GameObject toolToDrop = dropped.model;
        slots[itemSlotID] = new ToolSlot();
        toolToDrop.transform.position = toolHolderEmptyObject.transform.position;
        Instantiate(toolToDrop);
        
        toolToDrop.GetComponent<Rigidbody>().AddForce(Vector3.up*5, ForceMode.Impulse);
        itemCount--;
        invManager.SetEquipped(-1);
        UpdateInterface();
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

    public int GetEquippedToolID()
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
        slots[id] = new ToolSlot(iso as ToolScriptableObject);
        itemCount++; 
    }

    void DropAndAddTool(ItemScriptableObject iso, int id)
    {
        DropItem(id);
        slots[id] = new ToolSlot(iso as ToolScriptableObject);
        itemCount++;     
        UpdateInterface();
        
    }
    
    protected override void UpdateInterface(int id = -1)
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if(slots[i].tool == null) toolSpritesUI[i].SetActive(false);
            else
            {
                toolSpritesUI[i].GetComponent<Image>().sprite = slots[i].tool.inventorySprite;
                toolSpritesUI[i].SetActive(true);
            }
        }
    }
    
   
}
