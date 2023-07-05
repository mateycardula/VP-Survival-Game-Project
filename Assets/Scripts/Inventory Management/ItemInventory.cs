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

    [SerializeField] private GameObject inventoryPanel;
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
                Cursor.lockState = CursorLockMode.Confined;
                inventoryPanel.SetActive(true);
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
        Debug.Log(iso.name);
        int id = FindFirstNonFullISOslot(iso as ResourceScriptableObject);
        bool collected = false;
        if (id == -1)
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
        else
        {
            int count = slots[id].count;
            // Debug.Log(count);
            slots[id] = new ToolSlot(iso,count+1);
            UpdateInterface(id);
        }
        return iso;
    }

    protected override ItemScriptableObject DropItem(int itemSlotID)
    {
        throw new System.NotImplementedException();
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
            ItemSlotsUI.items[id].SetActive(true);
            ItemSlotsUI.items[id].GetComponentInChildren<Text>().text = slots[id].count.ToString();
            ItemSlotsUI.items[id].GetComponent<Image>().sprite = slots[id].tool.inventorySprite;
        }
    }

    private void slotsInit()
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            slots[i] = new ToolSlot();
        }
    }
}
