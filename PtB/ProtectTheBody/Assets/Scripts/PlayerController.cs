using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     * Variables to control hero's movement
     */
    // Publics
    public Rigidbody2D playerRb2d;
    public GameObject body;
    public float thrust = 5f;

    // Privates
    private Vector2 widthCameraRange;
    private Vector2 heightCameraRange;
    private Enemy currentEnemy;
    private  Transform playerControlTransform;
    private Transform playerTransform;
    private Animator animator;
    private Vector2 direction;
    private float angle;
    private bool canJump = false;
    private bool onEnemy = false;

    /*
     * Variables to control where the player
     * is touching the screen
     */
    private enum screenSides
    {
        INIT = 0,
        RIGHT,
        LEFT
    };

    private screenSides screenSide = screenSides.INIT;

    // Start is called before the first frame update
    void Start()
    {
        // Gets player's transform
        playerTransform = this.transform;

        // Gets PlayerControl's transform
        playerControlTransform = playerTransform.parent.transform;

        // Gets Animator
        animator = this.GetComponent<Animator>();

        // Gets Screen's size
        widthCameraRange = new Vector2(Screen.width * (-0.05f), Screen.width * (1.05f));
        heightCameraRange = new Vector2(Screen.height * (-0.05f), Screen.height * (1.05f));
    }

    // Update is called once per frame
    void Update()
    {
        // If player is touching the screen
        if (Input.touchCount > 0 && !animator.GetBool("jumping"))
        {
            // Gets touch position
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            // Gets which side player is touching
            screenSide = touchPos.x > playerControlTransform.position.x ? screenSides.RIGHT : screenSides.LEFT;

            // Gets direction and angle
            direction = touchPos - playerControlTransform.position;
            angle = Vector3.Angle(Vector3.up, direction);

            // Prepares to jump
            if (!canJump) canJump = true;
        }

        // If player is out of the screen
        Vector2 posInScreen = Camera.main.WorldToScreenPoint(playerTransform.position);
        if (posInScreen.x < widthCameraRange.x || posInScreen.x > widthCameraRange.y ||
            posInScreen.y < heightCameraRange.x || posInScreen.y > heightCameraRange.y)
        {
            UpdatePosition(body, false);
        }
    }

    void FixedUpdate()
    {
        /*
         * Assigns different angle depending on
         * where the player is touching
         */
        switch (screenSide)
        {
            case screenSides.INIT:
                playerControlTransform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case screenSides.LEFT:
                playerControlTransform.eulerAngles = new Vector3(0, 0, angle);
                break;
            case screenSides.RIGHT:
                playerControlTransform.eulerAngles = new Vector3(0, 0, -angle);
                break;
        }

        /*
         * Player's movement implementation
         */
        if (Input.touchCount == 0 && canJump)
        {
            // Adds force to hero
            playerRb2d.AddForce(direction.normalized * thrust, ForceMode2D.Impulse);

            // Kills Enemy if Player is on Enemy
            if (currentEnemy != null) currentEnemy.Die();

            // Sets Animator's variable to true
            animator.SetBool("jumping", true);

            canJump = false;
        }
    }

    /*
     * Function to update Player's position
     */
    public void UpdatePosition(GameObject gameObject, bool enemyCollided)
    {
        /*
         * If collided with Enemy, we need to get Enemy's GameObject
         * and sinalize that Player is currently on Enemy with a bool 
         */
        // Gets Enemy's GameObject
        if (enemyCollided) currentEnemy = gameObject.GetComponent<Enemy>();

        // Sinalizes that Player is on Enemy
        onEnemy = enemyCollided;

        // Gets circle collider radius
        float radius = gameObject.GetComponent<CircleCollider2D>().radius + 0.1f;

        // Updates PlayerControl's position
        playerControlTransform.position = gameObject.transform.position;

        // Stops PLayer
        playerRb2d.velocity = Vector2.zero;

        // Updates Player's position
        playerTransform.localPosition = Vector3.ClampMagnitude(Vector3.up * radius, 0.4f);

        // Updates PlayerControl's angle
        playerControlTransform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // Isn't jumping anymore
        animator.SetBool("jumping", false);
    }
}
