using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : MonoBehaviour {
	
	// I know this is totaly lame to do this this way
    // instead of using a button
    // but when I use a button it's rendered somewhere freaking far away.
    // I'll do it with button when I'll know how to use buttons.
    // Because buttons.
	void Update () {
        if (Input.GetButton("Fire1")){
            Application.LoadLevel("CongaScene");
        }
    }
}
