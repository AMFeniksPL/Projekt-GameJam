using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour, IWeapon
{

    public LineRenderer laser;

    public float laserDistance = 1000f;

    public float damagePerSecond = 60;

    public GameObject laserEnding;

    public AudioSource laserSound;



    // Start is called before the first frame update
    void Start()
    {
        laser.enabled = false;
        laserEnding.SetActive(false);
        laserSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
        else
        {
            laser.enabled = false;
            laserEnding.SetActive(false);
            laserSound.Stop();
        }
    }    
    
    public void Attack()
    {
        if (!laserSound.isPlaying)
        {
            laserSound.Play();
        }

        laser.enabled = true;
        laser.SetPosition(0, transform.GetChild(0).position);

        RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).position, transform.GetChild(0).right, laserDistance);
        if (hit.collider != null)
        {
            laser.SetPosition(1, hit.point);
            if (hit.collider.transform.CompareTag("Enemy"))
            {
                RogueDicedEvents.hitEvent.Invoke(new HitEventData(hit.collider.gameObject, null, damagePerSecond * Time.deltaTime, 1000 * Time.deltaTime * transform.right));
            }

        }
        else
        {
            laser.SetPosition(1, transform.right * laserDistance);
        }
        laserEnding.SetActive(true);

        laserEnding.transform.position = laser.GetPosition(1);
    }
}
