using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Destroyable Object")]
public class DestroyableScriptableObject : ScriptableObject
{
    public float health; 
    public GameObject brokenObject; //objekt koj treba da se instancira koja health<=0
    public List<ToolScriptableObject> WeaknessList; //Dokolku alatkata so koja se udira po objektot e vo weakness listata togas health-=tool.damage*2
    public List<ToolScriptableObject> StrenghtList; //Dokolku e vo strenght health-=tool.damage*2 
    public GameObject [] itemsToDrop; //Objekti koi gi frla kako nagradi (moze samo eden tip, no moze i povekje), se frlaat itemToDropCount objekti po slucaen izbor od listata 
    public int itemToDropCount;
}
