using System.Collections;
using UnityEngine;
 
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // INSTRUCTIONS
    // This script must be on an object that has a Character Controller component.
    // It will add this component if the object does not already have it.
    //    This is done by line 4: "[RequireComponent(typeof(CharacterController))]"
    //
    // Also, make a camera a child of this object and tilt it the way you want it to tilt.
    // The mouse will let you turn the object, and therefore, the camera.

    // These variables (visible in the inspector) are for you to set up to match the right feel
    public float speed = 12f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;

    // This must be linked to the object that has the "Character Controller" in the inspector. You may need to add this component to the object
    public CharacterController controller;
    private Vector3 velocity;

    // Customisable gravity
    public float gravity = -20f;

    // Tells the script how far to keep the object off the ground
    public float groundDistance = 0.4f;

    // So the script knows if you can jump!
    private bool isGrounded;

    // How high the player can jump
    public float jumpHeight = 2f;

    private void Start()
    {
        // If the variable "controller" is empty...
        if(controller == null)
        {
            // ...then this searches the components on the gameobject and gets a reference to the CharacterController class
            controller = GetComponent<CharacterController>();
        }
    }
 
    private void Update()
    {
        // These lines let the script rotate the player based on the mouse moving
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        // Get the Left/Right and Forward/Back values of the input being used (WASD, Joystick etc.)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
 
        // Let the player jump if they are on the ground and they press the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Rotate the player based off those mouse values we collected earlier
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
 
        // This is stealing the data about the player being on the ground from the character controller
        isGrounded = controller.isGrounded;
 
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        // This fakes gravity!
        velocity.y += gravity * Time.deltaTime;

        // This takes the Left/Right and Forward/Back values to build a vector
        Vector3 move = transform.right * x + transform.forward * z;
 
        // Finally, it applies that vector it just made to the character
        controller.Move(move * speed * Time.deltaTime + velocity * Time.deltaTime);
    }
}