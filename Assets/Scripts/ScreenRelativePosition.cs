using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRelativePosition : MonoBehaviour {

    public enum ScreenEdge { LEFT, RIGHT, TOP, BOTTOM };
    public ScreenEdge screenEdge;
    public float yOffset;
    public float xOffset;

    // Use this for initialization
    void Start () {
        // 1
        Vector3 newPosition = transform.position;
        Camera camera = Camera.main;

        // 2
        switch (screenEdge)
        {
            // 3
            case ScreenEdge.RIGHT:
                newPosition.x = camera.aspect * camera.orthographicSize + xOffset;
                newPosition.y = yOffset;
                break;

            // 4
            case ScreenEdge.TOP:
                newPosition.x = xOffset;
                newPosition.y = camera.orthographicSize + yOffset;
                break;

            case ScreenEdge.LEFT:
                newPosition.x = -camera.aspect * camera.orthographicSize + xOffset;
                newPosition.y = yOffset;
                break;
            
            case ScreenEdge.BOTTOM:
                newPosition.x = xOffset;
                newPosition.y = -camera.orthographicSize + yOffset;
                break;
        }
        // 5
        transform.position = newPosition;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
