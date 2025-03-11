using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
public class ButtonInternal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool selected;
    private bool wasPressed = false;
    public bool isMouseOver;
    [SerializeField] Image img;
    Color std;
    [SerializeField] Color over;
    [SerializeField] Color pressed;
    [SerializeField] float transitionTime = 0.1f;
    
    protected virtual void Start()
    {
        GetComponent<Image>().raycastTarget = true;
        std = img.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        if (!wasPressed){img.CrossFadeColor(over, transitionTime, true, true);}
        HandlePointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        if (!wasPressed){img.CrossFadeColor(std, transitionTime, true, true);}
        HandlePointerExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandlePointerClick();
    }

    protected virtual void HandlePointerEnter() {}
    protected virtual void HandlePointerExit() {}
    protected virtual void HandlePointerClick() {ActedOn();}

    public void DSBHActSelected(){
        ActedOn();
    }

    protected virtual void ActedOn(){
        wasPressed = true; img.CrossFadeColor(pressed, transitionTime, true, true);
    }

    public void DSBHSelected(){
        if (!wasPressed){img.CrossFadeColor(over, transitionTime, true, true);}
    }
    public void DSBHUnselected(){
        if (!wasPressed){img.CrossFadeColor(std, transitionTime, true, true);}
    }

}
