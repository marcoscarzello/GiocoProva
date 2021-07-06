using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virusHelper : MonoBehaviour
{

    public GameObject virusattack;

    public void startVirusAttack() {
        virusattack.GetComponent<VirusAttack>().AttaccoVirus(true);
    }
}
