using UnityEngine;
using System.Collections;

public class KyraSniperScript : MonoBehaviour
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
    
    public int maxAmmo = 10;
    public int currentAmmo;
	public float reloadTime = 3f;
	private bool isReloading = false;

    //camera
    public Camera fpsCam;
    //fx for projectile
    public ParticleSystem gunFX;
    //animator
	public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
		currentAmmo = maxAmmo;
        damage = 0f;
    }

	void OnEnable()
	{
		isReloading = false;
		animator.SetBool("Reloading", false);
	}

	// Update is called once per frame
	void Update()
    {

		if (isReloading)
			return;

		if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(Reload());
			return;
		}

		//Hold left mouse button
		if (Input.GetMouseButtonDown(0))
        {
            //charging
            Debug.Log("Charging");
            //current duration of charge
            ChargeDuration_Current = 0f;
            //charging is true
            isCharging = true;
			//find audiomanager script
			FindObjectOfType<AudioManager>().Play("Railgun Charge");

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

    IEnumerator Reload()
	{

		isReloading = true;

		Debug.Log("Reloading...");

        //reload animation plays
		animator.SetBool("Reloading", true);

        //wait for reload time to finish
		yield return new WaitForSeconds(reloadTime - .25f);

		//reload animation stops
		animator.SetBool("Reloading", false);

		yield return new WaitForSeconds(.25f);

		//reloaded
		currentAmmo = maxAmmo;

		isReloading = false;
	}
   
    void ShootRelease()
	{
		currentAmmo--;
        //play sound
		FindObjectOfType<AudioManager>().Play("Railgun Fire");
		//stop previous sound
		FindObjectOfType<AudioManager>().Stop("Railgun Charge");
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
