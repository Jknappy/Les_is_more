using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    public bool go_next_level = false;
    public bool last_level = false;
    public float level_count = 1;
    public float x_axis_offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            go_next_level = true;
            if(last_level == false)
            {
                transform.position = new Vector2(x_axis_offset, 4);
                x_axis_offset += 3.7f;
            }
            level_count += 1f;
            Debug.Log("next level?");
        }
    }
}
