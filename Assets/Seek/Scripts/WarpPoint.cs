using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    // このスクリプトは制約とワープ先座標のみを管理します．変数の解釈はPlayer.csが行います．
    public int restrictCode = 0; // 1:x座標のみ移動, 2:y座標のみ移動, 3:ワープ無効
    public bool isChickenRestrict = false; // ニワトリでないとワープできないか否か
    public bool useSound = false; // サウンドを使うか否か
    public Vector2 warpCoord; // ワープ先座標
    private Transform player;
    private Player haniwa;
    [SerializeField] bool compassInverse = false;
    private bool compassRock = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Haniwa"))
        {
            player = collider.transform;
            haniwa = player.GetComponent<Player>();
            if (isChickenRestrict && haniwa.character != 2) return; // ニワトリ制約
            else if (restrictCode != 3) Warp();
        }
    }

    async void Warp()
    {
        Debug.Log("Warp");
        if (useSound) GameManager.PlaySE(7);
        Vector2 temp = warpCoord;
        if (restrictCode == 1) temp.y = player.position.y;
        else if (restrictCode == 2) temp.x = player.position.x;
        player.position = temp;

        if (compassInverse)
        {
            if (!compassRock)
            {
                compassRock = true;
                GameManager.InverseCompass();
                await Task.Delay(500);
                compassRock = false;
            }
        }
    }
}
