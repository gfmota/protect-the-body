using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D body;
    public int speed;
    private GameObject manager;
    private Animator anim;
    public GameObject rewardScore;

    //private float lifetime = 0f;
    //private bool dead = false;
    private bool tween = false;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        direction = -transform.position.normalized;
        body.AddForce(direction * speed, ForceMode2D.Force);

        manager = GameObject.Find("Body");
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        /*lifetime += Time.deltaTime;
        if (lifetime >= 5 && !dead)
        {
            Die();
            dead = true;
        }*/
        AnimatorStateInfo animation = anim.GetCurrentAnimatorStateInfo(0);
        if (animation.IsName("enemyDie") && animation.length <= animation.normalizedTime && !tween)
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

        anim.Play("enemyDie");

        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }
}
