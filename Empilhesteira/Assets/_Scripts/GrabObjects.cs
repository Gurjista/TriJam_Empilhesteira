using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObjects : MonoBehaviour
{
    //private InputAction _grabAction;
    [SerializeField] private Transform _grabPoint;

    [SerializeField] private Transform _rayPoint;
    [SerializeField] private float _rayDistance = 5f;

    private GameObject _grabbedObject;
    private int layerIndex;
    private RaycastHit2D hitInfo;

    // Start is called before the first frame update
    private void Start()
    {
        layerIndex = LayerMask.NameToLayer("Object");
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null && InputManager.Instance._inputActions != null)
        {
            //_grabAction = InputManager.Instance._inputActions.Player.Grab;
            InputManager.Instance._inputActions.Player.Grab.performed += OnGrab;
            InputManager.Instance._inputActions.Player.Grab.canceled += OnGrab;
        }
        else
        {
            Debug.LogError("InputManager.Instance or InputManager.Instance._inputActions is null");
        }
    }

    private void OnDisable()
    {
        InputManager.Instance._inputActions.Player.Grab.performed -= OnGrab;
        InputManager.Instance._inputActions.Player.Grab.canceled -= OnGrab;
    }

    private void OnGrab(InputAction.CallbackContext context)
    {
        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (_grabbedObject == null)
            {
                _grabbedObject = hitInfo.collider.gameObject;
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                _grabbedObject.transform.position = _grabPoint.position;
                _grabbedObject.transform.SetParent(transform);
            }
            else
            {
                _grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
                _grabbedObject.transform.SetParent(null);
                _grabbedObject = null;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(_rayPoint.position, transform.right, _rayDistance);

        Debug.DrawRay(_rayPoint.position, transform.right * _rayDistance, Color.red);
    }
}