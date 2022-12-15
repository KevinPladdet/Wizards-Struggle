using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{

    public float Speed;
    public Transform Player;


    private Vector3 LeftAxis = new Vector3(0, 0, -1);
    private Vector3 RightAxis = new Vector3(0, 0, 1);

    void FixedUpdate()
    {
        if (Input.GetKey("left"))
        {
            transform.RotateAround(Player.position, LeftAxis, Speed);
        }

        if (Input.GetKey("right"))
        {
            transform.RotateAround(Player.position, RightAxis, Speed);
        }

        //gameplay controls for arcade machine
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.RotateAround(Player.position, LeftAxis, Speed);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(Player.position, RightAxis, Speed);
        }
        //game play controls for arcade machine ^^^^^
    }

}
