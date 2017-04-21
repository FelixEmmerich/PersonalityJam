using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{

    public GameObject DiscRef;
    public GraphicsManager GraphicsManagerRef;
    public float BaseSpawnTime=1;
    private float _currentSpawnTime;
    public float SpawnRadius;
    public bool CanSpawn = true;
    private Coroutine _spawnRoutine;

	// Use this for initialization
	void Start ()
    {
        if (DiscRef == null)
        {
            DiscRef = Disc.Instance.gameObject;
        }
        if (GraphicsManagerRef == null)
        {
            GraphicsManagerRef = GraphicsManager.Instance;
        }
        _currentSpawnTime = BaseSpawnTime;
        if (CanSpawn)
        {
            SetAutoSpawnActive(true);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void SetAutoSpawnActive(bool active)
    {
        CanSpawn = active;

        if (!active)
        {
            StopCoroutine(_spawnRoutine);
            Debug.Log("Return");
            return;
        }
        _spawnRoutine = StartCoroutine(SpawnAfterTime(BaseSpawnTime));
    }

    public void Spawn(GameObject prefab, Vector2 center, float radius)
    {
        if (Disc.Instance!=null)
        {
            Vector2 location = GetRandomPointOnCircle(center, radius);
            GameObject go = Instantiate(prefab);
            go.transform.position = location; 
        }
    }

    public void Spawn()
    {
        if (Disc.Instance!=null)
        {
            int rnd = Random.Range(0, GraphicsManagerRef.BubblePrefabs.Length);
            GameObject objectToSpawn = GraphicsManagerRef.BubblePrefabs[rnd];
            Spawn(objectToSpawn, DiscRef.transform.position, SpawnRadius);
        }
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
        while (CanSpawn)
        {
            yield return new WaitForSeconds(time);
            Spawn();
            time = GetNextSpawnTime(); 
        }
        SetAutoSpawnActive(false);
        yield break;
    }

    public float GetNextSpawnTime()
    {
        //Placeholder
        return _currentSpawnTime;
    }
}
