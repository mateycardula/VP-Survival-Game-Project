using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Item")] //Ovozmozuva kreiranje na instanci od klasata preku interface na Unity
public class ItemScriptableObject : ScriptableObject
{
    public int stack; //Maksimalen broj na items vo edno inventory pole
    public string itemName; //Ime na item
    public string description; //Opis na item
    public GameObject model; //Gameobject: 3D model i komponenti potrebni za pravilna rabota
    public Sprite inventorySprite; //SLika za inventory
    public AnimationClip OnLeftClickAnimationClip; //Animacija koja se stratuva pri pritiskanje na lev klik
}
