using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TestoSolutionCode : MonoBehaviour
{
    public TextMeshProUGUI testo;
    public string tmp = "";
    public GameObject red1;
    public GameObject red2;
    public GameObject red3;
    public GameObject green1;
    public GameObject green2;
    public GameObject green3;
    public GameObject blue1;
    public GameObject blue2;
    public GameObject blue3;


    public static string sol; 

    void Start()
    {
        DisattivaSol();
    }

    public void ScriviSoluzione()
    {
        tmp = "";
        //testo.text = EnemyFinder.codiceSoluzione;
        sol = EnemyFinder.codiceSoluzione;

        if (sol == null || sol.Length == 0 || sol == "No Match")
        {
            Debug.Log("NoMatch");
            tmp = "No Match";

        }
        else
        {
            First();
            tmp += "   " + sol[1] + "         ";
            if (sol.Length == 4 || sol.Length == 6)
            {
                Second();
                tmp += "      " + sol[3] + "         ";
                if (sol.Length == 6)
                {
                    Third();
                    tmp += "      " + sol[5] + "     ";
                }
            }
        }

        testo.text = tmp;
    }

    public void First()
    {
        if (sol[0] == 'u')
            red1.SetActive(true);

        else if (sol[0]  =='v')
            green1.SetActive(true);

        else if (sol[0] == 'z')
            blue1.SetActive(true);

    }
    public void Second()
    {
        if (sol[2]== 'u')
            red2.SetActive(true);

        else if (sol[2] == 'v')
            green2.SetActive(true);

        else if (sol[2] == 'z')
            blue2.SetActive(true);
    }

    public void Third()
    {
        if (sol[4] == 'u')
            red3.SetActive(true);

        else if (sol[4] == 'v')
            green3.SetActive(true);

        else if (sol[4] == 'z')
            blue3.SetActive(true);
    }

    public void DisattivaSol()
    {
        red1.SetActive(false);
        red2.SetActive(false);
        red3.SetActive(false);
        green1.SetActive(false);
        green2.SetActive(false);
        green3.SetActive(false);
        blue1.SetActive(false);
        blue2.SetActive(false);
        blue3.SetActive(false);
    }

}
