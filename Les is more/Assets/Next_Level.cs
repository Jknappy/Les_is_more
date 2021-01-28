using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    public bool go_next_level = false;
    public bool last_level = false;
    public float level_count = 1;
    public float first_offset;
    public float x_offset_amount;

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
                transform.position = new Vector2(first_offset, -.5f);
                first_offset += x_offset_amount;
            }
            level_count += 1f;
            Debug.Log("next level?");
        }
    }
}
