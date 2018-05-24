using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ZombieController : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;
    public float winCond = 20;
    private bool isInvincible = false;
    private float timeSpentInvincible;
    private int lives = 3;

    public AudioClip enemyContactSound;
    public AudioClip catContactSound;

    private List<Transform> congaLine = new List<Transform>();

    [SerializeField]
    private PolygonCollider2D[] colliders;
    private int currentColliderIndex = 0;

    private Vector3 moveDirection;

    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cat"))  {
            Transform followTarget = congaLine.Count == 0 ? transform : congaLine[congaLine.Count - 1];
            other.transform.parent.GetComponent<CatController>().JoinConga(followTarget, moveSpeed, turnSpeed );
            congaLine.Add(other.transform);
            GetComponent<AudioSource>().PlayOneShot(catContactSound);

            if (congaLine.Count >= winCond) {
                Application.LoadLevel("WinScene");
            }

        } else if (!isInvincible && other.CompareTag("enemy")) {
            isInvincible = true;
            timeSpentInvincible = 0;
            GetComponent<AudioSource>().PlayOneShot(enemyContactSound);
            for (int i = 0; i < 2 && congaLine.Count > 0; i++) {
                int lastIdx = congaLine.Count - 1;
                Transform cat = congaLine[lastIdx];
                congaLine.RemoveAt(lastIdx);
                cat.parent.GetComponent<CatController>().ExitConga();
            }
            if (--lives <= 0) {
                Application.LoadLevel("LoseScene");
            }
        }
    }

    // Use this for initialization
    void Start () {
		moveDirection = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {

        if (isInvincible)
        {
            //2
            timeSpentInvincible += Time.deltaTime;

            //3
            if (timeSpentInvincible < 3f)
            {
                float remainder = timeSpentInvincible % .3f;
                GetComponent<Renderer>().enabled = remainder > .15f;
            }
            //4
            else
            {
                GetComponent<Renderer>().enabled = true;
                isInvincible = false;
            }
        }

        // 1
        Vector3 currentPosition = transform.position;
		// 2
		if( Input.GetButton("Fire1") ) {
			// 3
			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			// 4
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}

		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );

		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
			                 Quaternion.Euler( 0, 0, targetAngle ), 
			                 turnSpeed * Time.deltaTime );

        EnforceBounds();
    }

    private void EnforceBounds()
    {
        // 1
        Vector3 newPosition = transform.position;
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;

        // 2
        float xDist = mainCamera.aspect * mainCamera.orthographicSize - 0.2f;
        float xMax = cameraPosition.x + xDist;
        float xMin = cameraPosition.x - xDist;

        // 3
        if (newPosition.x < xMin || newPosition.x > xMax)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
            moveDirection.x = -moveDirection.x;
        }
        // TODO vertical bounds

        float yDist = mainCamera.orthographicSize - 0.2f;
        float yMax = cameraPosition.y + yDist;
        float yMin = cameraPosition.y - yDist;

        // 3
        if (newPosition.y < yMin || newPosition.y > yMax)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
            moveDirection.y = -moveDirection.y;
        }

        // 4
        transform.position = newPosition;
    }
}
