using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int health;
    public DestroyableScriptableObject dso;
    // Start is called before the first frame update
    void Start()
    {
        health = dso.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
