using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour 
{

    ScrollRect scrollRect;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void ScrollToTop( ScrollRect scrollRect)
    {
        Canvas.ForceUpdateCanvases();

        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
    public void ScrollToBottom( ScrollRect scrollRect)
    {
        Canvas.ForceUpdateCanvases();

        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    private void OnEnable()
    {

        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
