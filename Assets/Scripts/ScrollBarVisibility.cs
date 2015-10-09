using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollBarVisibility : MonoBehaviour {

    private GameObject content = null;
    private RectTransform m_rectTransform = null;
    private Image scrollBack = null;
    private Image scrollHandle = null;
    private bool allSet = false;

	// Use this for initialization
	void Start () 
    {
        content = GameObject.Find("Content");
        m_rectTransform = GetComponent<RectTransform>();
        scrollBack = GetComponent<Image>();
        Transform tempGO = transform.GetChild(0);
        tempGO = tempGO.transform.GetChild(0);
        scrollHandle = tempGO.gameObject.GetComponent<Image>();	
        scrollBack.enabled = false;
        scrollHandle.enabled = false;             
	}
	
    void SetVisibility() 
    {
        int contentScale = (int)content.GetComponent<RectTransform>().sizeDelta.y;
        int scrollScale = (int)m_rectTransform.sizeDelta.y;

        Debug.Log(contentScale + " " + scrollScale);

        if (scrollScale > contentScale) 
        {
            scrollBack.enabled = false;
            scrollHandle.enabled = false;
        } 
        else 
        {
            scrollBack.enabled = true;
            scrollHandle.enabled = true;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if(!allSet)
        {
            if (content.GetComponent<ContentTransform>().allSet == true)
            {
                SetVisibility();
                allSet = true;
            }                
        }        
	}
}
