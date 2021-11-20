using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public PlayerController playerController;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.transform.parent.GetComponent<Animator>();
    }

    /*
     * Detects collision on Player
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*
         * If player is colliding with layer "Enemy"
         * and is jumping
         */
        int layer = collider.gameObject.layer;

        // Gets Hero's transform inside collided GameObject
        GameObject playerControlInside = collider.gameObject;

        // If player isn't jumping, just return
        if (!animator.GetBool("jumping"))
        {
            return;
        } else if (layer == 6)
        {
            // Updates Hero's position
            playerController.UpdatePosition(playerControlInside, false);
        } else if (layer == 7)
        {
            collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // Updates Hero's position
            playerController.UpdatePosition(playerControlInside, true);
        }
    }
}
