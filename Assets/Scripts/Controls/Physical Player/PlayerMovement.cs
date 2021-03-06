﻿using UnityEngine;
using System;
using System.Collections;

public class PlayerMovement : MonoBehaviour, IMoveAble {

    [SerializeField]
    private float maxMovementSpeed = 8;

    [SerializeField]
    private float movementIncreaseMultiplier = 1.02f;

    private float moveSpeed = 1;

    private Vector3 myVelocity;

    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        GetComponent<SetPlayerDown>().startPlaying += StartPlayerMovement;
    }

    void OnDisable()
    {
        GetComponent<SetPlayerDown>().startPlaying -= StartPlayerMovement;
    }

    private void StartPlayerMovement()
    {
        StartCoroutine(UpdatePlayerMovement());
    }

    IEnumerator UpdatePlayerMovement() {
        while (true) {
            if (moveSpeed < maxMovementSpeed)
                moveSpeed *= movementIncreaseMultiplier;

            //add our own constant force, without removing the gravity of our rigidbodys
            rb.velocity = transform.forward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

            yield return new WaitForFixedUpdate();
        }
    }

    public void AddMovementY(float _strength) {
        rb.velocity = new Vector3(rb.velocity.x, _strength, rb.velocity.z);
    }
}
