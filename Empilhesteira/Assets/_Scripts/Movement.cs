using UnityEngine;
using UnityEngine.InputSystem; // Importar o novo Input System

public class Movement : MonoBehaviour
{
    public float rotateSpeed = 100f; // Velocidade de rotação
    public Transform rotateAroundLeft; // Transform para rotacionar ao redor da esquerda
    public Transform rotateAroundRight; // Transform para rotacionar ao redor da direita

    private InputActions inputActions; // Referência para o InputActions
    private InputAction _leftMoveAction;
    private InputAction _rightMoveAction;
    private float leftMovementInput; // Armazena o valor do input de movimento para a esquerda
    private float rightMovementInput; // Armazena o valor do input de movimento para a direita

    private void Awake()
    {
        inputActions = new InputActions(); // Inicializa o InputActions
        inputActions.Enable(); // Habilita o InputActions
    }

    private void OnEnable()
    {
        _leftMoveAction = inputActions.Player.MovementLeft;
        _rightMoveAction = inputActions.Player.MovementRight;

        _leftMoveAction.performed += OnLeftMovement; // Assina o evento de movimento para a esquerda
        _leftMoveAction.canceled += OnLeftMovement; // Assina o evento de cancelamento de movimento para a esquerda

        _rightMoveAction.performed += OnRightMovement; // Assina o evento de movimento para a direita
        _rightMoveAction.canceled += OnRightMovement; // Assina o evento de cancelamento de movimento para a direita

        //inputActions.Player.Movement.Left.performed += OnLeftMovementPerformed; // Assina o evento de movimento para a esquerda
        //inputActions.Player.Movement.Left.canceled += OnLeftMovementCanceled; // Assina o evento de cancelamento de movimento para a esquerda
        //inputActions.Player.Movement.Right.performed += OnRightMovementPerformed; // Assina o evento de movimento para a direita
        //inputActions.Player.Movement.Right.canceled += OnRightMovementCanceled; // Assina o evento de cancelamento de movimento para a direita
    }

    private void OnDisable()
    {
        _leftMoveAction.performed -= OnLeftMovement;
        _leftMoveAction.canceled -= OnLeftMovement;

        _rightMoveAction.performed -= OnRightMovement;
        _rightMoveAction.canceled -= OnRightMovement;
        //inputActions.Player.Left.performed -= OnLeftMovementPerformed; // Remove a assinatura do evento de movimento para a esquerda
        //inputActions.Player.Left.canceled -= OnLeftMovementCanceled; // Remove a assinatura do evento de cancelamento de movimento para a esquerda
        //inputActions.Player.Right.performed -= OnRightMovementPerformed; // Remove a assinatura do evento de movimento para a direita
        //inputActions.Player.Right.canceled -= OnRightMovementCanceled; // Remove a assinatura do evento de cancelamento de movimento para a direita
        //inputActions.Player.Disable(); // Desabilita o InputActions
    }

    public void OnLeftMovement(InputAction.CallbackContext context)
    {
        leftMovementInput = context.ReadValue<float>(); // Lê o valor do input de movimento para a esquerda
    }

    //public void OnLeftMovementCanceled(InputAction.CallbackContext context)
    //{
    //    leftMovementInput = 0f; // Reseta o valor do input de movimento para a esquerda
    //}

    public void OnRightMovement(InputAction.CallbackContext context)
    {
        rightMovementInput = context.ReadValue<float>(); // Lê o valor do input de movimento para a direita
    }

    //public void OnRightMovementCanceled(InputAction.CallbackContext context)
    //{
    //    rightMovementInput = 0f; // Reseta o valor do input de movimento para a direita
    //}

    private void Update()
    {
        if (leftMovementInput > 0)
        {
            // Rotaciona para a esquerda
            this.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            this.transform.RotateAround(rotateAroundLeft.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        }
        else if (leftMovementInput < 0)
        {
            // Rotaciona para a direita
            this.transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
            this.transform.RotateAround(rotateAroundLeft.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
        }

        if (rightMovementInput > 0)
        {
            // Rotaciona para a esquerda
            this.transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
            this.transform.RotateAround(rotateAroundRight.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
        }
        else if (rightMovementInput < 0)
        {
            // Rotaciona para a direita
            this.transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
            this.transform.RotateAround(rotateAroundRight.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        }
    }
}