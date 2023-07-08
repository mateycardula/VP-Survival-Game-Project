using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemsHolder : MonoBehaviour
{
    [SerializeField] public GameObject[] items = new GameObject[20];
    [SerializeField] public GameObject[] selectedBackground = new GameObject[20];


    [SerializeField] public ItemInventory itemInventory;
    [SerializeField] private GameObject itemName, itemDescription;

    private Text nameTxt, descriptionTxt; 
    // Start is called before the first frame update
    void Start()
    {
        nameTxt = itemName.GetComponent<Text>();
        descriptionTxt = itemDescription.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectItem(int id, int deselectId = -1)
    {
        if (deselectId != -1)
        {
            nameTxt.text = " ";
            descriptionTxt.text = " ";
            selectedBackground[deselectId].SetActive(false);
        }

        if (id == -1 && deselectId == -1)
        {
            nameTxt.text = " ";
            descriptionTxt.text = " ";
            foreach (GameObject background in selectedBackground)
            {
                background.SetActive(false);
            }
        }
        else
        {
            selectedBackground[id].SetActive(true);
            nameTxt.text = itemInventory.slots[id].tool.itemName;
            descriptionTxt.text = itemInventory.slots[id].tool.description;
        }
    }
}
