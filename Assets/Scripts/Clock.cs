using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] float turnSpeed = 20f;
    [SerializeField] float timeBonus = 30f;
    [SerializeField] ParticleSystem pickUpParticles;
    // Start is called before the first frame update

    [SerializeField] GameManager gameManager;

    bool pickedUp;
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
    }

    public float GetTimeBonus()
    {
        return timeBonus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !pickedUp)
        {
            gameManager.AddTime(timeBonus);
            pickUpParticles.Play();
        }
        GetComponent<SpriteRenderer>().enabled = false;
        pickedUp = true;
        Destroy(gameObject, 2f);
    }

}
