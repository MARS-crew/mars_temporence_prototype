using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameDirector : MonoBehaviour
{
 
    GameObject player;
    GameObject flag;
    GameObject timerText;
    GameObject distance;
    GameObject life;

    float setTime = 180.0f; //초단위 타이머 시간 

    void Start()
    {
        this.player = GameObject.Find("Cowboy");
        this.flag = GameObject.Find("Flag");
        this.timerText = GameObject.Find("TimeText");
        this.distance = GameObject.Find("DistanceUIText");
        this.life = GameObject.Find("LifeUIText");

    }

    void Update()
    {
       // this.timeText.GetComponent<Text>().text = TimerManager.text_time;
        float length = this.flag.transform.position.x - this.player.transform.position.x; //깃발까지의 거리 
        //float length = Vector3.Distance(playerposition, flagposition);  
        this.distance.GetComponent<Text>().text = "깃발까지 " + length.ToString("F2") + "m"; //소수점 아래 2자리까지 출력




        //타이머
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
        }
        else if (setTime <= 0)
        {
            Time.timeScale = 0.0f;  //모든 오브젝트 정지
        }

        int seconds = (int)(setTime % 60);          
        int minutes = (int)(setTime / 60) % 60;    

        string timerString = string.Format("{1:00}:{0:00}", seconds, minutes);  
        this.timerText.GetComponent<Text>().text = timerString;
    }



    public void DecreaseLife(int life)
    {
        this.life.GetComponent<Text>().text = "Life : "+ life;
    }

}
