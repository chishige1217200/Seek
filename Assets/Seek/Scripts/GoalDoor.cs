using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDoor : MonoBehaviour
{
    private int canGoal;
    private Player haniwa;

    // Update is called once per frame
    void Update()
    {
        if (canGoal >= 1 && Input.GetKeyDown(KeyCode.Space))
        {
            canGoal = -1000;
            haniwa.cannotDie = true;
            //GameManager.bgm[0].Stop();
            GameManager.PlaySE(8);
            GameManager.bgm[0].Stop();
            GameManager.GameClear();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
        {
            canGoal++;
            if (haniwa == null) haniwa = collider.GetComponent<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa")) canGoal--;
    }
}
