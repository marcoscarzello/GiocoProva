using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class NetworkManagerCustom : NetworkManager
{
    // Called by UI element NetworkAddressInput.OnValueChanged
    public void SetHostname(TextMeshProUGUI text)
    {
        networkAddress = text.text;
    }


    public override void OnStartServer()
    {
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

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            SetupMenuSceneButtons();
        else
            SetupOtherSceneButtons();
    }

    void SetupMenuSceneButtons()
    {
        GameObject.Find("HostButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("HostButton").GetComponent<Button>().onClick.AddListener(StartHost);

        GameObject.Find("ClientButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ClientButton").GetComponent<Button>().onClick.AddListener(StartHost);
    }

    void SetupOtherSceneButtons()
    {
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("DisconnectButton").GetComponent<Button>().onClick.AddListener(StopHost);

    }
}
