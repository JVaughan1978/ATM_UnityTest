using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollBarHandleHeight : MonoBehaviour 
{
    private RectTransform m_rectTransform = null;
    private int height = 0;
    private bool allSet = false;

	// Use this for initialization
	void Start () 
    {
        m_rectTransform = GetComponent<RectTransform>();
        if(Screen.width > Screen.height)
        {
            height = (int)((float)Screen.width * 0.1f);
        }
        else 
        {
            height = (int)((float)Screen.height * 0.1f);
        }        
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
	    if (!allSet)
        {
            m_rectTransform.sizeDelta = new Vector3(m_rectTransform.sizeDelta.x, height);
            allSet = true;
        }
	}
}
