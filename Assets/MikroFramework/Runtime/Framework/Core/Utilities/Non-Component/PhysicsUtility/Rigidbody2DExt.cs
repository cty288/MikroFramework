using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mikrocosmos
{
    public static class Rigidbody2DExt
    {

        public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Impulse)
        {
            var explosionDir = rb.position - explosionPosition;
            float explosionDistance = (explosionDir.magnitude / explosionRadius);

            // Normalize without computing magnitude again
            if (upwardsModifier == 0)
            {
                explosionDir /= explosionDistance;
            }
            else
            {
                // If you pass a non-zero value for the upwardsModifier parameter, the direction
                // will be modified by subtracting that value from the Y component of the centre point.
                explosionDir.y += upwardsModifier;
                explosionDir.Normalize();
            }
           Vector2 force = Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir;
            if (!float.IsNaN(force.magnitude))
            {
               rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
            }
           
        }
    }
}
