using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create enemy", fileName = "DefaultEnemyObject")]
public class EnemyScriptableObject : ScriptableObject
{
    public int health;
    public int damage;
    public GameObject Model;
    public bool isAggresive;
}
