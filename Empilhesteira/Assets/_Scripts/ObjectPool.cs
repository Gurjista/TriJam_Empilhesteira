using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private List<GameObject> _pooledObjects = new List<GameObject>();
    private int _poolSize = 30;

    [SerializeField] private GameObject _boxPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject box = Instantiate(_boxPrefab);
            box.SetActive(false);
            _pooledObjects.Add(box);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}