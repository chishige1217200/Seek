using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectre : MonoBehaviour
{
    private float speed = 0.5f; //お化けの移動速度
    private bool isFalling = false; // 落下中ならtrue
    private Rigidbody2D rb2D;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        rb2D.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 落下中かどうかを判定する
        if (rb2D.velocity.y == 0f && isFalling) isFalling = false;
        else if (rb2D.velocity.y < 0f && !isFalling) isFalling = true;
        // テクスチャの向きを変える
        if (speed > 0 && !sr.flipX) sr.flipX = true;
        else if (speed < 0 && sr.flipX) sr.flipX = false;
    }

    void FixedUpdate()
    {
        if (sr.isVisible)
        {
            rb2D.simulated = true;
            //お化けの移動
            if (!isFalling && (rb2D.velocity.x < 0.01f && rb2D.velocity.x > -0.01f))
                Return();
            Move();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //踏まれたら敵スクリプトを消す
        if (other.CompareTag("Haniwa") || other.CompareTag("Die") || other.CompareTag("Bark"))
        {
            GameManager.PlaySE(1);
            Destroy(this.gameObject);
        }
        //壁ではね返る
        if (other.CompareTag("Wall")) Return();
    }

    void Move() // 移動
    {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
    }

    void Return() // 反転
    {
        speed = speed * -1;
    }
}