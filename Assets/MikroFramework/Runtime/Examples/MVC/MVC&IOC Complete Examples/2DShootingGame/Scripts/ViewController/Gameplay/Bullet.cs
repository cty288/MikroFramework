using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Architecture;
using UnityEngine;

public class Bullet : AbstractMikroController<ShootingEditor2D> {

    private Rigidbody2D rigidbody2D;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    private void Start() {
        float isRight =Mathf.Sign(transform.lossyScale.x);
        rigidbody2D.velocity=Vector2.right*10*isRight;
        StartCoroutine(Destroy(5));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            this.SendCommand<KillEnemyCommand>();
            Destroy(other.gameObject);
            //Destroy(gameObject);
            GameObjectPoolManager.Singleton.Recycle(this.gameObject);
        }
    }


    private IEnumerator Destroy(float time) {
        yield return new WaitForSeconds(time);
        GameObjectPoolManager.Singleton.Recycle(this.gameObject);
    }
}
