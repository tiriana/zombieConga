using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Transform spawnPoint;

    public float speed = -2;

    // Use this for initialization
    void Start () {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        spawnPoint = GameObject.Find("SpawnPoint").transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnBecameInvisible() {
        if (Camera.main == null)
            return;
        float yMax = Camera.main.orthographicSize - 0.5f;
        transform.position = new Vector3(spawnPoint.position.x,
                                          Random.Range(-yMax, yMax),
                                          transform.position.z);
    }
}
