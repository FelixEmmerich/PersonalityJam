using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectSpawner : MonoBehaviour
{

    public GameObject Disc;
    public List<GameObject> FlyingObjectPrefabs;
    public float SpawnTime;
    public float SpawnRadius;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn(GameObject prefab, Vector2 center, float radius)
    {
        Vector2 location = GetRandomPointOnCircle(center, radius);
        GameObject go = Instantiate(prefab);
        go.transform.position = location;
    }

    public void Spawn()
    {
        GameObject objectToSpawn = FlyingObjectPrefabs[Random.Range(0,FlyingObjectPrefabs.Count-1)];
        Spawn(objectToSpawn, Disc.transform.position, SpawnRadius);
    }

    public Vector2 GetRandomPointOnCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0, 360);

        float x = center.x + radius * Mathf.Cos(angle);
        float y = center.y + radius * Mathf.Sin(angle);

        return new Vector2(x, y);
    }

    private IEnumerator SpawnAfterTime (float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            Spawn();
            time = GetNextSpawnTime(); 
        }
    }

    public float GetNextSpawnTime()
    {
        //Placeholder
        return 1;
    }
}
