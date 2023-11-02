using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    public static ObjectPooling Instance;
    private int amountEffectPool = 100;
    [SerializeField] private GameObject[] effects;
    private Dictionary<string, List<GameObject>> pooledObjects = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        Instance = this;
        InitializeObjectPools();
    }

    private void InitializeObjectPools()
    {
        foreach (GameObject effect in effects)
        {
            List<GameObject> effectPool = new List<GameObject>();

            for (int i = 0; i < amountEffectPool; i++)
            {
                GameObject obj = Instantiate(effect);
                obj.SetActive(false);
                effectPool.Add(obj);
                obj.transform.parent = transform;
            }

            // Use the effect's name as the key in the dictionary
            pooledObjects[effect.name] = effectPool;
        }
    }

    public GameObject GetPooledObject(string effectName)
    {
        if (pooledObjects.ContainsKey(effectName))
        {
            List<GameObject> pool = pooledObjects[effectName];

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }
        }
        return null;
    }

    public void ReturnToPool(GameObject obj, float delay)
    {
        StartCoroutine(ReturnToPoolCoroutine(obj, delay));
    }

    private IEnumerator ReturnToPoolCoroutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}

