using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.forward * moveSpeed;
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
