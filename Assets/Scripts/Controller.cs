using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class Controller : MonoBehaviour {

    public string url = "";
    public RootObject data;
    public static bool JSON_LOAD_COMPLETE = false;
    public static bool OBJECTS_CREATED = false;

    public GameObject canvas = null;
    public Font defaultFont = null;
    
    public class Button 
    {
        public string title { get; set; }
        public string imgLocation { get; set; } //going to need to www fetch these images on the material level accessing this
        public string color { get; set; } // going to need to convert this string from 3 digits of hex to a Unity Color type
        public string type { get; set; } // a simple switch to choose between sphere or cube as the primative
        public bool obeyGravity { get; set; } //setting flags on the created object                
    }
    
    public class RootObject 
    {
        public string title { get; set; }
        public int maxObjects { get; set; }        
        public List<Button> buttons { get; set; }
    }    

    public List<GameObject> genObjects = new List<GameObject>();

    IEnumerator WWWGet() 
    {
        WWW www = new WWW(url);
        yield return www;
        PopulateData(www.text);
    }

    void PopulateData(string text) 
    {
        data = JsonConvert.DeserializeObject<RootObject>(text);
        JSON_LOAD_COMPLETE = true;
    }

    public string GetHtmlFromUri(string resource) 
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try 
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) 
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess) 
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream())) 
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs) 
                        {
                            html += ch;
                        }
                    }
                }
            }
        } 
        catch 
        {
            return "";
        }
        return html;
    }

    void CreateTitleGameObject(string text) 
    {
        if (canvas == null) {
            return;
        }

        GameObject go = new GameObject();
        go.name = "Title";
        go.transform.parent = canvas.transform;
        Text txt = go.AddComponent<Text>();
        txt.text = text;
        txt.font = defaultFont;
        go.AddComponent<TitleSizing>();
    }

	void Start () 
    {
        string HtmlText = GetHtmlFromUri("http://google.com");
        if (HtmlText == "") 
        {   //No connection
            Debug.LogError("Unable to load " + url + "from web.");
            Application.Quit();
        } 
        else if (!HtmlText.Contains("schema.org/WebPage")) 
        {   //Error since the beginning of googles html contains that           
            Debug.LogError("Unable to load " + url + "from web.");
            Application.Quit();
        } 
        else 
        {   //success
            StartCoroutine("WWWGet");
        }

        canvas = GameObject.Find("Canvas");
        //make upper text
        
        //make scrolling buttons
        //make bottom button
	}
	
	void Update () 
    {
        if (!OBJECTS_CREATED && JSON_LOAD_COMPLETE) 
        {
            CreateTitleGameObject(data.title);
            OBJECTS_CREATED = true;
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            Debug.Log(data.title);
            Debug.Log(data.maxObjects);
            Debug.Log(data.buttons.Count);
        }
        
    }
}