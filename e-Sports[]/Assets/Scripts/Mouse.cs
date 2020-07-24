using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private GameObject moveobj;
    private Camera mainCamera;
    private bool movecheck;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!movecheck) //マウスがクリックされたら
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入
            RaycastHit hit = new RaycastHit();
            int layerMask = 1<<10;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
                if (hit.collider.gameObject.tag == "Unit")
                {
                    moveobj = hit.collider.gameObject;
                    movecheck = true;
                }
            }
        }
        if(Input.GetMouseButtonUp(0)&&movecheck)
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {

                if(moveobj!=null)
                {
                    pos = hit.point - moveobj.transform.position;
                    moveobj.GetComponent<Unit>().movevec = pos/20;
                    moveobj = null;
                    movecheck = false;
                }

            }

        }
    }
    void OnDrawGizmos()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            pos = hit.point;
            if (pos != Vector3.zero)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(pos, 0.5f);
            }
        }

    }
}
