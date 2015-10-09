using UnityEngine;
using System.Collections;

public class ImageLoad : MonoBehaviour {

    public string url = "";
    private string _lastUrl = "";
    public Texture2D texture;

    IEnumerator LoadImage() 
    {
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(texture);
        texture.filterMode = FilterMode.Bilinear;            
    }

	void Start () 
    {
        
	}	
	
	void Update () 
    {
        if(url != null && url != _lastUrl) 
        {
            StartCoroutine(LoadImage());
            _lastUrl = url;
        }
        
	}
}
