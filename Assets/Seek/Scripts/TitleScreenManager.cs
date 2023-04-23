using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    private GameObject creditPanel;
    private GameObject optionPanel;
    private new AudioSource audio;
    [SerializeField] Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        creditPanel = transform.Find("Credit").gameObject;
        optionPanel = transform.Find("Option").gameObject;
        audio = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("rta"))
        {
            PlayerPrefs.SetInt("rta", 0);
            toggle.isOn = false;
        }
        else
        {
            int temp = PlayerPrefs.GetInt("rta");
            if (temp == 1) toggle.isOn = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) Play();
        if (Input.GetKeyDown(KeyCode.O)) ShowOption();
        if (Input.GetKeyDown(KeyCode.C)) ShowCredit();
        if (Input.GetKeyDown(KeyCode.Escape)) EndGame();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (optionPanel.activeSelf)
            {
                toggle.isOn = !toggle.isOn;
                SetRTA();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            DisShowCredit();
            DisShowOption();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ShowCredit()
    {
        if (!creditPanel.activeSelf)
        {
            audio.PlayOneShot(audio.clip);
            creditPanel.SetActive(true);
        }
    }

    public void DisShowCredit()
    {
        if (creditPanel.activeSelf) creditPanel.SetActive(false);
    }

    public void ShowOption()
    {
        if (!optionPanel.activeSelf)
        {
            audio.PlayOneShot(audio.clip);
            optionPanel.SetActive(true);
        }
    }

    public void DisShowOption()
    {
        if (optionPanel.activeSelf) optionPanel.SetActive(false);
    }

    public void SetRTA()
    {
        if (toggle.isOn) PlayerPrefs.SetInt("rta", 1);
        else if (!toggle.isOn) PlayerPrefs.SetInt("rta", 0);
        PlayerPrefs.Save();
        Debug.Log("SetRTA");
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
