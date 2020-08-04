using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputRate : MonoBehaviour
{
    private int rate;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Rate"))
        {
            rate = 1000;
            PlayerPrefs.SetInt("Rate", rate);
            PlayerPrefs.Save();
        }else
        {
            rate = PlayerPrefs.GetInt("Rate");
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + rate;
    }
}
