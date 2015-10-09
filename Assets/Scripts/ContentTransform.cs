using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContentTransform : MonoBehaviour {
    private RectTransform m_rectTransform = null;
    public int itemCount = 1;
    private int _lastItemCount = 0;
    public int itemX = 300;
    public int itemY = 300;
    private int xWidth = 0;
    private int yHeight = 0;
    private bool allSet = false;

    void SetScale() {
        float scalar = (float)Screen.width / 1920.0f;
        xWidth = (int)((float)itemX * scalar);
        int yBase = (int)((float)itemY * scalar);
        yHeight = itemCount * yBase;

        m_rectTransform.sizeDelta = new Vector2(xWidth, yHeight);
    }

    void Start() {
        m_rectTransform = GetComponent<RectTransform>();
    }

    void Update() {
        if (itemCount != _lastItemCount) { allSet = false; }

        if (!allSet) {
            SetScale();
            allSet = true;
        }

        _lastItemCount = itemCount;
    }
}
