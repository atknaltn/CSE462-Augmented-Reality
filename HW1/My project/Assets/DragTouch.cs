using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTouch : MonoBehaviour
{
    private Vector3 touchPosition;
    private Rigidbody rb;
    private Vector3 direction;
    private float moveSpeed = 75f;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            if (touch.phase == TouchPhase.Ended)
                rb.velocity = Vector2.zero;
        }
    }
}