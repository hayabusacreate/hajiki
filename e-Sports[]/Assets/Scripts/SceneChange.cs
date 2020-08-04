using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    Title,
    Online,
}
public class SceneChange : MonoBehaviour
{
    public GameObject King1,King2;
    public GameObject[] units;
    public int count;
    public Scene scene;
    public bool kingcreate;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (scene == Scene.Online)
        {
            if(kingcreate)
            {
                Instantiate(King1, new Vector3(1, 2, 1), Quaternion.identity);
                Instantiate(King2, new Vector3(-1, 2, -1), Quaternion.identity);
                for (int i = 0; i < count; i++)
                {
                    int x = Random.RandomRange(-10, 10);
                    int y = Random.RandomRange(-5, 5);
                    int rnd = Random.RandomRange(0, units.Length);
                    Instantiate(units[rnd], new Vector3(x, 0, y), Quaternion.identity);

                }
                kingcreate = false;
            }

        }
    }
    public void TitleChange()
    {
        SceneManager.LoadScene("OnLine");
    }
}
