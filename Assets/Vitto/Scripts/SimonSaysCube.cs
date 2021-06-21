using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonSaysCube : MonoBehaviour
{
    private SimonSaysGameBoard simonGameBoard;
    //private PlayerInputHandler playerinputhandler;

    private RawImage Raw;
    public Material InactiveMaterial;
    public Material ActiveMaterial;

    public float ActiveDurationSeconds = 1f;
    public float CooldownDurationSeconds = 0.2f;

    private int cubeIndex;

    private void Awake()
    {
        Raw = GetComponent<RawImage>();

        Raw.color = InactiveMaterial.color;

        simonGameBoard = GetComponentInParent<SimonSaysGameBoard>();
        //playerinputhandler = GetComponentInParent<PlayerInputHandler>();
    }

    //private void Update()
    //{
    //    if(playerinputhandler.getCanclick()

    //}
    
    public void SetActiveForPlayerSequence()
    {
        StartCoroutine(SetActiveRoutine(ActiveDurationSeconds / 3f, CooldownDurationSeconds / 3f));
    }

    public IEnumerator SetActiveRoutine()
    {
        Raw.color = ActiveMaterial.color;

        yield return new WaitForSeconds(ActiveDurationSeconds);

        Raw.color = InactiveMaterial.color;

        yield return new WaitForSeconds(CooldownDurationSeconds);
    }

    public IEnumerator SetActiveRoutine(float activeDuration, float cooldownDuration)
    {
        Raw.color = ActiveMaterial.color;

        yield return new WaitForSeconds(activeDuration);

        Raw.color = InactiveMaterial.color;

        yield return new WaitForSeconds(cooldownDuration);
    }

    private void SetInactive()
    {
        Raw.color = InactiveMaterial.color;
    }

    public void PlayerSelect()
    {
        SetActiveForPlayerSequence();

        simonGameBoard.RegisterInput(cubeIndex);
    }

    public void SetIndex(int i)
    {
        cubeIndex = i;
    }
}
