using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// definisce il comportamento di un macchina a stati finiti
//non incapsula il comportamento dentro la classe generale ma in diverse per ogni stato
//sono classi esterne al mono behaviour
public class FiniteStateMachine<T>
{
    private T _owner;  //generalizzazione della FSM
    private State _currentState;
    private Dictionary<string, List<Transition>> _transitions = new Dictionary<string, List<Transition>>(); //è un dizionario che punta ad una lista di possibili transizioni
    private List<Transition> _currentTransitions = new List<Transition>(); //mantiene riferimento alle transizioni per lo stato corrente
    public FiniteStateMachine(T owner)
    {
        _owner = owner;
    }

    public void Tik() //aggiorna lo stato corrente
    {
        State nextState = GetNextState(); //controlla se c'è un nuovo stato o meno quindi se diverso setta il nuovo stato
        if (nextState != null)
            SetState(nextState);

        if (_currentState != null)
            _currentState.Tik();
    }

    public void SetState(State state) //segna lo stato 
    {
        if (state == _currentState)
            return;

        _currentState?.Exit(); //esce dallo stato corrente
        Debug.Log($"Changing State FROM:{_currentState?.Name} --> TO:{state.Name}");
        _currentState = state; //assegno il nuovo stato 

        _transitions.TryGetValue(_currentState.Name, out _currentTransitions);

        _currentState.Enter(); //entra nel nuovo stato
    }

    public void AddTransition(State fromState, State toState, Func<bool> transitionCondition) //questa funzione vuole stato di partenza stato di arrivo e una funzione
    {
        if (_transitions.TryGetValue(fromState.Name, out var stateTransitions) == false) //controlla che non sia già presente la chiave, se non òp trova
        {
            stateTransitions = new List<Transition>();                                  //crea nuova lista
            _transitions[fromState.Name] = stateTransitions;
        }

        stateTransitions.Add(new Transition(toState, transitionCondition));

    }

    private State GetNextState()
    {
        if (_currentTransitions == null) //se la lista di transizioni associata alla stato corrente è null err
            Debug.LogError($"Current State {_currentState.Name} has NO transitions");

        foreach (Transition transition in _currentTransitions) //per ogni transizione verifico la condizione se è verificata se lo è ritorno il next state
        {
            if (transition.Condition())
                return transition.NextState;
        }

        return null;
    }
}
