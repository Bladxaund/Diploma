using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.Linq;
using TMPro;


public class DialButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI Ui;
    int countScore = 0;
    public List<GameObject> Stages; 
    public List<Button> ExampleButtons;
    public List<Button> OrigButtons;
    private int currentStage = 0;
    private int currentStageErrors = 0;
    private List<int> exampleSequence;
    private List<int> userSequence = new List<int>();
    private int sequenceLenght = 5;
    private System.Random random = new System.Random();
    private Stopwatch timer = new Stopwatch();
    private Stopwatch flashingTimer = new Stopwatch();
    private static Color normalColor = Color.red;
    private static Color baseColor = Color.blue;
    private int speed = 2;
    private GameState gameState = GameState.Demo;
    private ColorBlock highlighted = new ColorBlock { 
        normalColor = normalColor,
        colorMultiplier = 1,
        highlightedColor = normalColor,
        selectedColor = normalColor
    };
    private ColorBlock normal = new ColorBlock
    {
        normalColor = Color.white,
        colorMultiplier = 1,
        highlightedColor = Color.white,
        selectedColor = Color.white
    };
    private ColorBlock baseColorBlock = new ColorBlock
    {
        normalColor = baseColor,
        colorMultiplier = 1,
        highlightedColor = baseColor,
        selectedColor = baseColor
    };

    // Start is called before the first frame update
    void Start()
    {
        GenerateNewSequence();    
    }

    public void RepeatSequence()
    {
        timer.Start();
        flashingTimer.Start();
        gameState = GameState.Demo;
        foreach (var b in OrigButtons)
        {
            b.colors = normal;
        }
    }
    public void GenerateNewSequence()
    {
        exampleSequence = new List<int>();
        userSequence = new List<int>();
        for (var i = 0; i < sequenceLenght; i++)
        {
            exampleSequence.Add(random.Next(ExampleButtons.Count));
        }
        timer.Start();
        flashingTimer.Start();
        gameState = GameState.Demo;
        foreach (var b in OrigButtons)
        {
            b.colors = normal;
        }

    }

    /*
    private void RestartGame(bool isWin)
    {
        var addCountScore = isWin ? 20 : -10;
        countScore += addCountScore;
        text.text = $"Score: {countScore}";
        Ui.text = $"Score: {countScore}";
        //ClearButtons();

    }
    */
    
    public void UserClick(int index)
    {
        userSequence.Add(index);

        foreach (var b in OrigButtons)
        {
            b.colors = normal;
        }

        if (userSequence.Last() != exampleSequence[userSequence.Count - 1])
        {
            userSequence.Clear();
            OrigButtons[index].colors = highlighted;
            RepeatSequence();
            gameState = GameState.IncorrectInput;
        }
        else
        {
            OrigButtons[index].colors = baseColorBlock;
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Demo)
        {

            var elapsedSeconds = timer.Elapsed.Seconds / speed; // ( /2) - ������� ��������
            if(elapsedSeconds < exampleSequence.Count)
            {             
                if (elapsedSeconds > 0)
                {
                    ExampleButtons[exampleSequence[elapsedSeconds - 1]].GetComponent<Button>().colors = normal;                  
                }
                if(flashingTimer.ElapsedMilliseconds > 200)
                {
                    UnityEngine.Debug.Log(flashingTimer.ElapsedMilliseconds);
                    ExampleButtons[exampleSequence[elapsedSeconds]].GetComponent<Button>().colors = highlighted;
                }
                if (flashingTimer.Elapsed.Seconds >= speed) 
                {
                    flashingTimer.Restart();
                }
            }
            else
            {
                timer.Reset();
                flashingTimer.Reset();
                gameState = GameState.Game;
                ExampleButtons[exampleSequence[elapsedSeconds - 1]].GetComponent<Button>().colors = normal;
            }
        }
        else if(gameState == GameState.Game)
         {
          if(exampleSequence.Count == userSequence.Count)
            {
              //  sequenceLenght += 2;
                GenerateNewSequence();
                countScore += 100;
                if(currentStageErrors >= 1 && currentStageErrors <= 4)
                {
                    Stages[currentStage].GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else if(currentStageErrors > 4)
                {
                    Stages[currentStage].GetComponent<SpriteRenderer>().color = Color.red;
                }
                else if (currentStageErrors == 0)
                {
                    Stages[currentStage].GetComponent<SpriteRenderer>().color = Color.blue;
                }
                currentStageErrors = 0;
                currentStage += 1;  
                text.text = $"Score: {countScore}";
                Ui.text = $"Score: {countScore}";

            }
        }
        else if (gameState == GameState.IncorrectInput)
        {
            if(flashingTimer.ElapsedMilliseconds > 500)
            {
                flashingTimer.Restart();
                gameState = GameState.Demo;
                currentStageErrors += 1;
                countScore -= 15;
                text.text = $"Score: {countScore}";
                Ui.text = $"Score: {countScore}";

            }
        }
        
    }
}
