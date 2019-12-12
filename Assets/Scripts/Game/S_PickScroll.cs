using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class S_PickScroll : MonoBehaviour
{
	
	public float ChanceToSpawn;
	
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<GameObject> buildings;
    [SerializeField] private GameObject buildingObj;
    [SerializeField] private float scrollSpeed;
	
	private GameManager gm;
    private SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (Random.value <= ChanceToSpawn)
        {
            if (sprites.Count > 0)
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
            }
            if (buildings.Count > 0)
            {
                buildingObj = Instantiate(buildings[Random.Range(0, buildings.Count)], transform);
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<S_PlayerController>().canMove)
            {
                transform.Translate(Vector2.left * scrollSpeed * gm.gameSpeed * Time.deltaTime);
            }
        }
    }
}
