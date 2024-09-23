using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActions _inputActions;

    private static InputManager _instance;
    public static InputManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            {
                Destroy(gameObject);
            }
        }
    }
}