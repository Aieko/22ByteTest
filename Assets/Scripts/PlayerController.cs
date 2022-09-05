using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float forceAmount = 500;

    
    [SerializeField]
    private Rigidbody selectedRigidbody;
    private Camera targetCamera;
    private Vector3 originalScreenTargetPosition;
    private Vector3 originalRigidbodyPos;
    private float selectionDistance = 5;

    private bool screenWasTouched => Input.touchCount > 0;

    // Start is called before the first frame update
    void Start()
    {
        targetCamera = Camera.main;
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (screenWasTouched && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            originalScreenTargetPosition = targetCamera
                .ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, selectionDistance));

            originalRigidbodyPos = selectedRigidbody.transform.position;
        }
    }

    void FixedUpdate()
    {
        if (screenWasTouched)
        {
            Vector3 mousePositionOffset = targetCamera
                .ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, selectionDistance)) - originalScreenTargetPosition;

            selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.fixedDeltaTime;
        }
    }

}
