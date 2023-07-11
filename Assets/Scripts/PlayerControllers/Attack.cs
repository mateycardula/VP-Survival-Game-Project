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
    IEnumerator HitDestroyableObject()
    {
        Debug.Log("Ej");
        collider = checkCollider.collision();
        changed = false;
        
        //Proverka dali e napad
        if(attackingTool.toolItem is ToolScriptableObject)
        {
            if (collider.tag == "Destroyable")
            {
                float damage = (attackingTool.toolItem as ToolScriptableObject).damage;
                Destroyable destroyableData = collider.GetComponent<Destroyable>();
                
                yield return new WaitForSeconds(attackingTool.toolItem.OnLeftClickAnimationClip.length);
                if (changed) yield break;
                destroyableData.SetHealth(-damage, attackingTool.toolItem as ToolScriptableObject);
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
