using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // To able to edit variable in Debug mode...
    [SerializeField]
    private float moveSpeed = 0.7f;
    private float currentSpeed;
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
    //rb.AddRelativeForce (Vector3.forward * 10 - rigidbody.velocity);
    void Update()
    {
        if ((Input.GetButton("Vertical") && Input.GetButton("Horizontal"))||
            (Input.GetButton("Horizontal") && Input.GetButton("Vertical")))
        {
            currentSpeed = Input.GetAxis("Vertical") * moveSpeed;
            if (currentSpeed < 0)
            {
                //s
                anim.SetBool("isSideways", false);
                anim.SetBool("isBackward", true);
                anim.SetBool("isForward", false);
            }
            else if (currentSpeed > 0)
            {
                //w
                anim.SetBool("isSideways", false);
                anim.SetBool("isBackward", false);
                anim.SetBool("isForward", true);
            }
            if (Mathf.Abs(rb.velocity.x) < 2.8)
                rb.AddForce(Input.GetAxis("Horizontal") * moveSpeed * transform.right, ForceMode.VelocityChange);
            else if (Mathf.Abs(rb.velocity.x) > 2.8)
                rb.velocity = new Vector3(2.8f * Input.GetAxis("Horizontal"), 0, rb.velocity.z);
            if (Mathf.Abs(rb.velocity.z) < 2.8)
                rb.AddForce(currentSpeed * transform.forward, ForceMode.VelocityChange);
            else if (Mathf.Abs(rb.velocity.z) > 2.8)
                rb.velocity = new Vector3(rb.velocity.x, 0, 2.8f*Input.GetAxis("Vertical"));
        }
        else if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("isSideways", true);
            currentSpeed = Input.GetAxis("Horizontal") * moveSpeed;
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            if (Mathf.Abs(rb.velocity.x) < 4)
                rb.AddForce(currentSpeed * transform.right, ForceMode.VelocityChange);
            else if (Mathf.Abs(rb.velocity.x) > 4)
                rb.velocity = new Vector3(4 * Input.GetAxis("Horizontal"), 0, rb.velocity.z);
        }
        else if (Input.GetButton("Vertical"))
        {
            currentSpeed = Input.GetAxis("Vertical") * moveSpeed;
            if (currentSpeed < 0)
            {
                //s
                anim.SetBool("isSideways", false);
                anim.SetBool("isBackward", true);
                anim.SetBool("isForward", false);
            }
            else if (currentSpeed > 0)
            {
                //w
                anim.SetBool("isSideways", false);
                anim.SetBool("isBackward", false);
                anim.SetBool("isForward", true);
            }

            if (Mathf.Abs(rb.velocity.z) < 4)
                rb.AddForce(currentSpeed * transform.forward, ForceMode.VelocityChange);
            else if (Mathf.Abs(rb.velocity.z) > 4)
                rb.velocity = new Vector3(rb.velocity.x, 0, 4 * Input.GetAxis("Vertical"));
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {               // Item Pickup
                        /* Code for picking up object */
            if (!pickupObject) pickupObject = true;
            else if (pickupObject) pickupObject = false;


            // Animation
            if (pickupObject)
            {
                //rb.transform.localPosition = new Vector3( rb.position.x + (float)0.5, rb.position.y, rb.position.z);
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

        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector3(0.1f, 0, rb.velocity.z);
        }

        if (Input.GetButtonUp("Vertical"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0.1f);
        }

        // Walking and idle animation
        if (currentSpeed != 0)
        {
            anim.SetFloat("walkingSpeed", moveSpeed);
            if (anim.GetBool("isSideways"))
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
