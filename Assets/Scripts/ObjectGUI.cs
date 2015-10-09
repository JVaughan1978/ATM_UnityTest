using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectGUI : MonoBehaviour {

    public GameObject followedObject = null;
    public CollisionCounter cc = null;
    private GameObject mainCam = null;
    private Text txt = null;
    private RectTransform rect = null;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        mainCam = GameObject.Find("Main Camera");
        txt = GetComponent<Text>();
        rect = GetComponent<RectTransform>();        
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = cc.collisionCount.ToString();
        txt.alignment = TextAnchor.UpperCenter;

        //position        
        Vector3 position = mainCam.GetComponent<Camera>().WorldToScreenPoint(followedObject.transform.position);
        position = new Vector3( (position.x - ((float)Screen.width * 0.5f)), (position.y - ((float)Screen.height * 0.5f)), position.z);
        rect.localPosition = position;        
	}
}
