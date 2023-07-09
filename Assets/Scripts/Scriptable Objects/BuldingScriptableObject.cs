using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Building")]
public class BuldingScriptableObject : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] public GameObject building, canBuildIndicator;
    [SerializeField] public ItemScriptableObject [] buildingMaterials;
    [SerializeField] public int[] countOfMaterials;
    public bool isWall, isFloor;
    
}
