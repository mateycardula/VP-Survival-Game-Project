using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class ResizeScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float defaultWidth = 1920.0f;
    private float defaultHeight = 1080.0f;
    private RectTransform rt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        float coefY = Screen.height / defaultHeight;
        float coefX = Screen.width / defaultWidth;
        Vector2 resize = new Vector2(coefX, coefY);
        var rtRect = rt.rect;
        transform.localScale *= resize;
    }

    // Update is called once per frame
    
}
