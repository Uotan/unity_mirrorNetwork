using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_loader : MonoBehaviour
{
    public int _scenenumber;
    public void _LoadScene()
    {
        SceneManager.LoadScene(_scenenumber);
    }
}
