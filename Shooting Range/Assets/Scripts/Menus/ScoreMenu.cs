using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour 
{
    static public int[] HighScoreArray = new int[7];
    public Text txt1;
    public Text txt2;
    public Text txt3;
    public Text txt4;
    public Text txt5;
    public Text txt6;
    public Text txt7;

    // Start is called before the first frame update
    void Start() 
    {
        txt1.text = HighScoreArray[0].ToString();
        txt2.text = HighScoreArray[1].ToString();
        txt3.text = HighScoreArray[2].ToString();
        txt4.text = HighScoreArray[3].ToString();
        txt5.text = HighScoreArray[4].ToString();
        txt6.text = HighScoreArray[5].ToString();
        txt7.text = HighScoreArray[6].ToString();
    }
}
