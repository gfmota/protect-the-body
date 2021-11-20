using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Vector2 secondDirection;
    private Rigidbody2D body;
    public int speed;
    private float timeLeft = 0;
    private GameObject manager;
    private Animator anim;
    public GameObject rewardScore;

    private bool tween = false;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 direction = -transform.position.normalized;
        body.AddForce(direction * speed, ForceMode2D.Force);
        secondDirection = new Vector2(direction.y * 0.0015f, -direction.x * 0.0015f);
        body.AddForce(new Vector2(-direction.y, direction.x), ForceMode2D.Force);
        body.AddForce(secondDirection * speed, ForceMode2D.Force);
        timeLeft = 1f;

        manager = GameObject.Find("Body");
        anim = gameObject.GetComponent<Animator>();
        anim.Play("enemy2Idle");
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

        AnimatorStateInfo animation = anim.GetCurrentAnimatorStateInfo(0);
        if (animation.IsName("enemy2Die") && animation.length <= animation.normalizedTime && !tween)
        {
            LeanTween.alpha(gameObject, 0, .8f);
            tween = true;
        }
        if (tween && !LeanTween.isTweening())
        {
            Destroy(gameObject);
        }
    }

    public void Die()
    {
        manager.GetComponent<ScoreManager>().ChangeScore(10);

        GameObject score = Instantiate(rewardScore);
        score.transform.SetParent(GameObject.Find("Canvas").transform, false);
        score.GetComponent<RewardScore>().Setup(this.transform.position, 10);

        anim.Play("enemy2Die");

        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }
}
