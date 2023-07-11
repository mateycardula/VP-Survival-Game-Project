using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float health;
    public DestroyableScriptableObject dso;
    void Start()
    {
        health = dso.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float h, ToolScriptableObject tso = null)
    {
        if(tso!=null)
        {
            if (dso.WeaknessList.Contains(tso)) h *= 2; //Dokolku objektot e slab na itemot koj go udira (pr. drvo na sekira)
            if (dso.StrenghtList.Contains(tso)) h /= 3; 
        }
        health += h;
        if(health<=0) DropLoot();
    }

    public void DropLoot()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.SetActive(false);
        
        if (dso.brokenObject != null)
        {
            SpawnBrokenObject(position);
        }
        
        GameObject [] lootItems = new GameObject[dso.itemToDropCount];
        for (int i = 0; i < dso.itemToDropCount; i++)
        {
            lootItems[i] = dso.itemsToDrop[Random.Range(0, dso.itemsToDrop.Length)];
            lootItems[i].transform.position = transform.position + Vector3.up * Random.Range(2, 3);
            Instantiate(lootItems[i]);
        }
        
        Destroy(gameObject);
    }

    private void SpawnBrokenObject(Vector3 position)
    {
        GameObject brokenObjectToSpawn;
        brokenObjectToSpawn = Instantiate(dso.brokenObject);
        brokenObjectToSpawn.transform.position = position;
    }
}
