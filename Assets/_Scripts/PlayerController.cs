using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Binding")]
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction runAction;
    [SerializeField] InputAction jumpAction; 

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 20f;
    public bool turnVisualWhenMoveBackward = true; // for setting
    public bool reverseInputWhenMoveBackward = true; // for setting
    [SerializeField] private float currentSpeed;
    private bool isRunning;

    // for jump
    // not require

    [Header("Player State")]
    public PlayerState state;

    [Header("Assigned Components")]
    [SerializeField] private GameObject visualCharacter;
    [SerializeField] private Rigidbody rb;

    // for in-class use
    private Vector2 inputVector;

    private void OnEnable()
    {
        moveAction.Enable();
        runAction.Enable(); 
        jumpAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        runAction.Disable();
        jumpAction.Disable();   
    }
    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;
        state = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;
            
        inputVector = moveAction.ReadValue<Vector2>();
        isRunning = runAction.IsPressed();

        HandleRotation();

        UpdatePlayerState();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        bool isMovingBackward = inputVector.y < 0;

        if (!turnVisualWhenMoveBackward && isMovingBackward)
        {
            currentSpeed = moveSpeed * 0.5f;
        }
        else if (isRunning && inputVector != Vector2.zero)
        {
            currentSpeed = moveSpeed * 1.5f; 
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        float moveX = inputVector.x;
        float moveZ = inputVector.y;

        // reverse A/D when moving backward (base on setting)
        if (isMovingBackward && turnVisualWhenMoveBackward && reverseInputWhenMoveBackward)
        {
            moveX = -inputVector.x; 
        }

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

        moveDirection = transform.TransformDirection(moveDirection) * currentSpeed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }
    private void HandleRotation()
    {
        if (inputVector == Vector2.zero) return;

        Vector3 targetDirection = Vector3.zero;
        bool isMovingBackward = inputVector.y < 0;

        // model turns around when move backward
        if (turnVisualWhenMoveBackward)
        {
            float rotX = inputVector.x;
            if (isMovingBackward && reverseInputWhenMoveBackward)
            {
                rotX = -inputVector.x; 
            }
            targetDirection = new Vector3(rotX, 0, inputVector.y);
        }
        // model does not turn around when move backward
        else
        {
            float rotZ = isMovingBackward ? -inputVector.y : inputVector.y;
            targetDirection = new Vector3(inputVector.x, 0, rotZ);
        }

        // rotate
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            visualCharacter.transform.localRotation = Quaternion.Slerp(
                visualCharacter.transform.localRotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    private void UpdatePlayerState()
    {
        if (inputVector == Vector2.zero)
        {
            state = PlayerState.Idle;
        }
        else
        {
            if (!turnVisualWhenMoveBackward && inputVector.y < 0)
                state = PlayerState.Moving;
            else
                state = isRunning ? PlayerState.Running : PlayerState.Moving;
        }
    }
}
public enum PlayerState { Idle, Moving, Running, Jumping, Falling };