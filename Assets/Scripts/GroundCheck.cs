using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterController controller;
    public bool isGrounded;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Update()
    {
        isGrounded = Physics.Raycast(controller.transform.position, Vector3.down, groundDistance, groundMask);
    }
}
