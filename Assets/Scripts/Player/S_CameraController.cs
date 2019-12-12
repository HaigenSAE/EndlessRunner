using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraController : MonoBehaviour
{

    public S_PlayerController player;
    public float camSpeed = 0.1f;

    private Vector3 prevPos, nextPos;
    private float dist;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<S_PlayerController>();
        prevPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dist = player.transform.position.x - prevPos.x;

        nextPos = new Vector3 (transform.position.x + dist, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, nextPos, camSpeed);

        prevPos = player.transform.position;
    }
}
