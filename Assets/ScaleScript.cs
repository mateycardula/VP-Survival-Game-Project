using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class ScaleScript : MonoBehaviour
{
    private int x;

    private int y;

    private void Awake()
    {
        transform.gameObject.transform.localScale = new Vector3((float)Screen.width/1920.0f, (float)Screen.height/1080.0f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
