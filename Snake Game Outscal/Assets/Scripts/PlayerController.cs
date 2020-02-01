using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerDirection direction;

    [HideInInspector]
    public float stepLength = 0.2f;

    [HideInInspector]
    public float movementFrequency = 0.1f;

    private float counter;
    private bool move;

    [SerializeField]
    private GameObject tail;

    [SerializeField]
    private GameObject fruit;
    private GameObject[] fruits;

    private List<Vector3> deltaPosition;

    private List<Rigidbody> nodes;

    private Rigidbody mainBody;
    private Rigidbody headBody;
    private Transform tr;
    private int randomizer;

    private bool createNodeAtTail;
    private string[] color;
    private string[] points;
    private int point = 20;

    // Start is called before the first frame update
    void Awake()
    {
        tr = transform;
        mainBody = GetComponent<Rigidbody>();

        InitSnakeNode();
        InitPlayer();

        deltaPosition = new List<Vector3>() {
            new Vector3(-stepLength, 0f),
            new Vector3(0f, stepLength),
            new Vector3(stepLength, 0f),
            new Vector3(0f, -stepLength),
            };
    }

    void Start()
    {
        color = new string[ReadCSV.point.Count];
        points = new string[ReadCSV.point.Count];
        fruits = new GameObject[ReadCSV.point.Count];

        for (int i = 0; i < ReadCSV.point.Count; i++)
        {
            fruits[i] = fruit;
            points[i] = ReadCSV.point[i];
        }
    }


    // Update is called once per frame

    void Update()
    {
        CheckMovementFrequenzy();
    }

    void FixedUpdate()
    {
        if (move)
        {
            move = false;
            Move();
        }
    }

    void InitSnakeNode()
    {
        nodes = new List<Rigidbody>();

        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        headBody = nodes[0];
    }

    void SetDirectionRandom()
    {
        int dirRandom = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dirRandom;
    }

    void InitPlayer()
    {
        SetDirectionRandom();
        switch (direction)
        {
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;

            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;

            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position - new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;

            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0f, Metrics.NODE, 0f);
                nodes[2].position = nodes[0].position + new Vector3(0f, Metrics.NODE * 2f, 0f);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.FRUIT)
        {
            Destroy(other.gameObject);
            GameManager.score += point /*int.Parse(ReadCSV.point[randomizer])*/;
            createNodeAtTail = true;
        }
        if (other.tag == Tags.TAIL | other.tag == Tags.WALL)
        {
            GameManager.gameOver = true;
        }
    }

    void Move()
    {
        Vector3 dPosition = deltaPosition[(int)direction];

        Vector3 parentPosition = headBody.position;

        Vector3 prevPosition;

        mainBody.position = mainBody.position + dPosition;
        headBody.position = headBody.position + dPosition;

        for (int i = 1; i < nodes.Count; i++)
        {
            prevPosition = nodes[i].position;
            nodes[i].position = parentPosition;
            parentPosition = prevPosition;
        }

        if (createNodeAtTail)
        {
            var x = Color.red;
            int j;
            createNodeAtTail = false;

            GameObject newNode = Instantiate(tail, nodes[nodes.Count - 1].position, Quaternion.identity);
            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
            randomizer = Random.Range(0, ReadCSV.point.Count);
            print(randomizer);
            Instantiate(fruits[randomizer], new Vector3(Random.Range(-19.46f, 18.64f), Random.Range(15.82f, -16f), -0.95f), Quaternion.identity);
            j = int.Parse(ReadCSV.colour[randomizer]);
            point = int.Parse(ReadCSV.point[randomizer]);
            switch (j)
            {
                case 1:
                    x = Color.red;
                    break;
                case 2:
                    x = Color.magenta;
                    break;
                case 3:
                    x = Color.black;
                    break;
                case 4:
                    x = Color.yellow;
                    break;
                case 5:
                    x = Color.green;
                    break;
                case 6:
                    x = Color.cyan;
                    break;
                default:
                    x = Color.blue;
                    break;
            }
            fruits[randomizer].GetComponent<MeshRenderer>().sharedMaterial.color = x;
        }
    }

    void CheckMovementFrequenzy()
    {
        counter += Time.deltaTime;

        if (counter >= movementFrequency)
        {
            counter = 0f;
            move = true; ;
        }
    }

    public void SetInputDirection(PlayerDirection dir)
    {
        if (dir == PlayerDirection.UP & direction == PlayerDirection.DOWN | dir == PlayerDirection.DOWN & direction == PlayerDirection.UP |
            dir == PlayerDirection.RIGHT & direction == PlayerDirection.LEFT | direction == PlayerDirection.RIGHT & dir == PlayerDirection.LEFT)
        {
            return;
        }
        direction = dir;
        ForceMove();
    }

    void ForceMove()
    {
        counter = 0;
        move = false;
        Move();
    }
}

