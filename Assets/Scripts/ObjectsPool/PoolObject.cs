using UnityEngine;

namespace ObjectsPool
{
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool() =>
            gameObject.SetActive(false);
    }
}