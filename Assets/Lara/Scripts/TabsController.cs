using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabsController : MonoBehaviour
{
    public Transform buttonsPanel;
    public Transform panelsPanel;

    public Color normalColor;
    public Color mouseEnterColor;
    public Color mouseClickColor;

    public UnityEvent TabSelectionChangedEvent;

    private int selectedIndex;
    private TabsButton selectedButton;

    private List<TabsButton> buttons = new List<TabsButton>();
    private List<Transform> panels = new List<Transform>();

    public void Start()
    {
        for (int i=0; i<buttonsPanel.transform.childCount; i++)
        {
            GameObject buttonGo = buttonsPanel.transform.GetChild(i).gameObject;
            TabsButton button = buttonGo.GetComponent<TabsButton>();
            button.SetIndex(i);
            buttons.Add(button);
        }


        foreach(Transform item in panelsPanel.transform)
        {
            panels.Add(item);
        }
    }

    public void ButtonMouseClick(int _id)
    {
        if(selectedButton != null)
        {
            selectedButton.ToggleActive();
        }
        selectedIndex = _id;
        selectedButton = buttons[selectedIndex];
        selectedButton.ToggleActive();
        HideAllPanels();
    }

    public void ButtonMouseEnter(int _id)
    {

    }

    public void ButtonMouseExit(int _id)
    {

    }

    private void HideAllPanels()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == selectedIndex)
            {
                panels[i].gameObject.SetActive(true);
            }
            else
            {
                panels[i].gameObject.SetActive(false);
            }
        }

        if(TabSelectionChangedEvent != null)
        {
            TabSelectionChangedEvent.Invoke();
        }
    }
}
