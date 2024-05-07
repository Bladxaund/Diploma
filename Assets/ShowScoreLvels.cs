using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScoreLvels : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private TextMeshProUGUI text3;
    [SerializeField] private TextMeshProUGUI text4;
    

    // Start is called before the first frame update
    void Start()
    {
        text1.text = SumScore.ScoreLevel1.ToString();
        text2.text = SumScore.ScoreLevel2.ToString();
        text3.text = SumScore.ScoreLevel3.ToString();
        text4.text = SumScore.ScoreLevel4.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
