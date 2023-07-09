using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Building")]
public class BuldingScriptableObject : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] public GameObject building, canBuildIndicator, cannotBuildIndicator;
    [SerializeField] public List<ItemScriptableObject> buildingMaterials;
    public bool isWall, isFloor;
    // private ItemScriptableObject buildingMaterial;
    // private int count;
}
