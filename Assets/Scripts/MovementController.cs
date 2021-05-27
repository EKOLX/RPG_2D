using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Vector2 movement = new Vector2();

    private Animator animator = default;
    private Rigidbody2D rb2D = default;
    private string animationState = "AnimationState";
    private enum MoveState
    {
        idle = 0,
        left = 1,
        right = 2,
        up = 3,
        down = 4
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void UpdateState()
    {
        if (movement.x > 0)
        {
            animator.SetInteger(animationState, (int)MoveState.right);
        }
        else if (movement.x < 0)
        {
            animator.SetInteger(animationState, (int)MoveState.left);
        }
        else if (movement.y > 0)
        {
            animator.SetInteger(animationState, (int)MoveState.up);
        }
        else if (movement.y < 0)
        {
            animator.SetInteger(animationState, (int)MoveState.down);
        }
        else
        {
            animator.SetInteger(animationState, (int)MoveState.idle);
        }
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb2D.velocity = movement * speed;
    }

}
