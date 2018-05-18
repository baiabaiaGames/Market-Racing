using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private const float turnSpeed = 0.05f;

    //Animation
    private Animator anim;

    //Movement
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private float speed = 7.0f;

    private void Start () {
        controller = GetComponent<CharacterController> ();
        anim = GetComponent<Animator> ();
    }

    private void Update () {

        if (!GameManager.instance.isPlaying)
            return;

        if (InputManager.Instance.SwipeLeft)
            Rotate (false);
        if (InputManager.Instance.SwipeRight)
            Rotate (true);

        //Calculate our move delta
        Vector3 moveVector = Vector3.zero;

        moveVector = new Vector3 (Input.acceleration.x, 0, 0);
        moveVector = transform.TransformDirection (moveVector);
        moveVector *= speed;

        if (Input.acceleration.x > 0 && transform.position.x >= 3.0f) {
            moveVector.x = 0f;
        } else if (Input.acceleration.x < 0 && transform.position.x <= -3.0f) {
            moveVector.x = 0f;
        }

        bool isGrounded = IsGrounded ();
        anim.SetBool ("Grounded", isGrounded);

        // Calculate Y
        if (isGrounded) {
            verticalVelocity = -0.1f;

            if (InputManager.Instance.SwipeUp) {
                //Jump
                anim.SetTrigger ("Jump");
                verticalVelocity = jumpForce;
            }
        } else {
            verticalVelocity -= (gravity * Time.deltaTime);

            //Fast falling mechanic
            if (InputManager.Instance.SwipeDown) {
                verticalVelocity = -jumpForce;
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        //Move the pengu
        moveVector = transform.TransformDirection (moveVector);
        controller.Move (moveVector * Time.deltaTime);

        //Rotate the pengu to where he is going
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero) {
            dir.y = 0;
            transform.forward = Vector3.Lerp (transform.forward, dir, turnSpeed);
        }
    }

    private bool IsGrounded () {
        Ray groundRay = new Ray (new Vector3 (controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);
        Debug.DrawRay (groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

        return (Physics.Raycast (groundRay, 0.2f + 0.1f));
    }

    private void Rotate (bool goingRight) {
        if (goingRight) {
            if (transform.eulerAngles.y >= 90.0f && transform.eulerAngles.y <= 180.0f) {
                transform.eulerAngles = transform.eulerAngles;
            } else if (transform.eulerAngles.y >= 180.0f && transform.eulerAngles.y <= 315.0f) {
                transform.eulerAngles = new Vector3 (0, 0, 0);
            } else {
                transform.eulerAngles = new Vector3 (0, 90.0f, 0);
            }
        } else {
            if (transform.eulerAngles.y <= 270.0f && transform.eulerAngles.y >= 180.0f) {
                transform.eulerAngles = transform.eulerAngles;
            } else if (transform.eulerAngles.y <= 180.0f && transform.eulerAngles.y >= 45.0f) {
                transform.eulerAngles = new Vector3 (0, 0, 0);
            } else {
                transform.eulerAngles = new Vector3 (0, 270.0f, 0);
            }
        }
    }
}