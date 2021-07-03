using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TabsButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int tabindex;
    private Image image;

    private TabsController controller;

    public bool isActive = false;

    private void Awake()
    {
        controller = FindObjectOfType<TabsController>();
        image = GetComponent<Image>();
    }
    public void SetIndex(int _index)
    {
        tabindex = _index;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        controller.ButtonMouseClick(tabindex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isActive)
        {
            image.color = controller.mouseEnterColor;
        }

        controller.ButtonMouseEnter(tabindex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isActive)
        {
            image.color = controller.normalColor;
        }

        controller.ButtonMouseExit(tabindex);
    }

    private bool flag = false; 
    public void ToggleActive()
    {
            isActive = !isActive;

        if (isActive)
        {
            image.color = controller.mouseClickColor;
        }
        else
        {
            image.color = controller.normalColor;
        }

    }

}
