using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void JoinConga() {
        Debug.Log("Cat joins conga");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().SetBool("inConga", true);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void GrantCatTheSweetReleaseOfDeath() {
        DestroyObject(gameObject);
    }

    void OnBecameInvisible() {
        GrantCatTheSweetReleaseOfDeath();
    }
}
