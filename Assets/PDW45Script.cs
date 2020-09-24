
using UnityEngine;

public class PDW45Script : MonoBehaviour
{
    //weapon damage
    public float damage;
    //weapon range
    public float range = 25f;

    //camera
    public Camera fpsCam;
    //fx for projectile
    public ParticleSystem gunFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit Hit;
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
