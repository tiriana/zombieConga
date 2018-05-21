using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    private Transform followTarget;
    private float moveSpeed;
    private float turnSpeed;
    private bool isZombie;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start () {
		
	}

    public void JoinConga( Transform followTarget, float moveSpeed, float turnSpeed ) {
        //2
        this.followTarget = followTarget;
        this.moveSpeed = moveSpeed * 2f;
        this.turnSpeed = turnSpeed;

        //3
        isZombie = true;

        Transform cat = transform.GetChild(0);
        cat.GetComponent<Collider2D>().enabled = false;
        cat.GetComponent<Animator>().SetBool("inConga", true);

        targetPosition = followTarget.position;
    }

    // Update is called once per frame
    void Update () {
        if (isZombie) {
            _followTarget();
        }
    }

    public void UpdateTargetPosition() {
        targetPosition = followTarget.position;
    }

    private void _followTarget() {
        //2
        Vector3 currentPosition = transform.position;
        Vector3 moveDirection = targetPosition - currentPosition;
        // Vector3 moveDirection = followTarget.position - currentPosition;

        //3
        float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                               Quaternion.Euler(0, 0, targetAngle),
                                               turnSpeed * Time.deltaTime);

        //4
        float distanceToTarget = moveDirection.magnitude;
        if (distanceToTarget > 0) {
            //5
            if (distanceToTarget > moveSpeed)
                distanceToTarget = moveSpeed;

            //6
            moveDirection.Normalize();
            Vector3 target = moveDirection * distanceToTarget + currentPosition;
            transform.position =
              Vector3.Lerp(currentPosition, target, moveSpeed * Time.deltaTime);
        }
    }

    void GrantCatTheSweetReleaseOfDeath() {
        Object.Destroy(gameObject);
    }

    public void OnBecameInvisible() {
        if (!isZombie) {
            GrantCatTheSweetReleaseOfDeath();
        }
    }
}
