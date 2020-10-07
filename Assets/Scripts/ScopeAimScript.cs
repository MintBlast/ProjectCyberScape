using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeAimScript : MonoBehaviour
{
    //animator
	public Animator animator;

	//scope overlay image
	//public GameObject scopeOverlay;

	//weapon cam
	public GameObject weaponCamera;

    //isScoped
	private bool isScoped = false;

	public Camera mainCamera;

	public float scopedFOV = 5f;
	float normalFOV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
		{
			isScoped = !isScoped;
			animator.SetBool("Scoped", isScoped);

		}
    }

    
}
