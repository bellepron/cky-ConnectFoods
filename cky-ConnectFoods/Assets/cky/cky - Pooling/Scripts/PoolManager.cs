using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CKY.Pooling
{
    public class PoolManager : SingletonPersistent<PoolManager>
    {
        [System.Serializable]
        public class Pool
        {
            public Transform prefabTr;
            public int size;
        }

        public List<Pool> pools;
        public Dictionary<Transform, Transform> poolHolderDictionary = new Dictionary<Transform, Transform>();
        public Dictionary<int, int> spawnedObjInstaneIdDictionary = new Dictionary<int, int>();
        public Dictionary<Transform, Queue<GameObject>> poolDictionary;

        protected override void OnFirstAwake() => Initialize();

        private void Initialize()
        {
            poolDictionary = new Dictionary<Transform, Queue<GameObject>>();

            for (int k = 0; k < pools.Count; k++)
            {
                var poolHolderTr = new GameObject($"{pools[k].prefabTr.name}'s").transform;
                poolHolderTr.parent = this.transform;
                var pool = pools[k];
                poolHolderDictionary.Add(pool.prefabTr, poolHolderTr);

                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefabTr.gameObject, poolHolderTr);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);

                    spawnedObjInstaneIdDictionary.Add(obj.GetInstanceID(), k);
                }

                poolDictionary.Add(pool.prefabTr, objectPool);
            }
        }

        public GameObject Spawn(Transform prefabTr, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(prefabTr))
            {
                Debug.LogWarning($"Pool doesn't have {prefabTr}");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[prefabTr].First();

            if (objectToSpawn.activeInHierarchy == false)
            {
                objectToSpawn = poolDictionary[prefabTr].Dequeue();
            }
            else
            {
                objectToSpawn = Instantiate(prefabTr.gameObject, poolHolderDictionary[prefabTr]);

                var poolIndex = FindPrefabPoolIndex(prefabTr);
                spawnedObjInstaneIdDictionary.Add(objectToSpawn.GetInstanceID(), poolIndex);
                pools[poolIndex].size++;
            }

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            if (objectToSpawn.TryGetComponent<IPooledObject>(out var pooledObj) == true)
            {
                pooledObj.OnObjectSpawn();
            }

            poolDictionary[prefabTr].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        private int FindPrefabPoolIndex(Transform prefabTr)
        {
            var k = 0;
            for (int i = 0; i < pools.Count; i++)
            {
                if (prefabTr == pools[i].prefabTr)
                {
                    k = pools.IndexOf(pools[i]);
                }
            }
            return k;
        }

        public void Despawn(GameObject pooledObj)
        {
            pooledObj.SetActive(false);

            var a = spawnedObjInstaneIdDictionary[pooledObj.GetInstanceID()];
            var b = pools[a];

            pooledObj.transform.parent = poolHolderDictionary[b.prefabTr];
        }

        public void ResetPool()
        {
            foreach (Queue<GameObject> queue in poolDictionary.Values)
                foreach (GameObject obj in queue)
                    Despawn(obj);
        }
    }
}