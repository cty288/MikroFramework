using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using MikroFramework.Utilities;
using UnityEngine;



public class Player : AbstractMikroController<ShootingEditor2D> {
    private Rigidbody2D rigidbody2D;

    private Trigger2DCheck groundCheck;

    private Gun gun;
    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck").GetComponent<Trigger2DCheck>();
        gun = transform.Find("Gun").GetComponent<Gun>();
    }

    private bool jumpPressed;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            gun.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            gun.Reload();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            this.SendCommand<ShiftGunCommand>();
        }
    }

    private void FixedUpdate() {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (horizontalMovement > 0 && transform.localScale.x < 0 ||
            horizontalMovement < 0 && transform.localScale.x > 0) {
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;
        }

        rigidbody2D.velocity = new Vector2(horizontalMovement * 5, rigidbody2D.velocity.y);

        bool grounded = groundCheck.Triggered;

        if (jumpPressed && grounded) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 5);
        }

        jumpPressed = false;
    }

    
}
