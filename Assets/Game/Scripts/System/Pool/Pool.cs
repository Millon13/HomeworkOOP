using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool : MonoBehaviour //сделать сиглтоном(легковес)
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private int _poolSize;

    private readonly Stack<GameObject> _pool = new();


    public void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject item = CreateNewItem();
            item.gameObject.SetActive(false);
            _pool.Push(item);
        }
    }

    public T Get<T>() where T : Component
    {
        GameObject item = _pool.Count > 0 ? _pool.Pop() : CreateNewItem();
        item.SetActive(true);
        return item.GetComponent<T>();
    }

    public GameObject Get()
    {
        GameObject item = _pool.Count > 0 ? _pool.Pop() : CreateNewItem();
        item.SetActive(true);
        return item;
    }

    public void Return(GameObject item)
    {
        if (item == null) return;
        item.gameObject.SetActive(false);
        _pool.Push(item);
    }


    public void ReturnToPool(GameObject item)
    {
        if (item == null) return;

        item.gameObject.SetActive(false);
        _pool.Push(item);
    }


    private GameObject CreateNewItem()
    {
        return Instantiate(_prefab);
    }
}