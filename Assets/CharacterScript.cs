using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    private Animator animator;
    private InputAction moveAction;
    private InputAction jumpAction;

    private enum State
    {
        Idle = 0,
        Walk = 1,
        Jump = 2
    }

    private bool isJumping = false;
    private bool isGrounded = true; // Дополнительная проверка на землю

    void Awake()
    {
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        if (jumpAction.ReadValue<float>() > 0f && isGrounded) 
        {
            SetMoveState(State.Jump);
            isJumping = true;
        }

        if (isJumping)
        {
            if (IsJumpingAnimationComplete()) 
            {
                isJumping = false;
                TransitionToIdleOrWalk();
            }
        }
        else
        {
            TransitionToIdleOrWalk();
        }
    }

    private void TransitionToIdleOrWalk()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.magnitude > 0)
        {
            SetMoveState(State.Walk);
        }
        else
        {
            SetMoveState(State.Idle);
        }
    }

    private bool IsJumpingAnimationComplete()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    private void SetMoveState(State state)
    {
        animator.SetInteger("MoveState", (int)state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
