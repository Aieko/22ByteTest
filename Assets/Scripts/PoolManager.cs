using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static PoolManager Instance;

    [SerializeField]
    private List<Pool> projectilePools;
    [SerializeField]
    private List<Pool> particlePools;
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> projectilePoolDictionary;
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> particlesPoolDictionary;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

      
    }

    // Start is called before the first frame update
    void Start()
    {
        AddToPoolDictionary(ref projectilePoolDictionary, projectilePools);
        AddToPoolDictionary(ref particlesPoolDictionary, particlePools);
    }

    private void AddToPoolDictionary(ref Dictionary<string, Queue<GameObject>> poolDictionary, List<Pool> pools )
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);

            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnParticleFromPool(string tag, Vector3 pos, Quaternion rot = new Quaternion())
    {
        if (!particlesPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        var objectToSpawn = particlesPoolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;
        objectToSpawn.SetActive(true);


        particlesPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnRandomProjectileFromPool(Vector3 pos, Quaternion rot = new Quaternion())
    {
        var objectToSpawn = GetRandomPoolOfProjectile().Dequeue();

        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;

        objectToSpawn.GetComponent<Projectile>().ResetTrigger();
        objectToSpawn.GetComponent<Rigidbody>().velocity = Vector3.zero;

        objectToSpawn.SetActive(true);

        projectilePoolDictionary[objectToSpawn.tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    private Queue<GameObject> GetRandomPoolOfProjectile()
    {
        return projectilePoolDictionary.ElementAt(Random.Range(0,projectilePools.Capacity)).Value;

    }
   

}
