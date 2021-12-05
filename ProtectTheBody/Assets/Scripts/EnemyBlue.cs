using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : Enemy
{
    public override void Setup()
    {
        body = GetComponent<Rigidbody2D>();
        
        enemyAnimations = new string[2] { "enemyBlueIdle", "enemyBlueDie" };
    }

    private void FixedUpdate()
    {
        
        Vector2 direction = -transform.position.normalized;

        Vector2 centripetalForceDirection = (Vector3.zero - transform.position).normalized;
        // Get centripetal force perpendicular direction
        Vector2 tangentVelocityDirection = new Vector2(centripetalForceDirection.y, -centripetalForceDirection.x);
        body.velocity = (tangentVelocityDirection + direction/10) * Time.deltaTime * speed;
    }
}
