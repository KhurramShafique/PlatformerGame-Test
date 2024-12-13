using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    PlyerControls controls;
    public Animator animator;

    public GameObject bullet;
    public Transform bulletHole;

    public float force = 200;

    private void Awake()
    {
        controls = new PlyerControls();
        controls.Enable();

        controls.Land.Shoot.performed += ctx => Fire();
    }

    void Fire()
    {
        animator.SetTrigger("shoot");
        GameObject go =  Instantiate(bullet, bulletHole.position, bullet.transform.rotation);
        if (GetComponent<PlayerMovement>().isFacingRight)
        {
            go.GetComponent<Rigidbody>().AddForce(Vector2.right * force);
        }
        else
        {
            go.GetComponent<Rigidbody>().AddForce(Vector2.left * force);
        }
    }
}
