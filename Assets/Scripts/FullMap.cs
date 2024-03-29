using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMap : MonoBehaviour
{
    public Transform    Player;
    public float        ZoomSpeed = 3f;
 
    private Transform   _transform;
    private Vector3     _offSet;
 
    private void Start ()
    {
        _transform = transform;
        _offSet = Player.position - _transform.position;
    }
   
    private void Update ()
    {
        _transform.position = Player.position - _offSet;
 
        // if(Input.GetKey(KeyCode.DownArrow))
        // {
        //     var dir = Player.position - transform.position;
 
        //     _offSet += dir.normalized * ZoomSpeed * Time.deltaTime;
        // }
        // else 
        if (Input.GetKey(KeyCode.M))
        {
            var dir = Player.position - transform.position;
 
            _offSet -= dir.normalized * ZoomSpeed * Time.deltaTime;
        }
    }
}

