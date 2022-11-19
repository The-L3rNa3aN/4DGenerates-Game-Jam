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

    [Header("Timer")]
    public float timer = 0f;
    public float timeLimit = 15f;
    public bool isLate = false;
    public bool runTimer = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InputManager.lmbInput += LeftClick;
    }

    private void Update()
    {
        TimerRun();
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

    private void TimerRun()
    {
        if(runTimer)
        {
            timer += Time.deltaTime;
        }
    }

    public void TimerReset()
    {
        float diff = timeLimit - timer;
        if(diff > 0f)
        {
            gameManager.AddScore(10);
        }
        else
        {
            if (diff <= -5f)
                gameManager.AddScore(9);
            else if (diff <= -10f)
                gameManager.AddScore(8);
            else if (diff <= -15f)
                gameManager.AddScore(7);
            else if (diff <= -20f)
                gameManager.AddScore(6);
            else if (diff <= -25f)
                gameManager.AddScore(5);
            else if (diff <= -30f)
                gameManager.AddScore(4);
            else if (diff <= -35f)
                gameManager.AddScore(3);
            else if (diff <= -40f)
                gameManager.AddScore(2);
            else if (diff <= -45f)
                gameManager.AddScore(1);
            else
                gameManager.AddScore(0);
        }

        timer = 0f;
        runTimer = true;
    }
}
