using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonTransform : MonoBehaviour {
        
    public int imageWidth = 0;
    public int imageHeight = 0;
    public int offset = 0;
    public int total = 0;
    public int targetHeight = 300;

    private RectTransform m_rectTranform = null;
    private int yOffset = 0;
    private Vector2 sizeDeltaScalar = Vector2.zero;
    private Vector2 targetSize = Vector2.one;
    private bool allSet = false;

    void SetRect() 
    {
        //set sizeDelta on RectTransform
        targetSize = new Vector2(targetHeight, targetHeight);
        float scalarWidth = (float)imageWidth / (float)imageHeight;
        sizeDeltaScalar = new Vector2(scalarWidth, 1.0f);
        float scalar = (float)Screen.width / 1920f;
        Vector2 sizeDelta = new Vector2(targetSize.x * sizeDeltaScalar.x * scalar, targetSize.y * sizeDeltaScalar.y * scalar);
        m_rectTranform.sizeDelta = sizeDelta;

        //set position based on offset
        float totalOffset = total * sizeDelta.y;       

        if( total % 2 == 0) 
        {
            float yOffset = totalOffset - (totalOffset * 0.5f) - ((float)offset * sizeDelta.y);
            Vector3 offsetPos = new Vector3(0, yOffset, 0);
            m_rectTranform.localPosition = offsetPos;
        }
        else 
        {
            float yOffset = totalOffset - ((totalOffset * 0.5f) + (sizeDelta.y * 0.5f)) - ((float)offset * sizeDelta.y);             
            Vector3 offsetPos = new Vector3(0, yOffset, 0);
            m_rectTranform.localPosition = offsetPos;            
        }
    }
    
	void Start () 
    {
        m_rectTranform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(!allSet) 
        {
            SetRect();
            allSet = true;
        }	
	}
}
