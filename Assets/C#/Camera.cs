using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Networking;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    void Update()
    {
        if (player==null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player!=null)
        {

            Vector3 P_pos = new Vector3(player.transform.position.x, player.transform.position.y, -1f);
            Vector3 pos = Vector3.Lerp(a: this.transform.position, b: P_pos, t: moveSpeed * Time.deltaTime);
            this.transform.position = pos;
        }
    }
}
