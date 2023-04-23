using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; // Transform情報
    private new Camera camera;
    public bool cameraAuto = true; // プレイヤーに自動追従
    public float[] x_cameraRestrict = new float[2]; // xの左・右制限
    public float[] y_cameraRestrict = new float[2]; // yの下・上制限

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Haniwa").transform; // "Haniwa"がシーン中に存在しないとエラーになるので注意
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && cameraAuto)
            AutoView();
    }

    public void ManualView(Vector2 pos)
    {
        Debug.Log("ManualView");
        StartCoroutine(Throw(new Vector3(pos.x, pos.y, transform.position.z)));
    }

    IEnumerator Throw(Vector3 pos)
    {
        while (!cameraAuto)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f * Time.deltaTime);
            yield return null;
        }
    }

    void AutoView()
    {
        Vector2 temp;
        temp.x = player.position.x;
        temp.y = player.position.y;

        if (temp.x < x_cameraRestrict[0] + camera.orthographicSize * 16f / 9f) temp.x = x_cameraRestrict[0] + camera.orthographicSize * 16f / 9f;
        else if (temp.x > x_cameraRestrict[1] - camera.orthographicSize * 16f / 9f) temp.x = x_cameraRestrict[1] - camera.orthographicSize * 16f / 9f;
        if (temp.y < y_cameraRestrict[0] + camera.orthographicSize) temp.y = y_cameraRestrict[0] + camera.orthographicSize;
        else if (temp.y > y_cameraRestrict[1] - camera.orthographicSize) temp.y = y_cameraRestrict[1] - camera.orthographicSize;

        transform.position = new Vector3(temp.x, temp.y, transform.position.z);
    }
}
