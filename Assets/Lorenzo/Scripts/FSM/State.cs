using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//da qui si ereditano tutti gli stati e sono presenti
//vengono implementate nelle classi figlie
public abstract class State
{
    private string _name;

    //qui le funzioni sono messe direttamente in linea
    public string Name => _name;

    protected State(string name)
    {
        _name = name;
    }
    public abstract void Enter();

    //all'interno di questo andranno a finire le cose che prima finivano in update state
    public abstract void Tik();
    public abstract void Exit();
}
