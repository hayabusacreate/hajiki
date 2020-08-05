using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallType
{
    Player1,
    Player2
}
public class WallManager : MonoBehaviour, IPunObservable
{
    public int body;
    public GameObject bady;
    public GameObject parent,child;
    public float far;
    private float childfar,parantfar;
    private int childwallcount,parentwallcount;
    private GameObject childobj,parentobj;
    public WallType wallType;
    private Dictionary<int,Wall> childwall, parentwall;
    private Dictionary<int,GameObject> childobjs, parentobjs;
    private Vector3 childvec, parentvec;
    // Start is called before the first frame update
    void Start()
    {
        childobj = gameObject;
        parentobj = gameObject;
        parentwall = new Dictionary<int, Wall>();
        childwall = new Dictionary<int, Wall>();
        childobjs = new Dictionary<int, GameObject>();
        parentobjs = new Dictionary<int, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.GetComponent<Unit>().playerType==PlayerType.None||child.GetComponent<Unit>().playerType == PlayerType.None)
        {
            for(int i=0;i<childobjs.Count;i++)
            {
                Destroy(childobjs[i+1]);
            }
            for (int i = 0; i < parentobjs.Count; i++)
            {
                Destroy(parentobjs[i+1]);
            }
            Destroy(gameObject);
        }
        childfar = Mathf.Abs(Vector3.Distance(transform.position, child.transform.position));
        childvec = transform.position - child.transform.position;
        parentvec = transform.position - parent.transform.position;
        if (Mathf.Floor(childfar / far) > childwallcount)
        {
            GameObject body = PhotonNetwork.Instantiate("Cube", gameObject.transform.position, Quaternion.identity);
            //body.transform.parent = parentobj.gameObject.transform;
            childobj = body;
            childwallcount++;
            body.GetComponent<Wall>().parent = gameObject;
            body.GetComponent<Wall>().count = childwallcount;
            body.GetComponent<Wall>().wallType = wallType;
            childwall[childwallcount]=body.GetComponent<Wall>();
            childobjs[childwallcount]=body;
        }
        else
        if (Mathf.Floor(childfar / far) < childwallcount)
        {
            childwall.Remove(childwallcount);
            PhotonNetwork.Destroy(childobjs[childwallcount]);


            childwallcount--;

        }
        parantfar = Mathf.Abs(Vector3.Distance(transform.position, parent.transform.position));
        if (Mathf.Floor(parantfar / far) > parentwallcount)
        {
            GameObject body = PhotonNetwork.Instantiate("Cube", gameObject.transform.position, Quaternion.identity);
            //body.transform.parent = parentobj.gameObject.transform;
            parentobj = body;
            parentwallcount++;
            body.GetComponent<Wall>().parent = gameObject;
            body.GetComponent<Wall>().count = parentwallcount;
            body.GetComponent<Wall>().wallType = wallType;
            parentwall[parentwallcount]=body.GetComponent<Wall>();
            parentobjs[parentwallcount]=body;
        }
        else
        if (Mathf.Floor(parantfar / far) < parentwallcount)
        {
            parentwall.Remove(parentwallcount);
            PhotonNetwork.Destroy(parentobjs[parentwallcount]);


            parentwallcount--;
        }
        for (int i = 1; i < parentwall.Count; i++)
        {
            parentwall[i].count = i;
            parentwall[i].child = parentvec/parentwallcount;
            parentwall[i].hight = (parent.GetComponent<Unit>().hight + child.GetComponent<Unit>().hight) / 2;
        }
        for (int i = 1; i < childwall.Count; i++)
        {
            childwall[i].count = i;
            childwall[i].child = childvec/childwallcount;
            childwall[i].hight = (parent.GetComponent<Unit>().hight + child.GetComponent<Unit>().hight) / 2;
        }
        transform.position = (parent.transform.position+child.transform.position)/2;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameObject);
            stream.SendNext(body);
            stream.SendNext(parent);
            stream.SendNext(child);
            stream.SendNext(wallType);
        }
        else
        {
            //this.gameObject = (GameObject)stream.ReceiveNext();
            body = (int)stream.ReceiveNext();
            parent = (GameObject)stream.ReceiveNext();
            child = (GameObject)stream.ReceiveNext();
            wallType = (WallType)stream.ReceiveNext();
        }
    }
}
