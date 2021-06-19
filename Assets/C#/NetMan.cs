using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetMan : NetworkManager
{
    //bool playerSpawned;
    NetworkConnection connection;
    //ol playerConnected;
    public void OnCreateCharacter(NetworkConnection conn, PosMessage message)
    {
        GameObject go = Instantiate(playerPrefab, message.vector2, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, go); 
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<PosMessage>(OnCreateCharacter); 
    }

    public void ActivatePlayerSpawn()
    {
        
        Vector3 pos = new Vector3(0f,0f,0f);
        //pos = Camera.main.ScreenToWorldPoint(pos);

        PosMessage m = new PosMessage() { vector2 = pos }; 
        connection.Send(m); 
        //playerSpawned = true;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        connection = conn;
        //playerConnected = true;
        ActivatePlayerSpawn();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0) && !playerSpawned && playerConnected)
    //    {
    //        ActivatePlayerSpawn();
    //    }
    //}

}
public struct PosMessage : NetworkMessage 
{
    public Vector2 vector2; 
}
