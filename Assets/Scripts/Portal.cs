using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal Other;
    public Transform ExitPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Enter");
            other.transform.position = Other.ExitPoint.position;
            other.transform.Rotate(new Vector3(0f, 180f, 0f));
        }
    }
}
