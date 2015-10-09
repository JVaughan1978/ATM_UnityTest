using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollRectTransform : MonoBehaviour 
{
    //width, height and offset from right hand side
    //width equals 20% of screen
    //height equals 90% of screen
    //right hand side equals 1/2 of Screen.width + 1/2 of this width + fixed offset from side
    private RectTransform m_rectTransform = null;
    private int width = 0;
    private int height = 0;
    private int baseOffset = 30; //leaving 20 pixels for scroll bar
    private int xOffset = 0;
    private bool allSet = false;

	void Start () 
    {
        m_rectTransform = GetComponent<RectTransform>();
        width = (int)((float)Screen.width * 0.2f);
        height = (int)((float)Screen.height * 0.9f);
        xOffset = (int)(((float)Screen.width * 0.5f) - ((float)width * 0.5f)) - baseOffset;	
	}	

	void Update () 
    {
        if(!allSet) 
        {
            m_rectTransform.localPosition = new Vector3( xOffset, 0, 0);
            m_rectTransform.sizeDelta = new Vector2(width, height);
            allSet = true;
        }	
	}
}
