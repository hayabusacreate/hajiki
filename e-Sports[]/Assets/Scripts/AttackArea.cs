using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Unit unit;
    // Start is called before the first frame update
    void Start()
    {
        unit = transform.parent.gameObject.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Unit")
        {
            if(other.gameObject.GetComponent<Unit>().playerType!=PlayerType.None)
            {
                if(other.gameObject.GetComponent<Unit>().playerType !=unit.playerType)
                {
                    unit.units.Add(other.gameObject.GetComponent<Unit>());
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            if (other.gameObject.GetComponent<Unit>().playerType != PlayerType.None)
            {
                if (other.gameObject.GetComponent<Unit>().playerType != unit.playerType)
                {
                    unit.units.Remove(other.gameObject.GetComponent<Unit>());
                }
            }
        }
    }
}
