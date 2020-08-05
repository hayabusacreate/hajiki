using Photon.Pun;
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
    public GameObject[] gameObjects; 
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
                //GameObject king1= Instantiate(King1, new Vector3(1, 2, 1), Quaternion.identity);
                //GameObject king1= PhotonNetwork.Instantiate("King1", new Vector3(1, 2, 1), Quaternion.identity);
                //GameObject king2 = Instantiate(King2, new Vector3(-1, 2, -1), Quaternion.identity);
                //GameObject king2= PhotonNetwork.Instantiate("King2", new Vector3(1, 2, 1), Quaternion.identity);
                for (int i = 0; i < count; i++)
                {
                    int x = Random.RandomRange(-10, 10);
                    int y = Random.RandomRange(-5, 5);
                    int rnd = Random.RandomRange(0, units.Length);
                    if(rnd==0)
                    {
                        GameObject gameObject = PhotonNetwork.Instantiate("High", new Vector3(x, 0, y), Quaternion.identity);
                    }else
                    {
                        GameObject gameObject = PhotonNetwork.Instantiate("Normal", new Vector3(x, 0, y), Quaternion.identity);
                    }

                    //gameObjects[i + 2] = gameObject;

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
