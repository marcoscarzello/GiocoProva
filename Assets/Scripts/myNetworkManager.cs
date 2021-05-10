using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class myNetworkManager : NetworkManager
{
    public override void OnStartServer() {
        Debug.Log("server partito");

    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("connesso al server");

    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("disconnesso dal server");

    }
}
