using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ConsoleManager : MonoBehaviour
{
    private TextMeshProUGUI testoConsole;

    private string testo;

    public GameObject pannelli;

    void Start()
    {
        testoConsole = gameObject.GetComponent<TextMeshProUGUI>();
        testo = "> Everything loaded fine. Use this tab to check progresses\n\n>The icons below indicate AL's health, the amount of energy, AL's ammos and the integrity of your PC\n\n> You'll need energy to use power ups, while virus attacks can destroy your PC\n\n> Check for AL's position in the map\n\n > Your first task is to guide AL to the DataBase icon visible on the map \n\n> You will need your DataBase to understand how AL can destroy complex enemies";
    }

    void Update()
    {
        testoConsole.text = testo;
    }

    public void aggiornaConsole(string nuovoTesto)
    {
        testo += nuovoTesto;

        //l'idea è passare alla console ogni volta che si aggiorna il testo
        pannelli.GetComponent<TabsController>().ButtonMouseClick(3);
    }

   


}
