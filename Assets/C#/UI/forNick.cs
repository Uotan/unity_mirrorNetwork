using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forNick : MonoBehaviour
{
    public Text _nickname;

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetString("net_name",_nickname.text);
    }
}
