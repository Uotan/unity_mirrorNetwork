using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawn_player : MonoBehaviour
{
    public GameObject _playerprefab;
    void Start()
    {
        Vector2 _spawnpos = new Vector2(0f,0f);
        PhotonNetwork.Instantiate(_playerprefab.name, _spawnpos,Quaternion.identity);
    }


}
