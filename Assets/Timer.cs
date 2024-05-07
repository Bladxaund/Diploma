using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject panel;
    public float GameTime;
    public bool TimeFreeze = true;
    public TextMeshProUGUI text;
    public int Scores;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeFreeze)
        {
            
            if(GameTime > 0)
            {
                GameTime -= Time.deltaTime;
            }
            else
            {
                SumScore.ScoreLevel1 += Scores;
                Scores = 0;
                panel.SetActive(true);
            }
        }
       
        text.text = $"{GameTime:0}";
      

    }
  
}
