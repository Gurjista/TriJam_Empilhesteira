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
    private InputAction _grabAction;
    private float leftMovementInput; // Armazena o valor do input de movimento para a esquerda
    private float rightMovementInput; // Armazena o valor do input de movimento para a direita

    // Coisas do GrabObject
    [SerializeField] private Transform _grabPoint;

    private float _grabInput;
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private float _rayDistance = 5f;

    private GameObject _grabbedObject;
    private int layerIndex;
    private RaycastHit2D hitInfo;

    private void Awake()
    {
        inputActions = new InputActions(); // Inicializa o InputActions
        inputActions.Enable(); // Habilita o InputActions
    }

    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Object");
    }

    private void OnEnable()
    {
        _leftMoveAction = inputActions.Player.MovementLeft;
        _rightMoveAction = inputActions.Player.MovementRight;
        _grabAction = inputActions.Player.Grab;

        _leftMoveAction.performed += OnLeftMovement; // Assina o evento de movimento para a esquerda
        _leftMoveAction.canceled += OnLeftMovement; // Assina o evento de cancelamento de movimento para a esquerda

        _rightMoveAction.performed += OnRightMovement; // Assina o evento de movimento para a direita
        _rightMoveAction.canceled += OnRightMovement; // Assina o evento de cancelamento de movimento para a direita

        _grabAction.performed += OnGrab;
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

        _grabAction.performed -= OnGrab;
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

    private void OnGrab(InputAction.CallbackContext context)
    {
        _grabInput = context.ReadValue<float>();

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);
            if (_grabInput > 0 && _grabbedObject == null)
            {
                Debug.Log("Grabbed object");
                _grabbedObject = hitInfo.collider.gameObject;
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                _grabbedObject.transform.position = _grabPoint.position;
                _grabbedObject.transform.SetParent(transform);
            }
            else if (_grabInput > 0)
            {
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                _grabbedObject.transform.SetParent(null);
                _grabbedObject = null;
            }
        }
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(_rayPoint.position, transform.up, _rayDistance);

        Debug.DrawRay(_rayPoint.position, transform.up * _rayDistance, Color.cyan);

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