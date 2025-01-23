using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerSinglePlayer : MonoBehaviour
{
    public GameObject panel; // پنل نمایش نتیجه
    public Text score_result; // نمایش مجموع امتیاز در رابط کاربری
    public int[] roundScore = new int[2]; // آرایه امتیازات (2 مرحله)
    public BallSinglePlayer ball; // مرجع توپ بازی

    private int SumScore1; // مجموع امتیاز
    public int round; // شماره دور (0 تا 1 برای دو مرحله)


    public GameObject Win_Panel;
    public GameObject Lost_Panel;

    void Start()
    {
        Time.timeScale = 1;
        // مقداردهی اولیه به آرایه `roundScore`
        for (int i = 0; i < roundScore.Length; i++)
        {
            roundScore[i] = 0;
        }

        // اطمینان از مخفی بودن پنل در ابتدا
        if (panel != null)
        {
            panel.SetActive(false);
        }
        
    }

    public void gaming(int score)
    {
        // بررسی مرجع توپ بازی
        if (ball == null)
        {
            Debug.LogError("Ball reference is missing in the Inspector.");
            return;
        }

        round = ball.GetRound(); // گرفتن شماره دور

        if (round < 2) // بررسی اینکه آیا دور کمتر از 2 است
        {
          
            roundScore[round] = score;
            ball.IncreaseRound();
            Debug.Log($"مرحله {round + 1}، امتیاز: {score}");
        }

        if (round == 1) // در دور دوم نتیجه نمایش داده شود
        {
            SumScore1 = CalculateSum(roundScore); // محاسبه مجموع امتیازات

            Check_Win_Lost();



            Debug.Log("مجموع امتیاز: " + SumScore1);
          
        }
        
        SumFunction();
    }


    public int score = 0;
    public void Check_Win_Lost()
    {


        foreach (Pins pin in GameObject.FindObjectsOfType<Pins>())
        {
            if (pin.pin_has_fallen())
            {
                score++;
            }
        }
        if (score == 10)
        {
            GetComponent<BallSinglePlayer>().ballPowerSlider.gameObject.SetActive ( false);
            Win_Panel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GetComponent<BallSinglePlayer>().ballPowerSlider.gameObject.SetActive(false);
            Lost_Panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // تابع برای محاسبه مجموع امتیازات
    public int CalculateSum(int[] scores)
    {
        int sum = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            sum += scores[i];
        }
        return sum;
    }

    // نمایش مجموع امتیاز در پنل
    public void SumFunction()
    {
        if (panel != null && score_result != null)
        {
            panel.SetActive(true); // فعال کردن پنل
            score_result.text = SumScore1.ToString(); // نمایش مجموع امتیاز
        }
       
    }


    public void Win_Panel_()
    {
        if(SceneManager.GetActiveScene().name != "Level 2")
        {
            SceneManager.LoadScene("Level 2");
        }
        else
        {
            SceneManager.LoadScene("main_menu");
        }
    }
     public void Lost_Panel_()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
