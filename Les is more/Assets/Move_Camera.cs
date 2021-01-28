using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    public Next_Level nl;
    public float x_axis_offset = 4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nl.go_next_level)
        {
            Camera.main.transform.position = new Vector3(x_axis_offset, 0, -10);
            x_axis_offset += 4;
            nl.go_next_level = false;
        }

    }
}
