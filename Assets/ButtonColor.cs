using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Diagnostics;
using TMPro;

public class ButtonColor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI Ui;
    [SerializeField] private float OpenTime;
    [SerializeField] private Timer Timer;
    int countScore = 0;
    // Start is called before the first frame update
    public List<Button> Buttons;
    public Button MainColor;
    private System.Random random = new System.Random();
    private Stopwatch timer = new Stopwatch();
    private bool NeedColorButtons = false;
    private bool NeedColorMain = false;
    private bool NeedUnColorButtons = false;
    private bool NeedUnColorMain = false;
    private Color mainColor;

    private Color[] colors = new Color[]
    {
        Color.black,
        Color.red,
        Color.blue,
        Color.gray,
        Color.green,
        Color.yellow,
    };

    private void SetRandomColors()
    {
        colors = colors.OrderBy(x => random.Next()).ToArray();
        for(var i = 0; i < Buttons.Count; i++)
        {
            SetColor(Buttons[i], colors[i]);
        }
        NeedColorButtons = false;
        
    }

    private void SetColor(Button button, Color color)
    {
        var colorBlock = new ColorBlock()
        {
            normalColor = color,
            colorMultiplier = 1,
            highlightedColor = color,
            selectedColor = color
        };
        button.GetComponent<Button>().colors = colorBlock;
    }

    private void UsualButton()
    {
        for (var i = 0; i < Buttons.Count; i++)
        {
            SetColor(Buttons[i], Color.white);
        }
        NeedUnColorButtons = false;
    }

    private void UsualButtonMain()
    {
        var colorBlock = new ColorBlock()
        {
            normalColor = Color.white,
            colorMultiplier = 1,
            highlightedColor = Color.white,
            selectedColor = Color.white
        };
        MainColor.GetComponent<Button>().colors = colorBlock;
        NeedUnColorMain = false;
    }
    private void SetRandomColorToMain()
    {
        var color = colors[random.Next(colors.Length)];
        var colorBlock = new ColorBlock()
        {
            normalColor = color,
            colorMultiplier = 1,
            highlightedColor = color,
            selectedColor = color
        };
        MainColor.GetComponent<Button>().colors = colorBlock;
        mainColor = color;
        NeedColorMain = false;
        Timer.TimeFreeze = false;
    }

    public void UserClick(int index)
    {
       // UnityEngine.Debug.Log(index);
       // UnityEngine.Debug.Log(colors[index]);
      //  UnityEngine.Debug.Log(MainColor.colors.normalColor);
       // UnityEngine.Debug.Log(Buttons[index].colors.normalColor);
         UnityEngine.Debug.Log(MainColor.colors.normalColor.g);
        UnityEngine.Debug.Log(MainColor.colors.normalColor.b);
        UnityEngine.Debug.Log(MainColor.colors.normalColor.r);
        UnityEngine.Debug.Log(mainColor.g);
        UnityEngine.Debug.Log(mainColor.b);
        UnityEngine.Debug.Log(mainColor.r);

        if (colors[index] == MainColor.colors.normalColor)
        {
            RestartGame(true);
        }
        else
        {
            RestartGame(false);
        }
        SetRandomColors();
        UsualButtonMain();
        timer.Start();
    }

    
    private void RestartGame(bool isWin)
    {
        var addCountScore = isWin ? 20 : -10;
        Timer.TimeFreeze = true;
        countScore += addCountScore;
        SumScore.ScoreLevel3 += addCountScore;
        text.text = $"Score: {countScore}";
        Ui.text = $"Score: {countScore}";
        //ClearButtons();
       
    }
    
    void Start()
    {
        timer.Start();
        SetRandomColors();
    }

    // Update is called once per frame
    void Update()
    {
        var elapsedSeconds = timer.Elapsed.Seconds;
        if(elapsedSeconds > 5)
        {
            timer.Reset();
            SetRandomColorToMain();
            UsualButton();
        }

    }

/*
    private void RestartGame(bool isWin)
    {
        var addCountScore = isWin ? 20 : -10;
        Timer.TimeFreeze = true;
        countScore += addCountScore;
        text.text = $"Score: {countScore}";
        Ui.text = $"Score: {countScore}";
        ClearButtons();
        StartCoroutine(GameRoutin());
    }

    [Serializable]
    public struct ButtonNumber
    {
        public Button button;
        public TextMeshProUGUI text;
        public Image color;
    }
*/
}
