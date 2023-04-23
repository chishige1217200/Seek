using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    // このスクリプトは使用しません

    GameObject[] prefabs = new GameObject[4];
    [SerializeField] int itemNum = 0; // 0:Haniwa, 1:Dog, 2:Chicken, 3:Horse
    bool isExecuted = false; // アイテムが複数個出てしまうのを防ぐ

    // Start is called before the first frame update
    void Start()
    {
        // Resourcesフォルダからprefabファイルを取得
        //prefabs[0] = (GameObject)Resources.Load("");
        prefabs[1] = (GameObject)Resources.Load("Prefabs/ItemDog");
        prefabs[2] = (GameObject)Resources.Load("Prefabs/ItemChicken");
        prefabs[3] = (GameObject)Resources.Load("Prefabs/ItemHorse");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isExecuted && collider.CompareTag("Haniwa"))
        {
            isExecuted = true;
            Spawn();
        }
    }

    void Spawn()
    {
        GameManager.PlaySE(3);
        Instantiate(prefabs[itemNum], new Vector3(this.transform.position.x, this.transform.position.y + 2f, 0f), new Quaternion(0, 0, 0, 0));
        Destroy(this.gameObject);
    }
}
