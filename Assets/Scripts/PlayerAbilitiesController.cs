using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesController : MonoBehaviour
{
    bool LAInput;

    [SerializeField]
    private GameObject fireballPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        getInput();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (LAInput)
        {
            LightAbility();
        }
    }

    private void getInput()
    {
        LAInput = Input.GetButtonDown("Light Ability");
    }

    private void LightAbility()
    {
        GameObject fireball = Instantiate(fireballPrefab, transform.position + transform.forward, transform.rotation);
    }
}
