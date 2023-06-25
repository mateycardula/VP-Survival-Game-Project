using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class  Inventory : MonoBehaviour
{
    public int inventoryCapacity, itemCount;

    public abstract ItemScriptableObject CollectItem(ItemScriptableObject iso);
    protected abstract ItemScriptableObject DropItem(int itemSlotID);
    
    
    
    // Start is called before the first frame update

}
