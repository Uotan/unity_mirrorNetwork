using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public InputField _joinfield;
    public InputField _createfield;

    public void JoinRoom(){
        PhotonNetwork.JoinRoom(_joinfield.text);
    }
    public void CreateRoom(){
        RoomOptions _rmoptns = new RoomOptions();
        _rmoptns.MaxPlayers = 6;
        PhotonNetwork.CreateRoom(_createfield.text,_rmoptns);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(5);
    }
}
