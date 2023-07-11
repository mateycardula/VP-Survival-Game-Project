using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Item/Tool")]
public class ToolScriptableObject : ItemScriptableObject //Nasleduva od ItemScriptableObject
{
    //public float attackSpeed;
    public float damage; //Shteta koja ja nanesuvaat
    public bool isTwoHanded; //Dali se drzi so dve race
    public bool isBuildingTool; //Dali so alatkata moze da se gradi
}
