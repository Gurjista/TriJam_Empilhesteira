using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    [SerializeField] private float _timeToAdd = -20f;
    [SerializeField] private float _timeToDeactivate = 20f;
    [SerializeField] private float _timer;

    [Header("Events")]
    public GameEvent OnDestroy;

    public GameEvent OnDespawned;

    // Start is called before the first frame update
    private void Start()
    {
        _timer = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeToDeactivate)
        {
            _timer = 0f;
            OnDespawned.Raise(this, null);
            OnDestroy.Raise(this, _timeToAdd);
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnDestroy.Raise(this, _timeToAdd);
            _timer = 0f;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }
    }

    public void OnGrabbed(Component sender, object data)
    {
        _timer = 0f;
    }
}