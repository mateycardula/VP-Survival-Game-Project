using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemsHolder : MonoBehaviour
{
    [SerializeField] public GameObject[] items = new GameObject[20];
    [SerializeField] public GameObject[] selectedBackground = new GameObject[20];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectItem(int id, int deselectId = -1)
    {
        if (deselectId != -1)
        {
            selectedBackground[deselectId].SetActive(false);
        }

        if (id == -1 && deselectId == -1)
        {
            foreach (GameObject background in selectedBackground)
            {
                background.SetActive(false);
            }
        }
        else
        {
            selectedBackground[id].SetActive(true);
        }
    }
}
