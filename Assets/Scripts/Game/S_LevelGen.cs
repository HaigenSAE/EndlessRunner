using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LevelGen : MonoBehaviour
{
    [SerializeField] private Transform genPoint;
    [SerializeField] private GameObject floorPrefab, bgBuildingPrefab, bgSkyPrefab, obsPrefab, pickupPrefab;
    [SerializeField] private GameObject lastFloor, lastBuildingBG, lastSkyBG;	
    [SerializeField] private float floorOffset;

    private bool canSpawn = true;
    private float width;


    // Start is called before the first frame update
    void Start()
    {
        //Obtain width for spawning dist
        width = 1.2f;
		lastBuildingBG = gameObject;
		lastFloor = gameObject;
		lastSkyBG = gameObject;
		SpawnFloor();
		SpawnBuildingBG();
		SpawnSkyBG();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastFloor.transform.position.x < genPoint.position.x)
        {
			SpawnFloor();
            //randomly spawn obstacles and pickups
            if(canSpawn)
            {
                float rand = Random.value;
                if (rand <= 0.3f)
                {
                    StartCoroutine(SpawnObs());
                    canSpawn = false;
                }
                else if (rand <= 0.6f)
                {
                    StartCoroutine(SpawnPickup());
                    canSpawn = false;
                }
            }
        }
		if(lastBuildingBG.transform.position.x < genPoint.position.x)
		{
			SpawnBuildingBG();
		}
		if(lastSkyBG.transform.position.x < genPoint.position.x)
		{
			SpawnSkyBG();
		}
    }
	
	private void SpawnFloor()
	{
		lastFloor = Instantiate(floorPrefab, new Vector2(lastFloor.transform.position.x + width, transform.position.y + floorOffset), transform.rotation);
	}
	
	private void SpawnBuildingBG()
	{
		lastBuildingBG = Instantiate(bgBuildingPrefab, new Vector3(lastBuildingBG.transform.position.x + width, transform.position.y, 10), transform.rotation);
	}
	
	private void SpawnSkyBG()
	{
		lastSkyBG = Instantiate(bgSkyPrefab, new Vector3(lastSkyBG.transform.position.x + width, transform.position.y, 15), transform.rotation);
	}

    IEnumerator SpawnObs()
    {
        Instantiate(obsPrefab, new Vector2(lastFloor.transform.position.x + width, transform.position.y + floorOffset + 0.08f), transform.rotation);
        yield return new WaitForSeconds(1);
        canSpawn = true;
    }

    IEnumerator SpawnPickup()
    {
        Instantiate(pickupPrefab, new Vector2(lastFloor.transform.position.x + width, transform.position.y + floorOffset + 0.33f), transform.rotation);
        yield return new WaitForSeconds(1);
        canSpawn = true;
    }
}
