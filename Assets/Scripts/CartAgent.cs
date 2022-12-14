using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CartAgent : MonoBehaviour
{
    private Camera cam;
    public NavMeshAgent agent;
    private int i = 0;
    private float dist;
    public int cartNumber;
    public Outline outLine;
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
        InputManager.lmbInput += LeftClick;
        cam = Camera.main;
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (runTimer && cartNumber >= gameManager.cartNum)
            timer += Time.deltaTime;

        if (positions.Count != 0)
        {
            dist = Vector3.Distance(positions[i], transform.position);
            agent.SetDestination(positions[i]);
        }

        if(dist < 2f)
            if (i < positions.Count - 1) positions.RemoveAt(i); //i++;

        if(cartNumber == gameManager.cartNum)
            outLine.GetComponent<Outline>().eraseRenderer = false;
        else
            outLine.GetComponent<Outline>().eraseRenderer = true;
    }

    private void LeftClick()
    {
        if (cam == null) cam = Camera.main;             //For a MissingReferenceException that pops up after reloading the level. Could it be performance intensive?

        if (cartNumber == gameManager.cartNum) //&& !gameManager.isPaused)
        {
            lmbTimer = cooldown;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ///Another way to try:
                ///If an aisle is clicked, have the pos to be manually moved towards the origin. (RECOMMENDED)
                
                if(hit.transform.tag != "Aisle")        //For a bug where the cart tries reaching the unreachable position
                {
                    Vector3 pos = hit.point;
                    positions.Add(pos);
                }
            }
        }

        //COOLDOWN.
        if(lmbTimer > 0f) lmbTimer -= Time.deltaTime;

        if(lmbTimer < 0f) lmbTimer = 0f;
    }

    public void TimerReset()
    {
        float diff = timeLimit - timer;
        if (diff > 0f)
            gameManager.AddScore(10);
        else if (diff < 0f && diff > -7.5f)
            gameManager.AddScore(5);
        else
            gameManager.AddScore(0);           //Is this even required?

        timer = 0f;
        runTimer = true;
    }
}
