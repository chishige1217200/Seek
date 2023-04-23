using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject group; // 対応する相手のBlockのGameObject情報．双方が双方を参照するように設定．

    void Start()
    {
        if (group == null) group = gameObject;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bark")
        {
            GameManager.PlaySE(9);
            this.gameObject.SetActive(false);
            group.SetActive(false);
        }
    }

    public void Reset()
    {
        this.gameObject.SetActive(true);
        if (!group.activeSelf) group.SetActive(true);
    }

    // tag == "Bark"がOnTriggerEnter2D()したときにオブジェクトを非アクティブにする（Destroy(this.gameObject)するとリセットできなくなるので）
    // 参考：https://www.sejuku.net/blog/53536
    // 2つの（表，裏の対応した位置の）ブロックをセットにして，双方が同時にアクティブ・非アクティブになるように

}
