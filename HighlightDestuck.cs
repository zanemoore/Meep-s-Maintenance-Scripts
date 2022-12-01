using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Written by Zane
[RequireComponent(typeof(Selectable))]
public class HighlightDestuck : EventTrigger
{

    private Selectable selectable;

    public void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    public void Destuck()
    {
        EventSystem e = EventSystem.current;
        if (selectable.interactable && e.currentSelectedGameObject != gameObject)
        {
            // highlights the button that is selected
            e.SetSelectedGameObject(gameObject);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Destuck();
    }
}