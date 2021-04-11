using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IPchecker : MonoBehaviour
{
    public Text textIP;
    void Start()
    {
        textIP.text = IPManager.GetIP(ADDRESSFAM.IPv4);
    }
}
