using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Transform playerTransform;

    private float xMin = -5;
    private float xMax = 5;
    private float yMin = -5;
    private float yMax = 5;

    private Vector3 targetPos;

    private void LateUpdate() {
        if (!playerTransform) {
            GameObject playerObj=GameObject.FindWithTag("Player");

            if (playerObj) {
                playerTransform = playerObj.transform;
            }
            else {
                return;
            }
        }


        Vector3 cameraPos = transform.position;
        float isRight = Mathf.Sign(playerTransform.transform.localScale.x);

        var playerPos = playerTransform.position;

        targetPos.x = playerPos.x + 3 * isRight;
        targetPos.y = playerPos.y + 2;
        targetPos.z = -10;

        float smoothSpeed = 5;

        cameraPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        

        transform.position = new Vector3(Mathf.Clamp(cameraPos.x,xMin,xMax),
            Mathf.Clamp(cameraPos.y,yMin,yMax),cameraPos.z);
    }
}
