using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private float _initialMinSpawnTime = 1f;
    [SerializeField] private float _initialMaxSpawnTime = 3f;
    [SerializeField] private float _spawnAcceleration = 0.1f;

    private float _currentMinSpawnTime;
    private float _currentMaxSpawnTime;

    private bool hasBox = false;

    // Start is called before the first frame update
    private void Start()
    {
        _currentMaxSpawnTime = _initialMaxSpawnTime;
        _currentMinSpawnTime = _initialMinSpawnTime;
        StartCoroutine(SpawnBoxRoutine());
    }

    private IEnumerator SpawnBoxRoutine()
    {
        while (true)
        {
            float waitTime = UnityEngine.Random.Range(_currentMinSpawnTime, _currentMaxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f);
            bool hasBox = false;
            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "Object")
                {
                    hasBox = true;
                    break;
                }
            }

            if (!hasBox)
            {
                GameObject box = ObjectPool.Instance.GetPooledObject();
                if (box != null)
                {
                    box.transform.position = transform.position;
                    box.SetActive(true);
                }
            }

            _currentMinSpawnTime = Mathf.Max(0.1f, _currentMinSpawnTime - _spawnAcceleration);
            _currentMaxSpawnTime = Mathf.Max(0.1f, _currentMaxSpawnTime - _spawnAcceleration);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.4f);
    }
}