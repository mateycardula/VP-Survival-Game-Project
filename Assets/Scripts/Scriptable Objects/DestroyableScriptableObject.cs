using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Destroyable Object")]
public class DestroyableScriptableObject : ScriptableObject
{
    public float health;
    public GameObject brokenObject;
    public List<ToolScriptableObject> WeaknessList;
    public List<ToolScriptableObject> StrenghtList;
    public GameObject itemToDrop;
    public int itemToDropCount;
}
