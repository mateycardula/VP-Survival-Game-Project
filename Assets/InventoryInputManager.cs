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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetEquipped(true, false);
            toolInventory.SetEquipped(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetEquipped(false, true);
            toolInventory.SetEquipped(2);
        }
    }

    //TODO: Add bool third when ItemInventory is implemented
    public void SetEquipped(bool first, bool second)
    {
        toolInventory.slots[0].isEquipped = first;
        toolInventory.slots[1].isEquipped = second;
        if (first)
        {
            toolHolder.UpdateToolModel(toolInventory.slots[0].tool);
        }
        
        if (second)
        {
            toolHolder.UpdateToolModel(toolInventory.slots[1].tool);
        }

        if (!(first || second))
        {
            toolHolder.UpdateToolModel();
        }
    }
}
