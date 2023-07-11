using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVitality : MonoBehaviour
{
    private float time = 10;
    private float timer = 10;
    public float hunger = 100;

    [SerializeField] private UIVitality ui_vitals;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            setHunger(-3, -2);
            timer = time;
        }
    }

    public void setHunger(float h, float t)
    {
        hunger += h;
        ui_vitals.setHunger(h, t);
    }
}
