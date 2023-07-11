using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Create Item/Consumable")]
public class ConsumableScriptableObject : ItemScriptableObject //Nasleduva od ItemScriptableObject
{
    public float hungerChange; //Promena na glad
    public float thirstChange; //Promena na zhed
}
