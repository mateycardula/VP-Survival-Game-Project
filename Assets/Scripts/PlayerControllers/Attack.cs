using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Attack : MonoBehaviour
{
    [SerializeField] private HITController checkCollider;
    [SerializeField] private Tool attackingTool;
    private Collider collider;

    public float time = 2;

    public float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ALO");
        StartCoroutine(HitDestroyableObject());
        Debug.Log("ALO");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0) && timer<0)
        {
            StartCoroutine(HitDestroyableObject());
            timer = time;
        }
    }

    private void DropLoot(Collider collider, Destroyable destroyableData)
    {
        GameObject [] lootItems = new GameObject[destroyableData.dso.itemToDropCount];
        for (int i = 0; i < destroyableData.dso.itemToDropCount; i++)
        {
            lootItems[i] = destroyableData.dso.itemToDrop;
            lootItems[i].transform.position = collider.transform.position + Vector3.up * Random.Range(2, 3);
            Instantiate(lootItems[i]);
            
           
        }
    }
    IEnumerator HitDestroyableObject()
    {
        Debug.Log("Ej");
        collider = checkCollider.collision();
        if (collider.tag == "Destroyable")
        {
            Destroyable destroyableData = collider.GetComponent<Destroyable>();
            if (destroyableData.dso.WeaknessList.Contains(attackingTool.toolItem as ToolScriptableObject))
            {
                yield return new WaitForSeconds(attackingTool.toolItem.OnLeftClickAnimationClip.length);
                destroyableData.health -= (attackingTool.toolItem as ToolScriptableObject).damage * 2;
            }
            if(destroyableData.health<=0)
            {
                Vector3 treePos = collider.transform.position;
                Collider colliderCP = collider;
                //StartCoroutine(waitEnumerator(colliderCP, destroyableData, 3, destroyableData.dso.itemToDrop));
                DropLoot(collider, destroyableData);
                Destroy(collider.gameObject);
                if (destroyableData.dso.brokenObject != null)
                {
                    GameObject brokenObjectToSpawn;
                    brokenObjectToSpawn = Instantiate(destroyableData.dso.brokenObject);
                    brokenObjectToSpawn.transform.position = treePos;
                } 
            }
        }
      
    }

    public void ResetSpamTimer(float setTime)
    {
        timer = setTime/2.5f;
        time = timer;
    }
}
