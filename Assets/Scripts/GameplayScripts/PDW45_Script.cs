using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PDW45_Script : MonoBehaviour
{
	//weapon damage
	public float damage = 25f;
	//weapon range
	public float range = 25f;

	//camera
	public Camera fpsCam;
	//fx for projectile
	public ParticleSystem gunFX;

	//current ammo
	public float currentAmmo;
	//max ammo
	public float maxAmmo = 25f;
	//animator
	public Animator animator;
    //is reloading
	private bool isReloading = false;
	//reload time
	public float reloadTime = 1f;

	//ammo display
	public Text ammoDisplay;
	//max ammo display
	public Text MaxAmmoDisplay;
	//weapon name display
	public Text weaponName;

    void Start()
	{
		currentAmmo = maxAmmo;
	}

    void OnEnable()
	{
        //reloading is set to false when starting
		isReloading = false;
		animator.SetBool("Reloading", false);
	}

    // Update is called once per frame
    void Update()
    {
		

        //when reloading
		if (isReloading)
			return;

		//when current ammo is 0 and or R key is presse
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

        //click left mouse button
        if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
    }

    IEnumerator Reload()
	{
		isReloading = true;

		Debug.Log("Reloading...");

		animator.SetBool("Reloading", true);

		yield return new WaitForSeconds(reloadTime - .25f);

		animator.SetBool("Reloading", false);

		yield return new WaitForSeconds(.25f);

		currentAmmo = maxAmmo;

		isReloading = false;
	}

    void Shoot()
	{
		currentAmmo--;
		FindObjectOfType<AudioManager>().Play("PDW-45 Fire SFX");
		RaycastHit Hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, range))
		{
			Debug.Log(Hit.transform.name);

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
	}
}
