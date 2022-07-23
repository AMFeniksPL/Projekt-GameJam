using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour, IWeapon
{

    public GameObject bulletPrefab;
    public bool isAutoFire;
    public float coolDown = 1f;
    private float lastTimeFire;
    public float bulletForce = 20f;
    private bool canFire;



    // Start is called before the first frame update
    void Start()
    {
        canFire = true;
        lastTimeFire = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            if ((Input.GetButton("Fire1") && isAutoFire) || (Input.GetButtonDown("Fire1") && !isAutoFire))
            {
                Attack();
            }
        }


        if(!canFire && Time.time > lastTimeFire + coolDown)
        {
            canFire = true;
        }
    }


    public void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletForce);

        canFire = false;
        lastTimeFire = Time.time;
    }
}
