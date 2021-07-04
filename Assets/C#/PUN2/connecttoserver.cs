using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class connecttoserver : MonoBehaviourPunCallbacks
{
    public int _timer;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(4);
    }
    private void FixedUpdate() {
        _timer++;
        if (_timer>=1000)
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
