using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    [SerializeField] private float pursuitSpeed;
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float directionChangeInterval;

    private Coroutine moveCoroutine;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Transform targetTransform = null;
    private Vector3 endPosition;
    private float currentSpeed;
    private float currentAngle = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        currentSpeed = wanderSpeed;

        StartCoroutine(WanderRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(K.player))
        {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(K.player))
        {
            animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Idle);

            currentSpeed = wanderSpeed;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            targetTransform = null;
        }
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndpoint();

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move());

            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    private IEnumerator Move()
    {
        float remainingDistance = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistance > float.Epsilon)
        {
            if (targetTransform != null)
            {
                endPosition = targetTransform.position;
            }

            if (rb2d != null)
            {
                MoveState direction = (transform.position - endPosition).x > 0 ? MoveState.Left : MoveState.Right;
                animator.SetInteger(K.AnimationKey.animationState, (int)direction);

                Vector3 newPosition = Vector3.MoveTowards(rb2d.position, endPosition, currentSpeed * Time.deltaTime);

                rb2d.MovePosition(newPosition);

                remainingDistance = (transform.position - endPosition).sqrMagnitude;
            }

            yield return new WaitForFixedUpdate();
        }

        animator.SetInteger(K.AnimationKey.animationState, (int)MoveState.Idle);
    }

    private void ChooseNewEndpoint()
    {
        currentAngle += Random.Range(0, 360);
        currentAngle = Mathf.Repeat(currentAngle, 360);
        endPosition += Vector3FromAngle(currentAngle);
    }

    private Vector3 Vector3FromAngle(float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians), 0);
    }

}
