using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private ParticleSystem part;

    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
        rb.velocity = transform.forward * moveSpeed;
        part.Play();
        Destroy(gameObject, part.main.duration);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
