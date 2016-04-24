using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnMgr : MonoBehaviour {

    [SerializeField]
    Transform sPlayer;

    [SerializeField]
    Transform sDropPod;

    [SerializeField]
    float sDropPodSpawnHeight = 10;

    [SerializeField]
    int sMinXSpawnDist;
    [SerializeField]
    int sMaxXSpawnDist;

    [SerializeField]
    int sMinZSpawnDist;
    [SerializeField]
    int sMaxZSpawnDist;

    [SerializeField]
    int sMinSpawnTime;
    [SerializeField]
    int sMaxSpawnTime;

    [SerializeField]
    int sMinItemsFound;
    [SerializeField]
    int sMaxItemsFound;

    // this is shit
    static Transform player;
    static Transform dropPod;
    static float dropPodSpawnHeight = 10;

    static int minXSpawnDist;
    static int maxXSpawnDist;

    static int minZSpawnDist;
    static int maxZSpawnDist;

    static int minSpawnTime;
    static int maxSpawnTime;

    static int minItemsFound;
    static int maxItemsFound;

    private static float m_fAccSpawnTime = 0;
    private static int m_iCurrSpawnTime = 0;

    static List<Transform> m_spawnedInstances;

    void Awake()
    {
        m_spawnedInstances = new List<Transform>();
    }

    // Use this for initialization
    void Start () {
        m_spawnedInstances.Clear();
        m_iCurrSpawnTime = GetSpawnTime();

        // this is shit
        player = sPlayer;
        dropPod = sDropPod;
        dropPodSpawnHeight = sDropPodSpawnHeight;

        minXSpawnDist = sMinXSpawnDist;
        maxXSpawnDist = sMaxXSpawnDist;

        minZSpawnDist = sMinZSpawnDist;
        maxZSpawnDist = sMaxZSpawnDist;

        minSpawnTime = sMinSpawnTime;
        maxSpawnTime = sMaxSpawnTime;

        minItemsFound = sMinItemsFound;
        maxItemsFound = sMaxItemsFound;
    }
	
	// Update is called once per frame
	void Update () {
        m_fAccSpawnTime += Time.deltaTime;

        if (m_fAccSpawnTime > m_iCurrSpawnTime)
        {
            SpawnDropPod();
        }
	}

    public static Resource.ResourceType GetRandomResource()
    {
        // hardcode ftw
        int first = (int)Resource.ResourceType.OXYGEN;
        int last  = (int)Resource.ResourceType.ENERGY;

        return (Resource.ResourceType)Random.Range(first, last); 
    }

    public static int GetRandomAmount()
    {
        return Random.Range(minItemsFound, maxItemsFound);
    }

    public static int GetSpawnTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }

    public static float GetXSpawnDistance(float playerX)
    {
        int distVal = Random.Range(minZSpawnDist, maxZSpawnDist);
        bool sign = Random.Range(0, 100) >= 50 ? true : false;
        if (!sign)
            distVal *= -1;
        return playerX + distVal;
    }

    public static float GetZSpawnDistance(float playerZ)
    {
        int distVal = Random.Range(minZSpawnDist, maxZSpawnDist);
        bool sign = Random.Range(0, 100) >= 50 ? true : false;
        if (!sign)
            distVal *= -1;
        return playerZ + distVal;
    }

    private static void SpawnDropPod()
    {
        Vector3 playerPos = player.position;

        float dropPodPosX = GetXSpawnDistance(playerPos.x);
        float dropPodPosZ = GetZSpawnDistance(playerPos.z);

        Transform dropPodInstance = (Transform)Instantiate(dropPod, 
            new Vector3(dropPodPosX, playerPos.y + dropPodSpawnHeight, dropPodPosZ), 
            Quaternion.identity);
        //dropPodInstance.transform.SetParent(this.transform, false);
        m_spawnedInstances.Add(dropPodInstance);

        // reset accumulator and get new spawn time
        m_fAccSpawnTime = 0;
        m_iCurrSpawnTime = GetSpawnTime();
    }

    public static void RemoveDropPod(Transform dropPodInstance)
    {
        m_spawnedInstances.Remove(dropPodInstance);
    }
}
