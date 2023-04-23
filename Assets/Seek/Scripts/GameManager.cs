using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // staticがついている変数と関数はシーン中には必ず1つしか存在しないので，「GameManager.isPaused」などのようにクラス名でそのまま参照することができます．

    public static AudioSource[] se; // 効果音情報格納
    public static AudioSource[] bgm; // 音楽情報格納
    private static GameObject gameOverPanel;
    private static GameObject gameClearPanel;
    private static RectTransform compass;
    public static GameObject messagePanel;
    public static GameObject[] message = new GameObject[2];
    [SerializeField] static Text[] timeText = new Text[2];
    [SerializeField] static Text clearTimeMessage;
    private DateTime startTime;
    private DateTime finishTime;
    private static TimeSpan sp;
    public static int rta = 0;
    void Start()
    {
        bgm = new AudioSource[1];
        gameOverPanel = transform.Find("Canvas").Find("GameOver").gameObject;
        gameClearPanel = transform.Find("Canvas").Find("GameClear").gameObject;
        messagePanel = transform.Find("Canvas").Find("Message").gameObject;
        message[0] = messagePanel.transform.Find("Text").gameObject;
        message[1] = messagePanel.transform.Find("Text2").gameObject;
        timeText[0] = transform.Find("Canvas").Find("Time").GetComponent<Text>();
        timeText[1] = gameClearPanel.transform.Find("Time").GetComponent<Text>();
        clearTimeMessage = gameClearPanel.transform.Find("Text2").GetComponent<Text>();
        compass = transform.Find("Canvas").Find("compass").GetComponent<RectTransform>();
        se = transform.Find("SE").GetComponents<AudioSource>();
        bgm[0] = transform.Find("BGM").GetComponent<AudioSource>();
        rta = PlayerPrefs.GetInt("rta");
        if (rta == 1)
        {
            startTime = DateTime.Now;
            transform.Find("Canvas").Find("Image").gameObject.SetActive(true);
            timeText[0].gameObject.SetActive(true);
        }
        Debug.Log("RTA Mode: " + rta);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) GameManager.Restart();
        if (Input.GetKeyDown(KeyCode.Escape)) GameManager.EndGame();
        if (rta == 1)
        {
            finishTime = DateTime.Now;
            sp = finishTime - startTime;
            timeText[0].text = sp.ToString(@"hh\:mm\:ss\.ff");
        }
    }

    public static void ShowMessage(int num)
    {
        Debug.Log("Show");
        messagePanel.SetActive(true);
        message[num].SetActive(true);
    }

    public static void DisShowMessage()
    {
        Debug.Log("DisShow");
        messagePanel.SetActive(false);
        message[0].SetActive(false);
        message[1].SetActive(false);
    }

    public static void InverseCompass()
    {
        Debug.Log("Inverse");
        compass.Rotate(0f, 0f, 180f);
    }

    public static void PlaySE(int num) // 1～
    {
        if (num == 0) num = 1;
        else if (num > se.Length) num = se.Length;
        se[num - 1].PlayOneShot(se[num - 1].clip);
    }

    public static void PlayBGM(int num) // 1～
    {
        if (num == 0) num = 1;
        else if (num > bgm.Length) num = bgm.Length;
        bgm[num - 1].Play();
    }

    public static void Restart() // シーンをやり直します
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void GameClear()
    {
        if (!gameClearPanel.activeSelf) gameClearPanel.SetActive(true);
        if (rta == 1)
        {
            rta = 0;
            timeText[0].gameObject.SetActive(false);
            timeText[1].text = sp.ToString(@"hh\:mm\:ss\.ff");
            clearTimeMessage.gameObject.SetActive(true);
            timeText[1].gameObject.SetActive(true);
        }
    }

    public static void GameOver()
    {
        if (!gameOverPanel.activeSelf) gameOverPanel.SetActive(true);
    }

    public static void EndGame() // ゲームを終了します
    {
        SceneManager.LoadScene("TitleScene");
    }
}
