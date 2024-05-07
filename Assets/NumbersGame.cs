using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class NumbersGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI Ui;
    [SerializeField] private List<ButtonNumber> buttons;
    [SerializeField] private float OpenTime;
    [SerializeField] private Timer Timer;
    public int PressedNumbers = 1;
    int countScore = 0;
    private List<int> correctSiquence = new();
    private List<int> playerSiquence = new();
    private int currentInputIndex;
    private bool isWin;

    private void Start()
    {

        
        StartCoroutine(GameRoutin());
    }

    private void GenerateNumbers()
    {
        currentInputIndex = 0;
        var currentButtons = new List<ButtonNumber>(buttons);
        correctSiquence.Clear();
        playerSiquence.Clear();

        for (var i = 1; i <= buttons.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, currentButtons.Count);
            var currentButton = currentButtons[randomIndex];
            currentButton.text.text = i.ToString();
            currentButtons.Remove(currentButton);
            var currentIndex = buttons.IndexOf(currentButton);
            correctSiquence.Add(currentIndex);
           

            currentButton.button.onClick.AddListener(() => { CheckButton(currentIndex); });
        }
        foreach (var id in correctSiquence)
        {
            Debug.Log(id);
        }
    }


    private IEnumerator GameRoutin()
    {
        isWin = true;
        GenerateNumbers();
        ShowButtons(true);
        yield return new WaitForSeconds(OpenTime);
        ShowButtons(false);
        Timer.TimeFreeze = false;
    }

    private void ShowButtons(bool isActive)
    {
        foreach (var button in buttons)
        {
            button.button.interactable = !isActive;
            button.text.gameObject.SetActive(isActive);
        }
    }

  
    void CheckButton(int index)
    {
        if (buttons[index].text.gameObject.activeSelf)
        {
            return;
        }

        Debug.Log($"Натиснута кнопка: {index}");
        playerSiquence.Add(index);
        Debug.Log($"curentIndex = {currentInputIndex}");
        if (index == correctSiquence[currentInputIndex])
        {
            buttons[index].color.color = Color.green;
        }
        else
        {
            buttons[index].color.color = Color.red;
            isWin = false;
        }
        buttons[index].text.gameObject.SetActive(true);
        if (playerSiquence.Count == correctSiquence.Count)
        {
            RestartGame(isWin);
            return;
        }
        currentInputIndex++;
    }

    // Очистити стан кнопок
    private void ClearButtons()
    {
        foreach (var button in buttons)
        {
            button.button.onClick.RemoveAllListeners();
            button.color.color = Color.white;
            button.text.gameObject.SetActive(false); // Сховати текст при очищенні
        }
    }

    // Перезапуск гри
    private void RestartGame(bool isWin)
    {
        var addCountScore = isWin ? 20 : -10;
        Timer.TimeFreeze = true;
        countScore += addCountScore;
        Timer.Scores = countScore;
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
}
