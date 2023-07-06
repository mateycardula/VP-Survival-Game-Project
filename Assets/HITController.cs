using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class HITController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryManager;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject UICollectIndicator;

    private ToolInventory toolInventoryManager;
    private ItemInventory itemInventoryManager;
    
    //Hover Effect Variables
    [SerializeField] private Behaviour hoveredItemEffect;
    private GameObject hitItem;
    // Start is called before the first frame update
    void Start()
    {
        toolInventoryManager = inventoryManager.GetComponent<ToolInventory>();
        itemInventoryManager = inventoryManager.GetComponent<ItemInventory>();
        UICollectIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 FrontObjectCheck = cameraObj.transform.TransformDirection(Vector3.forward) * 3f;
        if(Physics.Raycast(cameraObj.transform.position,cameraObj.transform.forward, out hit, 3f))
        {
            Debug.DrawRay(cameraObj.transform.position, FrontObjectCheck, Color.green);
            if (hit.collider.CompareTag("Item"))
            {
                if (hitItem != null && hitItem != hit.collider)
                {
                    hoveredItemEffect.enabled = false;
                }
                
                hitItem = hit.collider.gameObject;
                hoveredItemEffect = (Behaviour)hitItem.GetComponent("Halo");
                hoveredItemEffect.enabled = true;
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ItemScriptableObject hitItemISO = hitItem.GetComponent<ISOHolder>().iso;
                    if (hitItemISO is ToolScriptableObject)
                    {
                        toolInventoryManager.CollectItem(hitItemISO);
                        GameObject.Destroy(hitItem);
                    }
                    else
                    {
                        itemInventoryManager.CollectItem(hitItemISO);
                        GameObject.Destroy(hitItem);
                    }
                }
            }
            
        }
        else
        {
            if (hitItem != null)
            {
                hoveredItemEffect.enabled = false;
                hitItem = null;
            }
            UICollectIndicator.SetActive(false);
            Debug.DrawRay(cameraObj.transform.position, FrontObjectCheck, Color.red);
        }
    }
}
