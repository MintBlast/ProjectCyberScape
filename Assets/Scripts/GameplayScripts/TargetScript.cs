
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    float horizontalInput;
    public float health = 50f;
    public float speed = 5f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    } 

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.left * horizontalInput * Time.deltaTime * speed);

        //if the target is at 15 or lower
        if (transform.position.z < 15)
        {
            //moves to the target's right
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }


    }
}
