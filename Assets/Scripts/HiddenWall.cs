using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger");
        print("Tag: " + other);
        if (other.CompareTag("Player"))
        {
            print("Secret found");
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.enabled = false;
        }
    }
}
