using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryInputManager : MonoBehaviour
{
    [SerializeField] private ToolInventory toolInventory;

    [SerializeField] private Tool toolHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEquipped(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetEquipped(1);
        }
    }

    //TODO: Add bool third when ItemInventory is implemented
    public void SetEquipped(int id)
    {
        toolInventory.slots[0].isEquipped = false;
        toolInventory.slots[1].isEquipped = false;
        
        if(id != -1)
        toolInventory.slots[id].isEquipped = true;
        
        if (id == 0)
        {
            toolHolder.UpdateToolModel(toolInventory.slots[0].tool);
        }
        
        if (id == 1)
        {
            toolHolder.UpdateToolModel(toolInventory.slots[1].tool);
        }

        if (id == -1)
        {
            toolHolder.UpdateToolModel();
        }   
    }
}
