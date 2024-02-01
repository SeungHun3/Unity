using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour
{
    private ScrollRect scrollRect;
    public float space;
    public GameObject uiPrefab;
    public GameObject Image;
    public GameObject contentBox;
    public VerticalLayoutGroup layout;
    public ContentSizeFitter sizeFiltter;

    public List<RectTransform> uiObject = new List<RectTransform>();    
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        layout = contentBox.GetComponent<VerticalLayoutGroup>();
        sizeFiltter = contentBox.GetComponent<ContentSizeFitter>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewUIObject()
    {
        layout.enabled = false;
        sizeFiltter.enabled = false;
        var newUI = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        uiObject.Add(newUI);
        float y = 0;
        foreach(var target in uiObject)
        {
            target.anchoredPosition = new Vector2(0f, -y);
            y += target.sizeDelta.y + space;
        }
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }

    public void AddImage()
    {
        layout.enabled = true;
        sizeFiltter.enabled = true;
        Instantiate(Image, scrollRect.content);
    }
}
