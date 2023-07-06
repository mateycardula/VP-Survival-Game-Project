using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Item")]
public class ItemScriptableObject : ScriptableObject
{
    public int stack;
    public string itemName;
    public string description;
    public GameObject model;
    public Sprite inventorySprite;
    public AnimationClip OnLeftClickAnimationClip; 
}
