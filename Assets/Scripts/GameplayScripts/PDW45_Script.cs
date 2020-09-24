using System.Collections;
using UnityEngine;

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

    void Start()
	{
		currentAmmo = maxAmmo;
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
            if (target != null)
			{
				target.TakeDamage(damage);
			}
		}
	}
}
