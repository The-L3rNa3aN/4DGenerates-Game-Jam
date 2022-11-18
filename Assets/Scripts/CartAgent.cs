using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CartAgent : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    private int i = 0;
    private float dist;
    public int cartNumber;
    private GameManager gameManager;

    [SerializeField] private List<Vector3> positions = new List<Vector3>();

    [Header("Left Mouse Click cooldown")]
    private float cooldown = 1f;
    private float lmbTimer = 0f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InputManager.lmbInput += LeftClick;
    }

    private void Update()
    {
        if(positions.Count != 0)
        {
            dist = Vector3.Distance(positions[i], transform.position);
            agent.SetDestination(positions[i]);
        }

        if(dist < 2f)
        {
            if(i < positions.Count - 1)
            {
                positions.RemoveAt(i); //i++;
            }
        }
    }

    private void LeftClick()
    {
        if (cartNumber == gameManager.cartNum) //&& !gameManager.isPaused)
        {
            lmbTimer = cooldown;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 pos = hit.point;
                positions.Add(pos);
            }
        }

        //COOLDOWN.
        if(lmbTimer > 0f) lmbTimer -= Time.deltaTime;

        if(lmbTimer < 0f) lmbTimer = 0f;
    }
}
