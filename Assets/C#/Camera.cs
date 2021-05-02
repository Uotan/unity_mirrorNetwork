using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;

    public Text myNick;
    void Update()
    {
        if (player!=null)
        {

            Vector3 P_pos = new Vector3(player.transform.position.x, player.transform.position.y+2.34f, -1f);
            Vector3 pos = Vector3.Lerp(a: this.transform.position, b: P_pos, t: moveSpeed * Time.deltaTime);
            this.transform.position = pos;
        }
    }
}
