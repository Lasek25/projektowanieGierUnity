using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveWithMouse : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        // zakładamy, że komponent CharacterController jest już podpięty pod obiekt
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        // zmieniamy sposób poruszania się postaci
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // to już nam potrzebne nie będzie
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        if (Input.GetButton("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // zgodnie ze wzorem y = (1/2 * g) * t-kwadrat 
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "launcher")
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue * 3);
        }
    }
}
