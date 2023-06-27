using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class  Inventory : MonoBehaviour
{
    [SerializeField] public ToolSlot[] slots; 
    // [SerializeField] private Tool toolHolder;
    // [SerializeField] private ToolInventory toolInv;

    protected bool firstSlot, secondSlot, thirdSlot;
    
    public int inventoryCapacity, itemCount;

    public abstract ItemScriptableObject CollectItem(ItemScriptableObject iso);
    protected abstract ItemScriptableObject DropItem(int itemSlotID);

    
    void ChangeEquipped()
    {
        
    }
    //
    // protected void SetEquipped(bool first, bool second, bool third, ItemScriptableObject iso = null)
    // {
    //     // if (first) toolInv.slots[0].isEquipped = true;
    //     // else toolInv.slots[0].isEquipped = false;
    //     //
    //     // if (second) toolInv.slots[0].isEquipped = true;
    //     // else toolInv.slots[0].isEquipped = false;
    //     //
    //     // if (third) toolInv.slots[0].isEquipped = true;
    //     // else toolInv.slots[0].isEquipped = false;
    //     //
    //     // firstSlot = first;
    //     // secondSlot = second;
    //     // thirdSlot = third;
    //     //
    //     // toolHolder.UpdateToolModel();
    // }

    protected bool isEquipped(bool first, bool second, bool third)
    {
        if (first == firstSlot && second == secondSlot && third == thirdSlot) return true;
        return false;

    }
    
    
    
    // Start is called before the first frame update

}
