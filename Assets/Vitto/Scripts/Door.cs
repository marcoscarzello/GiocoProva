using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject panels = null;
    //public int i; //tab index 2=security
    private TabsController tabs = null;
    // Start is called before the first frame update
    void Start()
    {
        tabs = panels.GetComponent<TabsController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void openTab( int i){ tabs.ButtonMouseClick(i); }
}
