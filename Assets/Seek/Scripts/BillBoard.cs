using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public int canShow = 0;
    [SerializeField] int messageNum = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canShow == 0 && GameManager.message[messageNum].activeSelf) GameManager.DisShowMessage();
            else if (canShow >= 1 && !GameManager.messagePanel.activeSelf) GameManager.ShowMessage(messageNum);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
        {
            canShow++;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
        {
            canShow--;
            if (canShow < 0) canShow = 0;
        }
    }
}
