using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    void Update()
    {
        transform.position = playerTransform.position;
    }
}
