using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Tooltip("Parameter that determine movement acceleration.")]
    [SerializeField] float moveCoefficient = 100f;

    [Tooltip("Parameter that determines jump strength.")]
    [SerializeField] float jumpCoefficient = 200;

    [Tooltip("Parameter that determines drag coefficient during movement.")]
    [SerializeField] float horizontalDrag = 0.2f;
    private float horizontalMove;

    private Rigidbody rb;
    private bool isGrounded;
    private Transform camTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        camTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * moveCoefficient;
        if (Input.GetButton("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpCoefficient * -Mathf.Sign(Physics.gravity.y));
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.right * horizontalMove;
        rb.AddForce(moveDirection);
        rb.velocity -= horizontalDrag * rb.velocity.x * transform.right;
    }

    public void JumpToParallelWorld()
    {
        transform.position = new(transform.position.x, -transform.position.y, 0);
        Physics.gravity = new(0, -Physics.gravity.y, 0);
        camTransform.localPosition = new(camTransform.localPosition.x, -camTransform.localPosition.y, camTransform.localPosition.z);
        camTransform.localEulerAngles = new(-camTransform.localEulerAngles.x, camTransform.localEulerAngles.y, 0);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Terrain"))
        {
            isGrounded = true;
        }
    }
}
