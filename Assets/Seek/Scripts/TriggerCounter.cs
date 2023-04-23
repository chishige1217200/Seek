using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCounter : MonoBehaviour
{
    public float counter = 0f;
    private Player haniwa;
    private Rigidbody2D rb_haniwa;
    [SerializeField] GameObject activate;
    [SerializeField] GameObject[] message = new GameObject[2];

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
        {
            if (haniwa == null)
            {
                haniwa = collider.GetComponent<Player>();
                rb_haniwa = haniwa.rigidbody;
            }
            if (rb_haniwa.velocity.x > 0)
            {
                if (counter < 0f) counter = -2f;
                counter += 0.5f;
            }
            else if (rb_haniwa.velocity.x < 0) counter -= 0.5f;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
        {
            if (rb_haniwa.velocity.x > 0) counter += 0.5f;
            else if (rb_haniwa.velocity.x < 0) counter -= 0.5f;

            if (counter >= 4.0f && !activate.activeSelf)
            {
                activate.SetActive(true);
                message[0].SetActive(false);
                message[1].SetActive(true);
                Debug.Log("Activate Successfully.");
            }
        }
    }
}
