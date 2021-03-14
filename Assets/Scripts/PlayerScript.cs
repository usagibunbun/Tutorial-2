using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text win;
    public Text lives;
    public Text lose;
    private int livesValue = 3;
    private int scoreValue = 0;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Coins: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        win.text = "";
        lose.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (Input.GetKey("escape"))
        {
        Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector3 (36.0f, 1);
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();


            }
            if (scoreValue == 8)
            {
                win.text = "You Win! Game by Shayna Chevallier :}";
                {
                    musicSource.clip = musicClipOne;
                    musicSource.Stop();

                    musicSource.clip = musicClipTwo;
                    musicSource.Play();
                    musicSource.loop = false;
                }
            }
            
            
        }
        if (collision.collider.tag == "Enemy")
            {
                livesValue -= 1;
                lives.text = "Lives: " + livesValue.ToString();
                Destroy(collision.collider.gameObject);
        
            }
            if (livesValue == 0)
            {
                lose.text = "You Lose! Game by Shayna Chevallier :{";
                Destroy(gameObject);
            }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}