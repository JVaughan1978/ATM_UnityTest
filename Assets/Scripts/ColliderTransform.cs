using UnityEngine;
using System.Collections;

public class ColliderTransform : MonoBehaviour 
{
    public GameObject currCam = null; // set by user;   

    private int _lastScreenWidth = 0;
    private int _lastScreenHeight = 0;
    private bool allSet = false;

    public float scalarConstant = 1.0f;
    
    void SetPosition() 
    {
        Camera cam = currCam.GetComponent<Camera>();
        Vector3 offsetPosition = new Vector3(((float)Screen.width * 0.42f),((float)Screen.height * 0.5f), 10.0f);
        Vector3 position = cam.ScreenToWorldPoint(offsetPosition);
        transform.position = position;
    }

    void SetScale() 
    {
        //need the magic number from the screens
        float scalarX = 1.0f;
        float scalarY = 1.0f;
        float scalarZ = 1.0f;

        if(Screen.width > Screen.height) 
        {
            scalarX = (float)Screen.width / (float)Screen.height;
            scalarY = 1.0f / 0.8f;
        }
        else 
        {
            scalarY = (float)Screen.height / (float)Screen.width;
            scalarY = 1.0f / 0.8f;
        }

        Vector3 scale = new Vector3(scalarX * scalarConstant, scalarY * scalarConstant, scalarZ * scalarConstant);        
        transform.localScale = scale;
    }

	// Use this for initialization
	void Start () {
        currCam = GameObject.Find("Main Camera");
        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;
        SetPosition();
        SetScale();
	}
	
	// Update is called once per frame
	void Update () {
        if(_lastScreenHeight != Screen.height || _lastScreenWidth != Screen.width) 
        {
            allSet = false;
        }        

        if(!allSet) 
        {
            SetPosition();
            SetScale();
            allSet = true;
        }

        _lastScreenHeight = Screen.height;
        _lastScreenWidth = Screen.width;	
	}
}
