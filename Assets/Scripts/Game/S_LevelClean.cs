using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LevelClean : MonoBehaviour
{

    [SerializeField] private GameObject delPoint;

    // Start is called before the first frame update
    void Start()
    {
        delPoint = GameObject.Find("DelPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < delPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
