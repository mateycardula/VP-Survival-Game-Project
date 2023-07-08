using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVitality : MonoBehaviour
{

    [SerializeField] private GameObject hungerIndicator, thirstIndicator;
    private RectTransform hungerIndicatorRect, thirstIndicatorRect;
    // Start is called before the first frame update
    void Start()
    {
        thirstIndicatorRect = thirstIndicator.GetComponent<RectTransform>();
        hungerIndicatorRect = hungerIndicator.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHunger(float h, float t)
    {
        hungerIndicatorRect.sizeDelta += new Vector2(0, h);
        thirstIndicatorRect.sizeDelta += new Vector2(0, t);
        
        if(hungerIndicatorRect.sizeDelta.y>100) hungerIndicatorRect.sizeDelta = new Vector2(100, 100);
        if(hungerIndicatorRect.sizeDelta.y<0) hungerIndicatorRect.sizeDelta = new Vector2(100, 0);

        if (thirstIndicatorRect.sizeDelta.y > 100) thirstIndicatorRect.sizeDelta = new Vector2(100, 100);
        if (thirstIndicatorRect.sizeDelta.y < 0) thirstIndicatorRect.sizeDelta = new Vector2(100, 0);
    }
}
