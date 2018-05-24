using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera camera = Camera.main;

        float width = (float)GetComponent<Renderer>().bounds.size.x;
        float height = (float)GetComponent<Renderer>().bounds.size.y;

        float screenWidth = camera.aspect * camera.orthographicSize;
        float screenHeight = camera.aspect * camera.orthographicSize;

        float ratioVertical = screenWidth / width;
        float ratioHorizontal = screenHeight / height;

        float maxRatio = Mathf.Max(ratioVertical, ratioHorizontal);

        transform.localScale = new Vector3(maxRatio, maxRatio, 1);
    }

}
