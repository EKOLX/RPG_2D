using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Transform targetTransform = default;

    private Touch touch;
    private Animator animator = default;
    private Vector3 targetPosition = default;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        if (targetPosition.x > 0)
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Right);
        }
        else if (targetPosition.x < 0)
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Left);
        }
        else if (targetPosition.y > 0)
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Up);
        }
        else if (targetPosition.y < 0)
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
        if (Input.GetMouseButtonUp(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetTransform.position = targetPosition;
        }
#endif
#if UNITY_IOS || UNITY_ANDROID 
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
                targetTransform.position = targetPosition;
            }
        }
#endif
    }

}
