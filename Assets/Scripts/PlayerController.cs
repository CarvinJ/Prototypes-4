using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;

    public float speed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;

    public GameObject rocketPrefab;
    GameObject temRocket;
   
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3 (0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Power Up"))
        {
        hasPowerup = true;
        powerupIndicator.gameObject.SetActive(true);
        Destroy(other.gameObject);
        StartCoroutine(PowerupCountdownRoutine());
        }
       
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
        

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody ememyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayfromPlayer = (collision.gameObject.transform.position - transform.position);
            ememyRigidBody.AddForce(awayfromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log(" Player Collided with " + collision.gameObject.name + "with Power Up set to " + hasPowerup);
        }
    }
}
