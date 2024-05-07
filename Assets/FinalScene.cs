using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScene : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI Ui;
    [SerializeField] private GameObject panelScore;
    [SerializeField] private GameObject panelImage;
    private int MaxScore = 715;

    void Start()
    {
        if(SumScore.Score >= MaxScore * 0.85)
        {
            panelScore.SetActive(true);
            panelImage.SetActive(false);
        }
        else
        {
            panelScore.SetActive(false);
            panelImage.SetActive(true);
        }
       /*
        ���� ��� ������ ������ 85% ����� �� 100%. �� �����  ������� ������� ����� error
       � �� �������� ����� � �����������.���� ������ 85 % �� ����� � ��� ������ ������ � ������ ������ ����
       */
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Score:{SumScore.Score}";
        Ui.text = $"Score: {SumScore.Score}";
    }
}
