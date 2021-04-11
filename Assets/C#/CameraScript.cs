using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    public Text myNick;

    void Update()
    {
        if (player != null)
        {
            myNick.text = PlayerPrefs.GetString("net_name");
            //Vector3 P_pos = new Vector3(player.transform.position.x, player.transform.position.y, -1f);
            //Vector3 pos = Vector3.Lerp(a: this.transform.position, b: P_pos, t: moveSpeed * Time.deltaTime);
            //this.transform.position = pos;
            Vector3 P_pos = new Vector3(player.transform.position.x, player.transform.position.y, -1f);
            this.transform.position = P_pos;
        }
    }
}
