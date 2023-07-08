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
    [SerializeField] private ItemInventory itemInventoryScript;
    
    private PlayerVitality vitalityController;
    private Collider collider;

    public bool changed = false;
    public float time;
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        vitalityController = GetComponent<PlayerVitality>();
        Debug.Log("ALO");
        //StartCoroutine(HitDestroyableObject());
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
        //Proverka dali e napad
        changed = false;
        if(attackingTool.toolItem is ToolScriptableObject)
        {
            if (collider.tag == "Destroyable")
            {
                float damage = (attackingTool.toolItem as ToolScriptableObject).damage;
                Destroyable destroyableData = collider.GetComponent<Destroyable>();

                if (destroyableData.dso.WeaknessList.Contains(attackingTool.toolItem as ToolScriptableObject))
                    damage *= 2.0f;
                
                // if (destroyableData.dso.WeaknessList.Contains(attackingTool.toolItem as ToolScriptableObject))
                // {
                //     yield return new WaitForSeconds(attackingTool.toolItem.OnLeftClickAnimationClip.length);
                //     if (changed) yield break;
                //     destroyableData.health -= (attackingTool.toolItem as ToolScriptableObject).damage * 2;
                // }
                
                yield return new WaitForSeconds(attackingTool.toolItem.OnLeftClickAnimationClip.length);
                if (changed) yield break;
                destroyableData.health -= (attackingTool.toolItem as ToolScriptableObject).damage * 2;

                if (destroyableData.health <= 0)
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

        if (attackingTool.toolItem is ConsumableScriptableObject)
        {
            int consumeID = itemInventoryScript.equippedId;
            yield return new WaitForSeconds(attackingTool.toolItem.OnLeftClickAnimationClip.length);
            if(changed) yield break;
            vitalityController.setHunger((attackingTool.toolItem as ConsumableScriptableObject).hungerChange,
                (attackingTool.toolItem as ConsumableScriptableObject).thirstChange);
            itemInventoryScript.Consume(consumeID);
        }
      
    }

    public void ResetSpamTimer(float setTimer)
    {
        timer = 0;
        time = setTimer;
    }
}
