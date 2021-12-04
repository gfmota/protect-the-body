using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Enemy
{
    
    // Start is called before the first frame update
    public override void Setup()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 direction = -transform.position.normalized;
        body.AddForce(direction * speed, ForceMode2D.Force);

        enemyAnimations = new string[2] { "enemyGreenIdle", "enemyGreenDie" };
    }
}
