using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBlock : MonoBehaviour
{
    [SerializeField] bool isDestroy = false; // インスペクターからのみ変数を設定可能
    public Block[] blockSet = new Block[1]; // リセット対象のBlockのインスタンスを保持（今回は片側のみでも可）

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Haniwa")
        {
            for (int i = 0; i < blockSet.Length; i++)
                blockSet[i].Reset();
            if (isDestroy == true) Destroy(this.gameObject);
        }
    }

    // tag == "Haniwa"がOnTriggerEnter2Dすると，対応するブロックをアクティブに戻します．
    // isDestroy == trueのとき，Destroy(this.gameObject)するオプションを追加してください（一度しか実行する必要のない場所ではオブジェクトを消したいため）
}
