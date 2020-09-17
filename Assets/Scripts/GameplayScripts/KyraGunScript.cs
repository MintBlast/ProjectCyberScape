using UnityEngine;

public class KyraGunScript : MonoBehaviour
{
    //weapon damage 
    public float damage;
    //add damage when charging
    public float Add_damage = 10f; 
    //power
    public float power = 10f;
    //max power
    public float maxPower = 100f;
    //charge speed
    public float chargeSpeed = 10f;
    //weapon range
    public float range = 100f;
    //weapon charge
    public bool isCharging = false;
    //charge duration
    public float ChargeDuration_Current = 0f;
    


    //camera
    public Camera fpsCam;
    //fx for projectile
    public ParticleSystem gunFX;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
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
            //current duration of charge
            ChargeDuration_Current = 0f;
            //charging is true
            isCharging = true;
            
        }else if (Input.GetMouseButtonUp(0) && isCharging )
        {
            //shoots
            ShootRelease();
            isCharging = false;
            damage = 0f;
        }

        if (isCharging)
        {
            
            damage += Add_damage * power * Time.deltaTime * chargeSpeed;
        }
    }


   
    void ShootRelease()
    {
        
        gunFX.Play();
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
