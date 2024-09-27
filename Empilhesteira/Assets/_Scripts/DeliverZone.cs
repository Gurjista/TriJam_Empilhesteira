using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverZone : MonoBehaviour
{
    public GameEvent OnDeliver;
    [SerializeField] private float _timeToAdd = 15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.tag == "Object")
        {
            OnDeliver.Raise(this, _timeToAdd);
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}