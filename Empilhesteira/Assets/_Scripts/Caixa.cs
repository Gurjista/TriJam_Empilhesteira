using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    public GameEvent OnDestroy;
    [SerializeField] private float _timeToAdd = -20f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnDestroy.Raise(this, _timeToAdd);
            gameObject.SetActive(false);
        }
    }
}