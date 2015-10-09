using UnityEngine;
using System.Collections;

public class ScrollBarTransform : MonoBehaviour 
{
    private RectTransform m_rectTransform = null;
    private int height = 0;
    private int xOffset = 0;
    private int width = 20;
    private int baseOffset = 10;
    private bool allSet = false;

	// Use this for initialization
	void Start () 
    {
        m_rectTransform = GetComponent<RectTransform>();
        height = (int)((float)Screen.height * 0.9f);	    xOffset = (int)(((float)Screen.width * 0.5f) - ((float)width * 0.5f)) - baseOffset;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!allSet) {
            m_rectTransform.localPosition = new Vector3(xOffset, 0, 0);
            m_rectTransform.sizeDelta = new Vector2(width, height);
            allSet = true;
        }
	}
}
