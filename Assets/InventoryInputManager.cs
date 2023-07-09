using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryInputManager : MonoBehaviour
{
    [SerializeField] private ToolInventory toolInventory;
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private GameObject [] equippedToolInterface = new GameObject[3];
    [SerializeField] private Tool toolHolder;
    [SerializeField] private Attack attackScript;



    [SerializeField] private BuildSystem buildSystemScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(toolInventory.GetEquippedToolID() != 0)
                SetEquipped(0);
            else
                SetEquipped(-1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(toolInventory.GetEquippedToolID() != 1)
                SetEquipped(1);
            else
                SetEquipped(-1);
        }
    }

    //TODO: Add bool third when ItemInventory is implemented
    public void SetEquipped(int id)
    {
        //GetComponent<Animator>().Play("Idle State", 1);
        toolInventory.slots[0].isEquipped = false;
        toolInventory.slots[1].isEquipped = false;
        equippedToolInterface[0].SetActive(false);
        equippedToolInterface[1].SetActive(false);
        equippedToolInterface[2].SetActive(false);
        attackScript.changed = true;
        //attackScript.ResetSpamTimer();
        
        buildSystemScript.setBSO(null);

        if (id == 3)
        {
            toolHolder.UpdateToolModel(itemInventory.slots[itemInventory.selectedId].tool);
            
            return;
        }
        
        if(id != -1)
        {
            
            toolInventory.slots[id].isEquipped = true;
            if(toolInventory.slots[id].tool !=null)
            {
                if((toolInventory.slots[id].tool as ToolScriptableObject).isBuildingTool) equippedToolInterface[2].SetActive(true);
                equippedToolInterface[id].SetActive(true);
            }
            else equippedToolInterface[id].SetActive(false);
            
            
            toolHolder.UpdateToolModel(toolInventory.slots[id].tool);
            
        }
        else
        {
            toolHolder.UpdateToolModel();
        }

        itemInventory.equippedId = -1;
    }
}
