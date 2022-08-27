using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CartAgent : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject targetDest;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetDest.transform.position = hit.point;
                agent.SetDestination(hit.point);
            }
        }
    }
}
