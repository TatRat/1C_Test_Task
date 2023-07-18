using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace ObjectsPool
{
    public class Pool<T> where T : PoolObject
    {
        private readonly T _poolObjectPrefab;
        private readonly Transform _container;
        private readonly int _minCapacity;
        private readonly int _maxCapacity;
        private readonly bool _autoExpand;

        private List<T> _pool;

        public Pool(T poolObjectPrefab, Transform container, int minCapacity, int maxCapacity, bool autoExpand)
        {
            _poolObjectPrefab = poolObjectPrefab;
            _container = container;
            _minCapacity = minCapacity;
            _maxCapacity = maxCapacity;
            _autoExpand = autoExpand;

            if (_autoExpand)
                _maxCapacity = Int32.MaxValue;
        }

        public void Initialize() =>
            CreatePool();

        public void Disable()
        {
            foreach (T poolObject in _pool) 
                poolObject.ReturnToPool();
        }
        
        public void Destruct()
        {
            foreach (T poolObject in _pool) 
                UnityEngine.Object.Destroy(poolObject.gameObject);
            
            _pool.Clear();
        }

        private void CreatePool()
        {
            _pool = new List<T>(_minCapacity);
            
            for (int i = 0; i < _minCapacity; i++) 
                CreateElement();
        }

        private T CreateElement(bool isActiveByDefault = false)
        {
            T createdObject = UnityEngine.Object.Instantiate(_poolObjectPrefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);

            return createdObject;
        }

        private bool TryGetElement(out T element, bool isOverloaded = false)
        {
            foreach (T poolObject in _pool)
            {
                if (!poolObject.isActiveAndEnabled)
                {
                    element = poolObject;
                    if (!isOverloaded) 
                        element.gameObject.SetActive(true);
                    
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement(bool isOverloaded = false)
        {
            if (TryGetElement(out T element, isOverloaded))
                return element;

            if (_autoExpand)
                return CreateElement(!isOverloaded);

            if (_pool.Count < _maxCapacity)
                return CreateElement(!isOverloaded);

            throw new Exception("pool is out of objects");
        }

        public T GetFreeElement(Vector3 position, bool isOverloaded = false)
        {
            T element = GetFreeElement(true);
            element.transform.position = position;

            if (!isOverloaded)
                element.gameObject.SetActive(true);

            return element;
        }

        public T GetFreeElement(Vector3 position, Quaternion rotation)
        {
            T element = GetFreeElement(position, true);
            element.transform.rotation = rotation;
            element.gameObject.SetActive(true);

            return element;
        }
    }
}