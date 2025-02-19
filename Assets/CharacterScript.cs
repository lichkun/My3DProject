using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    private Animator animator;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector3 playerVelocity;
    private float jumpForce = 5f;

    private enum State
    {
        Idle = 0,
        Walk = 1,
        JumpStart = 2,
        Jump = 3,
        JumpFinish = 4
    }

    private bool isJumping = false;
    private bool isFalling = false;
    private bool isGrounded = true;
    private State moveState = State.Idle;

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
            SetMoveState(State.JumpStart);
            isJumping = true;
            isGrounded = false;
            playerVelocity.y = jumpForce;
        }

        if (isJumping && IsJumpStartAnimationComplete())
        {
            SetMoveState(State.Jump);
            isJumping = false;
            isFalling = true;
        }

        if (isFalling && isGrounded)
        {
            playerVelocity.y = 0f;
            SetMoveState(State.JumpFinish);
            isFalling = false;
        }

        if (moveState == State.JumpFinish && IsJumpFinishAnimationComplete())
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

    private bool IsJumpStartAnimationComplete()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("JumpStart") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    private bool IsJumpFinishAnimationComplete()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("JumpFinish") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    private void SetMoveState(State state)
    {
        moveState = state;
        animator.SetInteger("MoveState", (int)state);
    }
    private void OnJumpStartAnimationEnds()
    {
        animator.SetInteger("MoveState", (int)MoveState.Jump);
        Debug.Log("Jumping");
    }
    private void OnJumpFinishAnimationEnds()
    {
        animator.SetInteger("MoveState", (int)MoveState.Idle);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger Enter with: {other.name}");
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Landed on Ground");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Trigger Exit with: {other.name}");
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Left the Ground");
        }
    }
}
