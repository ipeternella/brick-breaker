using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    private float timeGear = 10;   
    private bool isInGearPotion;

    public int stackBlue = 0;
    //công tắc b1-b5 cho bình xanh
    public bool b1, b2, b3, b4, b5;

    // thời gian buff cho bình xanh
    public float t1 = 10f , t2 = 10f, t3 = 10f, t4 = 10f, t5 = 10f;
    //public float[] ts = new float[] { t1, t2, t3, t4, t5 };

    public Rigidbody2D ball;
    private void Awake()
    {
       
        ball = GameObject.Find("/Ball").GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        UpdateAndCheckGearPotion();
        UpdateAndCheckBlueBottle();

    }
    private void RemoveGearPotionEffect()
    {
        isInGearPotion = false;
        transform.localScale = new Vector3(1.45f, 1, 1);
        timeGear = 10;
    }
    private void UpdateAndCheckGearPotion() // đến ngược time Gear
    {
        if (isInGearPotion == true)
        {
            timeGear -= Time.deltaTime;

        }
        if (timeGear <= 0)
        {
            RemoveGearPotionEffect();
        }
    }
    private void RemoveBlueBottleEffect()
    {
        
        // chỉ reset những t < 10
        if (t1 < 10f) t1 = .01f;
        if (t2 < 10f) t2 = .01f;
        if (t3 < 10f) t3 = .01f;
        if (t4 < 10f) t4 = .01f;
        if (t5 < 10f) t5 = .01f;
    }
    private void UpdateAndCheckBlueBottle() // đến ngược time BlueBottle
    {
        if (stackBlue == 1 & t1 == 10f) //nếu ăn được buff đầu 0 ->1 thì bật công tắc. t1 = 10 để tránh buff giảm xếp chồng tử 2->1 lặp lại hàm
        {
            b1 = true; // bật công tắc b1         
        }
        if (b1 == true)
        {
            t1 -= Time.deltaTime; // thời gian buff giảm dần
        }    
        if (t1 < 0) //thời gian buff hết
        {
            stackBlue--; //số lượng buff đang xếp chồng -1
            ball.velocity = new Vector2(ball.velocity.x * (1 + 0.1f), ball.velocity.y * (1 + 0.1f)); //tăng  velocity ball lại
            t1 = 10f; //reset thời gian và tắt công tắt để tái sử dụng 
            b1 = false;       
        }
        //stack 2
        if (stackBlue == 2 & t2 == 10f)
        {
            b2 = true;   
        }
        if (b2 == true)
        {
            t2 -= Time.deltaTime; 
        }
        if (t2 < 0) 
        {
            stackBlue--; 
            ball.velocity = new Vector2(ball.velocity.x * (1 + 0.1f), ball.velocity.y * (1 + 0.1f));
            t2 = 10f; 
            b2 = false;
        }
        //stack 3
        if (stackBlue == 3 & t3 == 10f)
        {
            b3 = true;
        }
        if (b3 == true)
        {
            t3 -= Time.deltaTime;
        }
        if (t3 < 0)
        {
            stackBlue--;
            ball.velocity = new Vector2(ball.velocity.x * (1 + 0.1f), ball.velocity.y * (1 + 0.1f));
            t3 = 10f;
            b3 = false;
        }
        //stack 4
        if (stackBlue == 4 & t4 == 10f)
        {
            b4 = true;
        }
        if (b4 == true)
        {
            t4 -= Time.deltaTime;
        }
        if (t4 < 0)
        {
            stackBlue--;
            ball.velocity = new Vector2(ball.velocity.x * (1 + 0.1f), ball.velocity.y * (1 + 0.1f));
            t4 = 10f;
            b4 = false;
        }
        //stack 5
        if (stackBlue == 5 & t5 == 10f)
        {
            b5 = true;
        }
        if (b5 == true)
        {
            t5 -= Time.deltaTime;
        }
        if (t5 < 0)
        {
            stackBlue--;
            ball.velocity = new Vector2(ball.velocity.x * (1 + 0.1f), ball.velocity.y * (1 + 0.1f));
            t5 = 10f;
            b5 = false;
        }
    }
    private void ResetStatusOfPaddle()
    {
        RemoveGearPotionEffect();
        RemoveBlueBottleEffect();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var gameSession = GameSession.Instance;

        if (collision.gameObject.name.StartsWith("Health"))
        {
            if (gameSession.PlayerLives < 5)
            {
                gameSession.PlayerLives++;
            }
        }
        if (collision.gameObject.name.StartsWith("Gear"))
        {
            //Debug.Log("GEAR");
            if (isInGearPotion == false)
            {
                isInGearPotion = true;
                transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z);
            }
            else// nếu đang buff ăn buff tiếp thì reset time trở lại 10
            {
                timeGear = 10;
            }
        }
        if (collision.gameObject.name.StartsWith("Blue_Potion"))
        {
            //Debug.Log("Blue_Potion");
            if (stackBlue < 5) // va chạm thì - velocity ball tối đa không quá 5 lần 
            {                
                ball.velocity = new Vector2(ball.velocity.x * (1 - 0.1f), ball.velocity.y * (1 - 0.1f));
                stackBlue++;
            }
        }
        if (collision.gameObject.name.StartsWith("Empty_Bottle"))
        {
            //Debug.Log("Empty");
            ResetStatusOfPaddle();
        }
    }

}
