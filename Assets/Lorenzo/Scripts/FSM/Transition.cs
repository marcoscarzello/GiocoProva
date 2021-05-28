using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    //stato a cui la transizione è associata
    public State NextState { get; }

    //è un delegato dove condiction è una funzione che restituisce un delegato
    public Func<bool> Condition { get; }

    public Transition(State nextState, Func<bool> transitionCondition)
    {
        NextState = nextState;
        Condition = transitionCondition;
    }

}
