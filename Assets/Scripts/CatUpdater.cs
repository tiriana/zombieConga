using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatUpdater : MonoBehaviour
{

    private CatController catController;

    // Use this for initialization
    void Start()
    {
        catController = transform.parent.GetComponent<CatController>();
    }

    void UpdateTargetPosition()
    {
        catController.UpdateTargetPosition();
    }

    void OnBecameInvisible()
    {
        catController.OnBecameInvisible();
    }
}
