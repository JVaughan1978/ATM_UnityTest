using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class TitleSizing : MonoBehaviour {

    // I want to change font size and position of this object based on screen resolution
    private int fontSize = 0;
    private int topOffset = 0;
    private int rectWidth = 0;
    private int rectHeight = 0;
    private bool allSet = false;

    private Text m_text = null;

    int GetFontSize() {
        int ret = 0;
        if (Screen.height >= Screen.width) {
            ret = (int)((float)Screen.height * 0.03f);
        } else {
            ret = (int)((float)Screen.width * 0.03f);
        }
        return ret;
    }

    int GetTopOffset() {
        int ret = 0;
        ret = (int)((float)Screen.height * 0.5f) - (int)((float)fontSize * 1.5f);
        return ret;
    }

    // Use this for initialization
    void Start() {
        m_text = GetComponent<Text>();
        if (m_text.font == null) {
            //m_text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        }

        fontSize = GetFontSize();
        topOffset = GetTopOffset();
        rectHeight = fontSize * 2;
        rectWidth = fontSize * 13;
    }

    // Update is called once per frame
    void Update() {
        if (!allSet) {
            m_text.fontSize = fontSize;
            m_text.rectTransform.localPosition = new Vector3(0, topOffset, 0);
            m_text.rectTransform.sizeDelta = new Vector2(rectWidth, rectHeight);
        }

        //would also introduce to code to deal with Screen.orientation on mobile devices;
    }
}
