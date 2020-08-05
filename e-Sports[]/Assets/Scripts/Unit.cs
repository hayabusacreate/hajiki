using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public enum Unittype
{

    Normal,
    King,
    High
}
public enum PlayerType
{
    Player1,
    Player2,
    None
}
public class Unit : MonoBehaviour,IPunObservable
{
    //ステータス系
    public float hp, maxhp;
    public float wight;
    public float hight, maxhight;
    public float atk, maxatk;
    public float scale, maxscale;
    public Unittype unittype;
    public PlayerType playerType;
    private Renderer renderer;
    private bool changeflag;
    public int level;

    //攻撃系
    public List<Unit> units;
    public float atktime, time;

    //操作系
    private Camera mainCamera;
    private Vector3 mousePos, savemousePos;
    private float angle, power;
    public Vector3 movevec, savevec;
    private Rigidbody rigidbody;
    private bool beRay;
    private bool moveflag;
    private bool reflect;
    private RaycastHit Hit;
    private bool refrectx, refrectz;

    //壁系
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        if (playerType == PlayerType.None)
        {
            hp = 0;
        }
        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Statas();
        if (playerType == PlayerType.None)
        {
            renderer.material.color = Color.white;
            changeflag = false;
        }
        if (playerType == PlayerType.Player1)
        {
            renderer.material.color = Color.blue;

        }
        if (playerType == PlayerType.Player2)
        {
            renderer.material.color = Color.red;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RayCheck();
        }
        if (playerType != PlayerType.None)
        {
            Attack();
        }
        if (changeflag)
        {
            if (hp < maxhp)
            {
                hp += Time.deltaTime;
            }
            if (hp >= maxhp)
            {
                changeflag = false;
            }
        }
        Move();
    }

    void Statas()
    {
        if (hp < 0)
        {
            playerType = PlayerType.None;
            hp = 0;
        }
        //atk = level * maxatk;
        //hp = level * maxhp;
        //scale = level * maxscale;
        //hight = level * maxhight;
    }

    private void RayCheck()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == gameObject.GetComponent<Collider>())
        {
            beRay = true;
        }
        else
        {
            beRay = false;
        }

    }
    void Move()
    {
        if (transform.position.x > 20 && !refrectx)
        {
            movevec.x = -movevec.x;
            refrectx = true;
        }
        if (transform.position.x < -20 && !refrectx)
        {
            movevec.x = -movevec.x;
            refrectx = true;
        }
        if (transform.position.z > 15 && !refrectz)
        {
            movevec.z = -movevec.z;
            refrectz = true;
        }
        if (transform.position.z < -15 && !refrectz)
        {
            movevec.z = -movevec.z;
            refrectz = true;
        }
        if (refrectx || refrectz)
        {
            if (transform.position.x < 20 && transform.position.x > -20)
            {
                refrectx = false;
            }
            if (transform.position.z < 15 && transform.position.z > -15)
            {
                refrectz = false;
            }
        }
        transform.position += new Vector3(movevec.x, 0, movevec.z);
        if (!reflect)
        {
            savevec = movevec;
        }
        if (movevec.x > 0 || movevec.z > 0)
        {
            movevec += -movevec * Time.deltaTime;
        }
        if (movevec.x < 0 || movevec.z < 0)
        {
            movevec += -movevec * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {

            }
        }
    }


    private void Attack()
    {
        time += Time.deltaTime;

        if (units.Count != 0)
        {
            if (atktime < time)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].playerType == playerType)
                    {
                        units.Remove(units[i]);
                    }
                    if (units[i].playerType != PlayerType.None)
                    {
                        if (units[i].playerType == playerType)
                        {

                        }
                        else
                        {
                            Ray ray = new Ray(transform.position, units[i].gameObject.transform.position);
                            RaycastHit hit;
                            float distance = Vector3.Distance(transform.position, units[i].gameObject.transform.position);
                            int layerMask = 1 << 11;
                            bool atkflag = false;
                            if (Physics.Raycast(ray, out hit, distance, layerMask))
                            {


                                if ((playerType == PlayerType.Player1 && hit.transform.gameObject.GetComponent<Wall>().wallType == WallType.Player2) ||
                                    (playerType == PlayerType.Player2 && hit.transform.gameObject.GetComponent<Wall>().wallType == WallType.Player1))
                                {
                                    if (hit.transform.gameObject.GetComponent<Wall>().hight >= hight)
                                    {
                                        atkflag = true;
                                    }
                                }

                            }
                            if (!atkflag)
                            {
                                units[i].hp -= atk;
                            }

                        }

                    }
                }

                time = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerType == PlayerType.None)
        {
            if (collision.gameObject.tag == "Unit")
            {
                if (collision.gameObject.GetComponent<Unit>().playerType != PlayerType.None)
                {
                    playerType = collision.gameObject.GetComponent<Unit>().playerType;
                    changeflag = true;
                }


            }
        }
        if (collision.gameObject.tag == "Unit")
        {
            if (!reflect)
            {
                reflect = true;
                Vector3 a = collision.gameObject.transform.position - transform.position;
                Vector3 n = (collision.gameObject.transform.position - transform.position) / (a.magnitude);
                Vector3 savev = -(Vector3.Dot((movevec - collision.gameObject.GetComponent<Unit>().savevec), n)) * (n + movevec) / 2;

                //gameObject.GetComponent<Unit>().movevec = -(Vector3.Dot((gameObject.GetComponent<Unit>().movevec - movevec), n)) * (n + gameObject.GetComponent<Unit>().movevec) / 2;
                movevec = savev;

            }
            if (!changeflag)
            {
                if (collision.gameObject.GetComponent<Unit>().playerType == playerType)
                {
                    if (collision.gameObject.GetComponent<Unit>().unittype == unittype)
                    {
                        //Destroy(collision.gameObject);
                        level++;
                    }
                    else
                    {
                        GameObject walls = Instantiate(wall, transform.position, Quaternion.identity);
                        if (playerType == PlayerType.Player1)
                        {
                            walls.GetComponent<WallManager>().wallType = WallType.Player1;
                        }
                        if (playerType == PlayerType.Player2)
                        {
                            walls.GetComponent<WallManager>().wallType = WallType.Player2;
                        }
                        walls.GetComponent<WallManager>().parent = gameObject;
                        walls.GetComponent<WallManager>().child = collision.gameObject;
                    }
                }
            }

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            if (reflect)
            {
                reflect = false;
                collision.gameObject.GetComponent<Unit>().reflect = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            if (!reflect)
            {
                if ((playerType == PlayerType.Player1 && other.gameObject.GetComponent<Wall>().wallType == WallType.Player2) ||
                (playerType == PlayerType.Player2 && other.gameObject.GetComponent<Wall>().wallType == WallType.Player1))
                {
                    reflect = true;
                    movevec.x = -movevec.x;
                    movevec.z = -movevec.z;
                }

            }
            if ((playerType == PlayerType.Player1 && other.gameObject.GetComponent<Wall>().wallType == WallType.Player1) ||
                (playerType == PlayerType.Player2 && other.gameObject.GetComponent<Wall>().wallType == WallType.Player2))
            {

                if (other.GetComponent<Wall>().parent.GetComponent<WallManager>().parent != gameObject &&
                    other.GetComponent<Wall>().parent.GetComponent<WallManager>().child != gameObject)
                {
                    GameObject walls = Instantiate(wall, transform.position, Quaternion.identity);
                    if (playerType == PlayerType.Player1)
                    {
                        walls.GetComponent<WallManager>().wallType = WallType.Player1;
                    }
                    if (playerType == PlayerType.Player2)
                    {
                        walls.GetComponent<WallManager>().wallType = WallType.Player2;
                    }
                    walls.GetComponent<WallManager>().parent = gameObject;
                    walls.GetComponent<WallManager>().child = other.gameObject.GetComponent<Wall>().parent.GetComponent<WallManager>().child;
                    other.GetComponent<Wall>().parent.GetComponent<WallManager>().child = other.gameObject.GetComponent<Wall>().parent.GetComponent<WallManager>().parent;
                    other.GetComponent<Wall>().parent.GetComponent<WallManager>().parent = gameObject;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            if (reflect)
            {
                reflect = false;
            }
        }
    }
    private void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        info.Sender.TagObject = this.gameObject;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(hp);
            stream.SendNext(maxhp);
            stream.SendNext(atk);
            stream.SendNext(maxatk);
            stream.SendNext(hight);
            stream.SendNext(maxhight);
            stream.SendNext(scale);
            stream.SendNext(maxscale);
            stream.SendNext(wight);
            stream.SendNext(playerType);
            stream.SendNext(unittype);
            stream.SendNext(level);
        }
        else
        {
            hp = (float)stream.ReceiveNext();
            maxhp = (float)stream.ReceiveNext();
            atk = (float)stream.ReceiveNext();
            maxatk = (float)stream.ReceiveNext();
            hight = (float)stream.ReceiveNext();
            maxhight = (float)stream.ReceiveNext();
            scale = (float)stream.ReceiveNext();
            maxscale = (float)stream.ReceiveNext();
            wight = (float)stream.ReceiveNext();
            playerType = (PlayerType)stream.ReceiveNext();
            unittype = (Unittype)stream.ReceiveNext();
            level = (int)stream.ReceiveNext();
        }
    }
}
