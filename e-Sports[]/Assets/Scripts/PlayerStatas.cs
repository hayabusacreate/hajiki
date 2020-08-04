using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class PlayerStatas : MonoBehaviour
{
    public Text name,rate;
    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("Name"))
        {
            name.text = PlayerPrefs.GetString("Name");
        }
        if(PlayerPrefs.HasKey("Rate"))
        {
            rate.text = ""+PlayerPrefs.GetInt("Rate");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteKey("Name");
        }
    }
}
