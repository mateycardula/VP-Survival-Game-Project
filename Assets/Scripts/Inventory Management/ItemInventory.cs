using System.Collections;
using System.Collections.Generic;
using GLTF.Schema;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ItemInventory : Inventory
{
    private bool isOpenned = false;
    [SerializeField] private InventoryItemsHolder ItemSlotsUI;
    
    [SerializeField] private GameObject inventoryPanel, buildingPanel;
    [SerializeField] private InventoryItemsHolder UIitemsHolder;
    [SerializeField] private GameObject toolHolderEmptyObject; 
    
    public int selectedId = -1, equippedId = -1;
    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(false);
        inventoryCapacity = 20;
        slots = new ToolSlot[inventoryCapacity];
        slotsInit();
        UpdateInterface();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpenned = !isOpenned;
            if (isOpenned)
            {
                buildingPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                inventoryPanel.SetActive(true);
                UIitemsHolder.selectItem(-1, -1);
                SetSelectedID(-1);
                //selectedId = -1;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                inventoryPanel.SetActive(false);
            }
        }
    }

    public override ItemScriptableObject CollectItem(ItemScriptableObject iso)
    {
       int id = FindFirstNonFullISOslot(iso); //go barame prviot slot od itemot koj sakame da se equip-ne, a da ne e negoviot count = stack
        bool collected = false;
        if (id == -1) //Dokolku ne sme nasle slot koj ne go dostignal ogranicuvanjeto, itemot se mesti na prvoto slobodno mesto
        {
            for (int i = 0; i < inventoryCapacity; i++)
            {
                if (slots[i].tool == null)
                {
                    slots[i] = new ToolSlot(iso);
                    UpdateInterface(i);
                    collected = true;
                    break;
                }
            }
            if (!collected)
            {
                //TODO: Announce full inventory
            }
         }
        else //Dokolku sme nasle 
        {
            int count = slots[id].count;
            slots[id] = new ToolSlot(iso,count+1); //Na mestoto od stariot slot cuvame nov objekt so count++
            UpdateInterface(id);
        }
        return iso;
    }

    public override ItemScriptableObject DropItem(int i, bool shouldConsume)
    {
        if(selectedId != -1)
        {
            int count = slots[selectedId].count;
            ItemScriptableObject iso = slots[selectedId].tool;
            if (count == 1)
            {
                slots[selectedId] = new ToolSlot();
            }
            else
            {
                slots[selectedId] = new ToolSlot(iso, count - 1);
            }
            
            
            
            UpdateInterface(selectedId);
        }

        return null;
    }

    public void Drop()
    {
        if(selectedId != -1)
        {
            int count = slots[selectedId].count;
            ItemScriptableObject iso = slots[selectedId].tool;
            ItemScriptableObject dropped = slots[selectedId].tool;
            GameObject toolToDrop = dropped.model;
            toolToDrop.transform.position = toolHolderEmptyObject.transform.position + Vector3.up*2;
            // toolToDrop.transform.position += Vector3.forward*2;
            Instantiate(toolToDrop);
            toolToDrop.GetComponent<Rigidbody>().AddForce(Vector3.forward*5, ForceMode.Impulse);
            
            if (count == 1)
            {
                
                slots[selectedId] = new ToolSlot();

                if (selectedId == equippedId)
                {
                    invManager.SetEquipped(-1);
                }
                UIitemsHolder.selectItem(-1, -1);
                //selectedId = -1;
                SetSelectedID(-1);
            }
            else
            {
                slots[selectedId] = new ToolSlot(iso, count - 1);
            }
            UpdateInterface(selectedId);
        }
    }

    public void Consume(int consumeID)
    {
        ItemScriptableObject iso = slots[consumeID].tool;
        
        int count = slots[consumeID].count;
        
        if (count == 1)
        {
                
            slots[consumeID] = new ToolSlot();
            if(equippedId!=-1)
            invManager.SetEquipped(-1);
            //selectedId = -1;
            SetSelectedID(-1);
        }
        else
        {
            slots[consumeID] = new ToolSlot(iso, count - 1);
        }
        
        UpdateInterface(consumeID);
        
        
        
    }
    
    

    public void DropAll()
    {
        if (selectedId != -1)
        {
            int dropItemsCount = slots[selectedId].count;
            for (int i = 0; i < dropItemsCount; i++)
            {
                Drop();
            }
        }
    }

    private int FindFirstNonFullISOslot(ItemScriptableObject iso)
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            //if(slots[i].tool == null) continue;
        
            if (slots[i].tool == iso && slots[i].count < iso.stack) return i;
        }
        return -1;
    }

    protected override void UpdateInterface(int id = -1)
    {
        if(id == -1)
        {
            for (int i = 0; i < inventoryCapacity; i++)
            {
                Debug.Log(slots[i].count);
                if (slots[i].tool != null)
                {
                    ItemSlotsUI.items[i].SetActive(true);
                    ItemSlotsUI.items[i].GetComponentInChildren<Text>().text = slots[i].count.ToString();
                    ItemSlotsUI.items[i].GetComponent<Image>().sprite = slots[i].tool.inventorySprite;
                }
                else
                {
                    ItemSlotsUI.items[i].SetActive(false);
                    Debug.Log(i);
                }
            }
        }
        else
        {
            if (slots[id].tool != null)
            {
                ItemSlotsUI.items[id].SetActive(true);
                ItemSlotsUI.items[id].GetComponentInChildren<Text>().text = slots[id].count.ToString();
                ItemSlotsUI.items[id].GetComponent<Image>().sprite = slots[id].tool.inventorySprite;
            }
            else
            {
                ItemSlotsUI.items[id].SetActive(false);
            }
        }
    }
    
    

    private void slotsInit()
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            slots[i] = new ToolSlot();
        }
    }
    
    public void selectItem(int id)
    {
        Debug.Log("SelectItem");
        if (selectedId != -1)
        {
            UIitemsHolder.selectItem(id, selectedId);
        }
        UIitemsHolder.selectItem(id);
        //selectedId = id;
        SetSelectedID(id);
    }

    public void Equip()
    {
        if (selectedId != -1)
        {
            invManager.SetEquipped(3);
            equippedId = selectedId;
        }
    }

    public void SetSelectedID(int id)
    {
        selectedId = id;
    }

    public int GetItemCount(ItemScriptableObject iso)
    {
        int itemCount = 0;
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if(slots[i].tool == null) continue;
            
            if (slots[i].tool == iso)
            {
                itemCount += slots[i].count;
            }
        }
        return itemCount;
    }

    public void ConsumeMultiple(ItemScriptableObject iso, int count)
    {
        int consumed = 0; //brojac za da ne se nadmine potrebniot broj na materijali
        
        for (int i = 0; i < inventoryCapacity; i++) //se iterira niz sekoj slot na inventory
        {
            if (slots[i].tool == iso) //koga kje najde slot koj go cuva potrebniot objekt
            {
                int limit = slots[i].count;
                for (int k = 0; k < limit; k++)
                {
                    Consume(i); //za negovoto id povikuva consume(), metodot koj se povikuva pri jadenje na hrana
                    consumed++;
                    if(consumed == count) return;
                }
            }
        }
        //Debug.Log("Consume Multiple");
    }
}
