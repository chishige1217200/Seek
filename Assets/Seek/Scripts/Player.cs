using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform diePoint; // Shield()で使用するTransform情報
    public new Rigidbody2D rigidbody; // 物理演算情報
    private new SpriteRenderer renderer; // レンダラー情報
    [SerializeField] Sprite[] texture = new Sprite[4];
    private GameObject barkL;
    private GameObject barkR;
    private float x_direction = 0f; // 右：正, 左：負
    private float x_limitSpeed = 5f; // 横移動速度制限
    private int jumpCount = 1; // ジャンプ残回数
    public int character = 0; // 0:Haniwa, 1:Dog, 2:Chicken, 3:Horse
    public bool cannotDie;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody2Dの取得
        diePoint = transform.Find("DiePoint"); // Transformの取得
        renderer = GetComponent<SpriteRenderer>(); // レンダラーの取得
        barkL = transform.Find("BarkL").gameObject;
        barkR = transform.Find("BarkR").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        if (character == 1 && Input.GetKey(KeyCode.LeftControl)) Bark();
        else DisBark();
        if (character == 3 && Input.GetKey(KeyCode.LeftControl) && (rigidbody.velocity.x > 5 || rigidbody.velocity.x < -5)) Shield();
        else DisShield();

        if (x_direction > 0 && !renderer.flipX) renderer.flipX = true;
        else if (x_direction < 0 && renderer.flipX) renderer.flipX = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow)) x_direction = 1.0f;
        else if (Input.GetKey(KeyCode.LeftArrow)) x_direction = -1.0f;
        else x_direction = 0f;

        if (character == 3 && Input.GetKey(KeyCode.LeftControl))
        {
            x_limitSpeed = 10f;
            x_direction *= 2; // 馬は移動速度2倍
        }
        else x_limitSpeed = 5f;
        VerticalMove();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Die")) Die();
        if (collision.gameObject.CompareTag("Item"))
        {
            GameManager.PlaySE(3);
            character = collision.gameObject.GetComponent<Item>().itemNum;
            renderer.sprite = texture[character];
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Field"))
        {
            if (character != 2) jumpCount = 1;
            else jumpCount = 2;
        }
    }

    void VerticalMove()
    {
        if ((x_direction > 0 && rigidbody.velocity.x < x_limitSpeed) || (x_direction < 0 && rigidbody.velocity.x > -x_limitSpeed))
            rigidbody.AddForce(new Vector2(x_direction, 0), ForceMode2D.Force);
    }

    void Jump()
    {
        if (jumpCount > 0)
        {
            // ジャンプ音を鳴らす
            jumpCount--;
            if (character == 2 && jumpCount == 0) GameManager.PlaySE(6);
            if ((rigidbody.velocity.x < 0 && x_direction <= 0) || (rigidbody.velocity.x > 0 && x_direction >= 0))
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            else
                rigidbody.velocity = new Vector2(0, 0);
            rigidbody.AddForce(new Vector2(0, 0.8f), ForceMode2D.Impulse);
        }
    }

    void Bark()
    {
        if (!GameManager.se[3].isPlaying) GameManager.PlaySE(4);
        if (renderer.flipX && !barkR.activeSelf)
        {
            barkR.SetActive(true);
            barkL.SetActive(false);
        }
        else if (!renderer.flipX && !barkL.activeSelf)
        {
            barkL.SetActive(true);
            barkR.SetActive(false);
        }
    }

    void DisBark()
    {
        barkL.SetActive(false);
        barkR.SetActive(false);
    }

    void Die()
    {
        if (!cannotDie)
        {
            GameManager.PlaySE(2);
            GameManager.bgm[0].Stop();
            GameManager.GameOver();
            Destroy(this.gameObject);
        }
    }

    void Shield()
    {
        if (!diePoint.gameObject.activeSelf)
        {
            diePoint.gameObject.SetActive(true);
            if (!GameManager.se[4].isPlaying) GameManager.PlaySE(5);
        }
    }

    void DisShield()
    {
        if (diePoint.gameObject.activeSelf)
        {
            diePoint.gameObject.SetActive(false);
        }
    }
}
