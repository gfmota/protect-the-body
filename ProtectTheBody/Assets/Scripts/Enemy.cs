using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject rewardScore;
    private GameObject manager;
    private Animator anim;
    public string[] enemyAnimations;
    public int speed;
    enum EnemyAnimation
    {
        Idle,
        Die
    }
    private bool tween = false;

    public abstract void Setup();

    public void Start()
    {
        Setup();
        manager = GameObject.Find("Body");
        anim = gameObject.GetComponent<Animator>();
        anim.Play(enemyAnimations[(int) EnemyAnimation.Idle]);
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo animation = anim.GetCurrentAnimatorStateInfo(0);
        if (animation.IsName(enemyAnimations[(int)EnemyAnimation.Die]) && animation.length <= animation.normalizedTime && !tween)
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

        anim.Play(enemyAnimations[(int) EnemyAnimation.Die]);

        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }
}
