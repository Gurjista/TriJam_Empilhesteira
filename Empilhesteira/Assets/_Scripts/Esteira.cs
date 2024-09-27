using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esteira : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verifica se o objeto tem a tag "Object"
        if (other.CompareTag("Object"))
        {
            // Move o objeto para a direita
            other.transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }
    }
}