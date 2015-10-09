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
    private bool JSON_LOAD_COMPLETE = false;
    private bool TITLE_CREATED = false;
    private bool IMAGES_LOADED = false;
    private bool BUTTONS_CREATED = false;
    private bool MENU_CREATED = false;    

    public GameObject canvas = null;
    public GameObject content = null;
    public Font defaultFont = null; 

    private int totalImages = 99;
    private int imagesLoaded = 0;
    public Texture2D defaultTex2D = null; //set in editor

    public List<Texture2D> images = new List<Texture2D>();
    public List<Sprite> sprites = new List<Sprite>();        
    
    public class Button 
    {
        public string title { get; set; }
        public string image { get; set; } 
        public string color { get; set; } 
        public string type { get; set; } 
        public bool obeyGravity { get; set; }
    }
    
    public class RootObject 
    {
        public string title { get; set; }
        public int maxObjects { get; set; }        
        public List<Button> buttons { get; set; }
    }           
    
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

    void SetContentCount() 
    {
        content.GetComponent<ContentTransform>().itemCount = imagesLoaded;
    }

    void GetImages() 
    {
        totalImages = data.buttons.Count;        

        for (int i = 0; i < totalImages; i++) {            
            Texture2D tex2D = defaultTex2D;
            images.Add(tex2D);
            StartCoroutine(WWWGetImages(data.buttons[i].image, i));            
        }       
    }

    IEnumerator WWWGetImages(string url, int listPos) 
    {        
        WWW www = new WWW(url);
        yield return www;
        
        if (!string.IsNullOrEmpty(www.error)) 
        {
            Debug.LogError("Image " + url + " not loaded.");
            data.buttons[listPos].image = null;
            --totalImages;           
        }
        else 
        {
            images[listPos] = new Texture2D(400, 300);
            www.LoadImageIntoTexture(images[listPos]);
            images[listPos].filterMode = FilterMode.Bilinear;
            ++imagesLoaded;
        }        
    }

    void CreateButtons() 
    {
        int spriteCount = 0;
        
        foreach (Button button in data.buttons) 
        {            
            if (button.image != null) 
            {
                spriteCount++;
            }
        }
        totalImages = spriteCount;

        int iterator = 0;
        int offsetCounter = 0;

        foreach (Button button in data.buttons) 
        {
            if (button.image != null) 
            {
                //create a sprite                           
                Vector2 spriteSize = new Vector2(images[iterator].width, images[iterator].height);
                Sprite tempSprite = Sprite.Create(images[iterator], new Rect(Vector2.zero, spriteSize), new Vector2(0.5f, 0.5f));
                sprites.Add(tempSprite);

                //build the button GameObject;
                GameObject go = new GameObject();
                go.name = data.buttons[iterator].title;
                UnityEngine.UI.Image uiSprite = go.AddComponent<UnityEngine.UI.Image>();
                uiSprite.sprite = tempSprite;
                UnityEngine.UI.Button uiButton = go.AddComponent<UnityEngine.UI.Button>();
                uiButton.targetGraphic = uiSprite;
                
                //build the text child of the button
                GameObject textGO = new GameObject();
                textGO.name = "Text";
                Text text = textGO.AddComponent<Text>();
                text.text = data.buttons[iterator].title;
                text.font = defaultFont;
                text.color = Color.white;  
                //HACK: missing offset for text...
             
                //going to need some more positional stuff for the scroll window
                textGO.transform.SetParent(uiButton.gameObject.transform);
                go.transform.SetParent(content.transform);
                go.transform.localPosition = Vector3.zero;

                //adding transform tools
                ButtonTransform bt = go.AddComponent<ButtonTransform>();
                bt.imageWidth = (int)spriteSize.x;
                bt.imageHeight = (int)spriteSize.y;
                bt.offset = offsetCounter;
                bt.total = totalImages;
                offsetCounter++;
            }
            iterator++;     
        }
    }    

    void CreateButton(Button data) 
    {
        GameObject go = new GameObject();
        go.name = data.title;
        go.transform.parent = canvas.transform;
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
        content = GameObject.Find("Content");
	}
	
	void Update () 
    {
        if (!TITLE_CREATED && JSON_LOAD_COMPLETE) 
        {
            CreateTitleGameObject(data.title);
            TITLE_CREATED = true;
        }

        if (!IMAGES_LOADED && JSON_LOAD_COMPLETE) 
        {
            GetImages();            
            IMAGES_LOADED = true;            
        }
        
        if (imagesLoaded == totalImages && !BUTTONS_CREATED) {            
            SetContentCount();
            CreateButtons();
            BUTTONS_CREATED = true;
        }      
    }
}