using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virusHelper : MonoBehaviour
{

    public GameObject virusattack;

    public void startVirusAttack() {
        Debug.Log("Sono il virus helper, ora faccio partire l'attacco.");
        virusattack.GetComponent<VirusAttack>().AttaccoVirus();
    }
}
