using UnityEngine;
using UnityEngine.UI;
//using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class ObjectCreator : MonoBehaviour 
{    
    public string _name = "";
    public string _color = "";
    public string _type = "";
    public bool _gravity = false;

    public float _velocity = 1;
    public float _scaleFactor = 1f;
    public Material baseMat = null;

    private GameObject canvas = null;
    public Font defaultFont = null;

    public int maxObjects = 0;
    public static int totalCollisions = 0;    
    
    public List<Material> genMaterials = new List<Material>();
    public List<GameObject> genObjects = new List<GameObject>();
    
    public void DestroyAll() 
    { 
        foreach(GameObject go in genObjects) 
        {
            Destroy(go);
        }

        foreach(Material mat in genMaterials) 
        {
            Destroy(mat);
        }

        genMaterials = new List<Material>();
        genObjects = new List<GameObject>();
    }
    public void CreateObject(string name, string color, string type, bool gravity) 
    {
        //if object count == maxObjects destroy an object first;
        if (genObjects.Count == maxObjects) 
        {
            Destroy(genMaterials[0]);
            genMaterials.RemoveAt(0);
            Destroy(genObjects[0]);
            genObjects.RemoveAt(0);
        }

        switch (type) 
        { 
        case ("Cube"):
            CreateCube(name, color, gravity);
            break;
        case ("Sphere"):
            CreateSphere(name, color, gravity);
            break;
        default:
            Debug.Log("Boo!");
            break;
        }    
    }

    string ColorToHex(Color32 color) {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    public static string RemoveSpecialCharacters(string str) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++) {
            if ((str[i] >= '0' && str[i] <= '9')
                || (str[i] >= 'A' && str[i] <= 'z'
                    || (str[i] == '.' || str[i] == '_'))) {
                sb.Append(str[i]);
            }
        }

        return sb.ToString();
    }

    Color HexToColor(string hex) {

        if (hex == null) {
            return new Color(0.5f, 0.5f, 0.5f);
        }

        hex = hex.ToUpper();
        hex = RemoveSpecialCharacters(hex);

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);        
    }

    void CreateCube(string name, string color, bool gravity) 
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = name;
        //go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);

        go.GetComponent<BoxCollider>().material = this.GetComponent<MeshCollider>().material;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = gravity;
        Vector3 random = new Vector3(Random.Range(-_velocity, _velocity), Random.Range(-_velocity, _velocity), Random.Range(-_velocity, _velocity));
        rb.velocity = random;

        //get the color and create the material
        Material mat = new Material(baseMat);
        mat.color = HexToColor(color);       
        genMaterials.Add(mat);

        //colliderTest
        CollisionCounter colCount = go.AddComponent<CollisionCounter>();

        //add UI Jazz
        GameObject text = new GameObject();
        text.name = "Text";
        text.transform.parent = canvas.transform;
        Text txt = text.AddComponent<Text>();
        txt.font = defaultFont;
        ObjectGUI oUI = text.AddComponent<ObjectGUI>();
        oUI.followedObject = go;
        oUI.cc = colCount;

        go.GetComponent<MeshRenderer>().material = mat;
        genObjects.Add(go);
    }

    void CreateSphere(string name, string color, bool gravity) 
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.name = name;        
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);

        go.GetComponent<SphereCollider>().material = this.GetComponent<MeshCollider>().material;

        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.useGravity = gravity;
        Vector3 random = new Vector3(Random.Range(-_velocity, _velocity), Random.Range(-_velocity, _velocity), Random.Range(-_velocity, _velocity));
        rb.velocity = random;

        //get the color and create the material
        Material mat = new Material(baseMat);
        mat.color = HexToColor(color);        
        genMaterials.Add(mat);

        //colliderTest
        CollisionCounter colCount = go.AddComponent<CollisionCounter>();

        //add UI Jazz
        GameObject text = new GameObject();
        text.name = "Text";
        text.transform.parent = canvas.transform;
        Text txt = text.AddComponent<Text>();
        txt.font = defaultFont;
        ObjectGUI oUI = text.AddComponent<ObjectGUI>();
        oUI.followedObject = go;
        oUI.cc = colCount;

        go.GetComponent<MeshRenderer>().material = mat;
        genObjects.Add(go);
    }

	// Use this for initialization
	void Start () 
    {
        canvas = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(totalCollisions);

	    if(Input.GetKeyUp(KeyCode.D))
        {
            if( _name != null &&
                _color != null &&
                _type != null )
            {
                CreateObject(_name, _color, _type, _gravity);
            }
        }
	}
}
