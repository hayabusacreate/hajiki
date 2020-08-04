using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputName : MonoBehaviour
{
    public InputField inputField;
    private string name;
    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        //inputField = inputField.GetComponent<InputField>();
        if (PlayerPrefs.HasKey("Name"))
        {
            start = true;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InputText()
    {
        name = inputField.text;

            PlayerPrefs.SetString("Name", name);
            PlayerPrefs.Save();
        
            gameObject.SetActive(false);
        
    }
}
