using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    public GameEvent OnDestroy;
    [SerializeField] private float _timeToAdd = -20f;
    [SerializeField] private float _timeToDeactivate = 20f;
    private float _timer;

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
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnDestroy.Raise(this, _timeToAdd);
            _timer = 0f;
            gameObject.SetActive(false);
        }
    }
}