using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Utilities;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Trigger2DCheck wallCheck;
    private Trigger2DCheck fallCheck;
    private Trigger2DCheck groundCheck;

    private Rigidbody2D rigidbody2D;

    private void Awake() {
        wallCheck = transform.Find("WallCheck").GetComponent<Trigger2DCheck>();
        fallCheck = transform.Find("FallCheck").GetComponent<Trigger2DCheck>();
        groundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        float scaleX = transform.localScale.x;

        if (groundCheck.Triggered && fallCheck.Triggered && !wallCheck.Triggered) {
            rigidbody2D.velocity = new Vector2(scaleX * 10, rigidbody2D.velocity.y);
        }
        else {
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;
        }

       
    }
}
