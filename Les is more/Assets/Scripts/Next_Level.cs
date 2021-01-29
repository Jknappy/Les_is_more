using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    public bool go_next_level = false;
    public bool last_level = false;
    public bool beat_game = false;
    public bool collected_coin;

    public float coin_count;
    public float current_coin_count;

    public float total_level_count;
    public float level_count = 1;
    public float first_offset;
    public float x_offset_amount;

    public Collect_Coin cc;
    public GameObject coin;

    public Collider2D coin_block;

    // Start is called before the first frame update
    void Start()
    {
        coin = GameObject.FindGameObjectWithTag("Coin");
        cc = coin.gameObject.GetComponent<Collect_Coin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collected_coin)
        {
            current_coin_count++;
            if(current_coin_count == coin_count)
            {
                coin_block.enabled = false;
            }
            else
            {
                coin_block.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(total_level_count != level_count)
            {
                go_next_level = true;
                if (last_level == false)
                {
                    transform.position = new Vector2(first_offset, -.5f);
                    first_offset += x_offset_amount;
                }
                level_count += 1f;
                coin_count += 1f;
            }
            else
            {
                beat_game = true;
            }
        }


    }
}
