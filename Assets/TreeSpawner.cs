using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] trees = new GameObject[4];
    public int treeCount;
    private GameObject [] treesArray; 
    private void Awake()
    {
        treesArray = new GameObject[treeCount];
        for (int i = 0; i < treeCount; i++)
        {
            treesArray[i] = Instantiate(trees[Random.Range(0, 4)]);
            treesArray[i].transform.position = new Vector3(Random.Range(30, 970), 70, Random.Range(30, 970));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }
}
