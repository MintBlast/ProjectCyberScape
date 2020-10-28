using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KyraSniperScript : MonoBehaviour
{
    //weapon damage 
    public float damage;
    //add damage when charging
    public float Add_damage = 10f; 
    //power
    public float power = 10f;
    //max damage
    public float maxDamage = 100f;
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

    //cool down time
    private float cooldownTime = 3f;
    //is cooled
    private bool isCooled = true;
    

    //camera
    public Camera fpsCam;
    //fx for projectile
    public ParticleSystem gunFX;
    //animator
	public Animator animator;

	//ammo display
	public Text ammoDisplay;
	//max ammo display
	public Text MaxAmmoDisplay;
	//weapon name display
	public Text weaponName;

    // Start is called before the first frame update
    void Start()
    {
		currentAmmo = maxAmmo;
        damage = 0f;
    }

	void OnEnable()
	{
        //reloading is set to false when startings
		isReloading = false;
		animator.SetBool("Reloading", false);
	}

	// Update is called once per frame
	void Update()
    {
		

        //when reload 
		if (isReloading)
			return;

        //when current ammo is 0 and or R key is pressed
		if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
		{
            //reload
			StartCoroutine(Reload());
			return;
		}

		//ammo is displayed
		ammoDisplay.text = currentAmmo.ToString();
		//max ammo is displayed
		MaxAmmoDisplay.text = maxAmmo.ToString();

		//Hold left mouse button
		if (Input.GetMouseButtonDown(0) && isCooled)
        {
            //charging
            Debug.Log("Charging");
            //current duration of charge
            ChargeDuration_Current = 0f;
            //charging is true
            isCharging = true;
			//find audiomanager script
			FindObjectOfType<AudioManager>().Play("Railgun Charge");

            if (damage == maxDamage)
            {
                ShootRelease();
			}

        }else if (Input.GetMouseButtonUp(0) && isCharging)
        {
            //shoots
            ShootRelease();
            isCharging = false;
            damage = 0f;
        }

        if (isCharging)
        {
            damage = damage + Add_damage;
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
            Debug.Log("hit: " + Hit.transform.name);

            TargetScript target = Hit.transform.GetComponent<TargetScript>();
            EnemyTargetScript enemytarget = Hit.transform.GetComponent<EnemyTargetScript>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (enemytarget != null)
            {
                enemytarget.TakeDamage(damage);
            }
        }

        StartCoroutine(CoolDown());
        return;
    }

    IEnumerator CoolDown()
	{
        isCooled = false;
        //pauses for 3 seconds in cooldown time
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Overheat! Cooling Down!");

        yield return new WaitForSeconds(cooldownTime);

        Debug.Log("Cooled");
        isCooled = true;
	}

}
