using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Vector2 movement = new Vector2();

    private Animator animator = default;
    private Rigidbody2D rb2D = default;

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
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Right);
        }
        else if (movement.x < 0)
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Left);
        }
        else if (movement.y > 0)
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Up);
        }
        else if (movement.y < 0)
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Down);
        }
        else
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Idle);
        }
    }

    private void MoveCharacter()
    {
#if UNITY_EDITOR
        movement.x = Input.GetAxisRaw(K.horizontal);
        movement.y = Input.GetAxisRaw(K.vertical);
        movement.Normalize();
        rb2D.velocity = movement * speed;
#endif
    }

}
