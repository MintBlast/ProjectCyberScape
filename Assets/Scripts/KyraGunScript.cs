
using UnityEngine;

public class KyraGunScript : MonoBehaviour
{
    //weapon damage - I know the damage number... I know it's OP
    public float damage;
    //power
    public float power = 10f;
    //max power
    public float maxPower = 100f;
    //charge speed
    public float chargeSpeed = 100f;
    //weapon range
    public float range = 100f;

    //camera
    public Camera fpsCam;
    //fx for projectile
    public ParticleSystem gunFX;
    //sound fx for weapon charge
    public AudioSource chargeSFX;
    //sound fx for firing
    public AudioSource fireSFX;
    

    // Start is called before the first frame update
    void Start()
    {
        //gun charge sfx
        chargeSFX = GetComponent<AudioSource>();
        //gun firing sfx
        fireSFX = GetComponent<AudioSource>();
        damage = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Hold left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //charging
            Debug.Log("Charging");
            chargeSFX.Play();
            damage += Time.deltaTime * chargeSpeed;
        }else if (Input.GetMouseButtonUp(0))
        {
            //shoots
            ShootRelease();
            damage = 0f;
        }
    }


   
    void ShootRelease()
    {
        fireSFX.Play();
        gunFX.Play();
        //muzzleFlash.Play();
        //hits something
        RaycastHit Hit;
        //shoots at the position of the camera, the direction where it shoots - forward, raycast variable and the range
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, range))
        {
            Debug.Log(Hit.transform.name);

            TargetScript target = Hit.transform.GetComponent<TargetScript>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    
}
