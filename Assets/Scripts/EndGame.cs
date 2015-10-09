using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour {

    public GameObject EndText = null;

    public delegate void FinishAction();
    public static event FinishAction Finished;
    
    void OnEnable() 
    { 
        EndGame.Finished += TheEnd;
    }

    void OnDisable()
    {
        EndGame.Finished -= TheEnd;
    }

    void ApplicationRestart() 
    {
        Application.LoadLevel(0);
    }
    
    public void TheEnd() 
    {
        Debug.Log("HUZZAH!");
        Invoke("ApplicationRestart", 10.0f);
        EndText.SetActive(true);
        GameObject title = GameObject.Find("Title");
        Destroy(title);
        GameObject content = GameObject.Find("Content");
        Destroy(content);
        GameObject objectCreator = GameObject.Find("Collider");
        ObjectCreator objCreate = objectCreator.GetComponent<ObjectCreator>();
        objCreate.DestroyAll();         
        GameObject canvas = GameObject.Find("Canvas");
        for (int i = 0; i < canvas.transform.childCount; i++) 
        {
            Transform go = canvas.transform.GetChild(i);
            if(go.gameObject.name == "Text") 
            {
                Destroy(go.gameObject);
            }
        }            
        this.gameObject.SetActive(false);
        Text txt = EndText.GetComponent<Text>();
        txt.text = ObjectCreator.totalCollisions.ToString();
    }
    	
	void Start () {
        EndText.SetActive(false);
	}
		
	void Update () {
	
	}
}
