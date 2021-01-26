using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public PlayerMovement pm;
    //public CircleCollider2D detection_radius;
    

    public float max_distance;
    public float min_distance;
    public float move_speed;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, move_speed * Time.deltaTime);
    }

}
