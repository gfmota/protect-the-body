using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    private Vector2 secondDirection;
    private float timeLeft = 0;

    public override void Setup()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 direction = -transform.position.normalized;
        body.AddForce(direction * speed, ForceMode2D.Force);
        secondDirection = new Vector2(direction.y * 0.0015f, -direction.x * 0.0015f);
        body.AddForce(new Vector2(-direction.y, direction.x), ForceMode2D.Force);
        body.AddForce(secondDirection * speed, ForceMode2D.Force);
        timeLeft = 1f;

        enemyAnimations = new string[2] { "enemyRedIdle", "enemyRedDie" };
    }

    private void FixedUpdate()
    {
        if (timeLeft <= 0)
        {
            secondDirection = -secondDirection;
            timeLeft = 2f;
        }
        timeLeft -= Time.deltaTime;
        body.AddForce(secondDirection * speed, ForceMode2D.Impulse);
    }
}
