
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    //weapon(s) in the weaponholder
	public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
		SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
		int previousSelectedWeapon = selectedWeapon;

        //when the scroll wheel goes up
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (selectedWeapon >= transform.childCount - 1)
				selectedWeapon = 0;
            else
                //select weapon
			    selectedWeapon++;
		}
		//when the scroll wheel goes down
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (selectedWeapon <= 0)
				selectedWeapon = transform.childCount - 1;
			else
				//select weapon
				selectedWeapon--;
		}

        //press "1" key
        if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			selectedWeapon = 0;
		}

		//press "2" key
		if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
		{
			selectedWeapon = 1;
		}

		if (previousSelectedWeapon != selectedWeapon)
		{
			SelectWeapon();
		}
	}

    void SelectWeapon()
	{
        //weapon currently in the weaponholder 
		int i = 0;
        foreach (Transform weapon in transform)
		{
			//selected weapons
			if (i == selectedWeapon)
				weapon.gameObject.SetActive(true);
			else
				weapon.gameObject.SetActive(false);

			i++;
		}
	}
}
