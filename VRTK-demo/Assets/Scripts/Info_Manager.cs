using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Info_Manager : MonoBehaviour
{
    public int accurateShot;
    public int missShot;
    public int shotNum;
    public int bulletNum;
    [SerializeField] Text info_text;

    // Start is called before the first frame update
    void Start()
    {
        accurateShot = 0;
        missShot = 0;
        shotNum = 0;
        bulletNum = 0;
    }
    
    public void update_when_shot(int b)
    {
        bulletNum = b;
        shotNum += 1;
        Debug.Log("Shot num: " + shotNum + " Accurate: " + accurateShot + " Missed: " + missShot);
        info_text.text = "Bullet num: " + bulletNum + " | Accurate: " + accurateShot;
    }

    public void update_when_hit()
    {
        missShot = shotNum - accurateShot;
        accurateShot += 1;
        Debug.Log("Shot num: " + shotNum + " Accurate: " + accurateShot + " Missed: " + missShot);
        info_text.text = "Bullet num: " + bulletNum + " | Accurate: " + accurateShot;
    }
}
