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
                panel.SetActive(true);
            }
        }
       
        text.text = $"{GameTime:0}";
      

    }
  
}
