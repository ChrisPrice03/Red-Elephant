using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class PortalLogic : MonoBehaviour
{
    public GameObject destinationPortal; // Reference to the destination portal

    void Start() {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a "Player" tag
        {
            // Vector2 playerLastPosition = other.transform.position;

            // if (this.tag != "enter shop portal") {
            //     destinationPortal.transform.position = playerLastPosition;
            // }
            // Teleport the player to the destination portal's position
            // other.transform.position = destinationPortal.transform.position;
            other.transform.position = new Vector3(-20,-8,0);
        }
    }
}