using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // To able to edit variable in Debug mode...
    [SerializeField]
    private float moveSpeed;
    private float currentSpeed;
    private bool isSideways, isForward, isBackward;
    private bool pickupObject = false;
    private Animator anim;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            isSideways = true;
            isForward = false;
            isBackward = false;

            anim.SetBool("isSideways", true);
            currentSpeed = Input.GetAxis("Horizontal") * moveSpeed;
            rb.AddForce(currentSpeed * transform.right, ForceMode.VelocityChange);
        }
        else if (Input.GetButton("Vertical"))
        {
            isSideways = false;
            currentSpeed = Input.GetAxis("Vertical") * moveSpeed;
            if (currentSpeed < 0)
            {
                isForward = true; isBackward = false;
                anim.SetBool("isSideways", false);
            }
            else if (currentSpeed > 0)
            {
                isForward = false; isBackward = true;
            }

            rb.AddForce(currentSpeed * transform.forward, ForceMode.VelocityChange);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {               // Item Pickup
                        /* Code for picking up object */
            if (!pickupObject) pickupObject = true;
            else if (pickupObject) pickupObject = false;


            // Animation
            if (pickupObject)
            {
                anim.SetBool("isPickup", true);
                anim.SetBool("isHoldingItem", false);
            }
            else if (!pickupObject)
            {
                anim.SetBool("isHoldingItem", true);
                anim.SetBool("isPickup", false);
            }

        }
        else
        {
            currentSpeed *= 0.00001f;
            rb.velocity *= 0.8f;
        }

        // Walking and idle animation
        if (currentSpeed != 0)
        {
            anim.SetFloat("walkingSpeed", moveSpeed);

            if (isSideways)
            {
                if (currentSpeed > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (currentSpeed < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
        else
        {
            anim.SetFloat("walkingSpeed", 0f);
        }
    }

    // Best used in physic-based movement
    // or anything with player's physics
    private void FixedUpdate()
    {

    }
}
