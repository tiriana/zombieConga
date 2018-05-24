using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Invoke("LoadLevel", 3f);
    }

    void LoadLevel()
    {
        Application.LoadLevel("LaunchScene");
    }

}
