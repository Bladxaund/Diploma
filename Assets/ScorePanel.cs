using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    private void Awake()
    {
        panel.SetActive(false);
    }
    public void AcitivatePanel(int score)
    {
        Debug.Log(panel);
        text.text = $"Score: {score}";
        panel.SetActive(true);
    }
}

