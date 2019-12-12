using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LevelGen : MonoBehaviour
{
    [SerializeField] private Transform genPoint;
    [SerializeField] private GameObject floorPrefab, bgPrefab, obsPrefab, pickupPrefab;
    [SerializeField] private float floorOffset, dist;

    private bool canSpawn = true;
    private float width;


    // Start is called before the first frame update
    void Start()
    {
        //Obtain width for spawning dist
        width = floorPrefab.GetComponent<SpriteRenderer>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < genPoint.position.x)
        {
            transform.position = new Vector2(transform.position.x + width + dist, transform.position.y);
            Instantiate(floorPrefab, new Vector2(transform.position.x, transform.position.y + floorOffset), transform.rotation);
            Instantiate(bgPrefab, transform.position, transform.rotation);

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
    }

    IEnumerator SpawnObs()
    {
        Instantiate(obsPrefab, new Vector2(transform.position.x, transform.position.y + floorOffset + 0.08f), transform.rotation);
        yield return new WaitForSeconds(1);
        canSpawn = true;
    }

    IEnumerator SpawnPickup()
    {
        Instantiate(pickupPrefab, new Vector2(transform.position.x, transform.position.y + floorOffset + 0.33f), transform.rotation);
        yield return new WaitForSeconds(1);
        canSpawn = true;
    }
}
