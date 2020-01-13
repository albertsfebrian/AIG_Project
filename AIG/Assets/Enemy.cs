using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int health;
    public static int count =0 ;
    private bool isHeadshot = false;
    public int move;
    public Animator animator;
    private int isDeathCounted = 0;

    public int enemyRow, enemyCol;
    Map map;

    const int START = 1, END = 0, OBSTACLE = 100;

    class Node
    {
        public int row, col;
        public Node visitor;
        public bool isVisited;
        public double cost;
        public double heur;
        public double totalCost;


        public Node(int row, int col, Node visitor)
        {
            this.row = row;
            this.col = col;
            this.visitor = visitor;
        }

        public Node(int row, int col, Node visitor, double cost, double heuristic)
        {
            this.row = row;
            this.col = col;
            this.visitor = visitor;
            this.cost = cost;
            this.heur = heuristic;
            this.totalCost = this.cost + this.heur;
        }

    }

    static List<Node> queues;

    void copyMap() {
        
        map = new Map(21, 21);
        for (int row = 0; row < 21; row++)
        {
            for (int col = 0; col < 21; col++)
            {
                if (DisplayMap.map.coor[row, col].value == DesignController.WALL)
                    map.coor[row, col].value = OBSTACLE;
                else if (DisplayMap.map.coor[row, col].value == DesignController.ENEMY )
                {
                    if(this.enemyRow != row && this.enemyCol != col)
                        map.coor[row, col].value = OBSTACLE;
                    else
                        map.coor[row, col].value = START;
                }
                else if (DisplayMap.map.coor[row, col].value == DesignController.TREASURE)
                    map.coor[row, col].value = OBSTACLE;
                else if (DisplayMap.map.coor[row, col].value == DesignController.OBSTACLE_LION)
                    map.coor[row, col].value = OBSTACLE;
                else if (DisplayMap.map.coor[row, col].value == DesignController.OBSTACLE_BOX)
                    map.coor[row, col].value = OBSTACLE;
                else if (DisplayMap.map.coor[row, col].value == DesignController.PLAYER)
                    map.coor[row, col].value = END;
            }
        }
    }


    //Node[,] nodes;
    List<Node> path = new List<Node>();

    void backtrack(Node [,] visitor,int sRow, int sCol, int eRow, int eCol)
    {
        Node curr = visitor[eRow, eCol];
        while (curr != null)
        {
            if (curr.row == sRow && curr.col == sCol) break;
            
            //Debug.Log("x " + curr.x + " y : " + curr.y);
            path.Add(new Node(curr.row, curr.col, null));
            curr = visitor[curr.row, curr.col];
        }

        //string a = "";
        //foreach (Node item in path)
        //{
        //    a += "x,y(" + item.row + "," + item.col + "),";
        //}
        //Debug.Log(a);
    }


    void aStar()
    {
        path.Clear();
        DisplayMap.map.coor[enemyRow, enemyCol].value = DesignController.FLOOR;
        this.enemyRow = Mathf.RoundToInt(transform.position.x);
        this.enemyCol = Mathf.RoundToInt(transform.position.z);
        DisplayMap.map.coor[enemyRow, enemyCol].value = DesignController.ENEMY;
        /* ------------------------- Start A* -------------------- */
        int targetCol = PlayerCode.playerCol, targetRow = PlayerCode.playerRow;
        

        bool isFound = false;

        copyMap();
        queues = new List<Node>();
        

        bool[,] visited = new bool[21, 21];
        Node[,] visitor = new Node[21, 21];
        queues.Add(new Node(this.enemyRow, this.enemyCol, null, 0, 0));
        visited[this.enemyRow, this.enemyCol] = true;

        while (queues.Count > 0 && !isFound)
        {
            Node node = queues.ElementAt(0);
            queues.RemoveAt(0);
            //Debug.Log(node.row + " " + node.col);
           

            if (node.row == targetRow && node.col == targetCol)
            {
                Debug.Log("found");
                visitor[node.row,node.col] = node.visitor;
                backtrack(visitor ,this.enemyRow, this.enemyCol, targetRow, targetCol);
                isFound = true;
                break;
            }
            visitor[node.row, node.col] = node.visitor;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 && j == 0) || (i != 0 && j != 0))
                        continue;

                    if (node.row + i > 20 || node.row + i < 0) continue;
                    if (node.col + j > 20 || node.col + j < 0) continue;

                    //Debug.Log("x,y:" + node.x + " " + node.y);
                    if (map.coor[node.row + i,node.col + j].value == OBSTACLE || visited[node.row + i,node.col + j])
                        continue;

                    double heur = Mathf.Sqrt(Mathf.Pow(node.row - targetRow, 2) + Mathf.Pow(node.col - targetCol, 2));

                    queues.Add(new Node(node.row + i, node.col + j, node, node.cost + 1, heur));
                    visited[node.row + i,node.col + j] = true;

                    //Debug.Log("add(" + node.row + ", " + node.col + ")=(" + (node.row + i) + "," + (node.col + j) + ")");
                }
            }

            queues.Sort((a, b) => a.totalCost.CompareTo(b.totalCost));
            //string tmp = "";
            //foreach (Node item in queues)
            //{
            //    tmp += "(" + item.row + "," + item.col + ")";
            //}
            //Debug.Log(tmp);
        }

                
    }

    public int getHealth() { return this.health; }
    public void setHealth(int health) { this.health = health; }
    public void shooten() { this.health -= 10; }

    public void headShoot() {
        //Weapon.StartTimer = Time.time;
        //Weapon.isStartTimer = true;
        //Weapon.StartTimer = Time.time;
        this.health -= 50;
        isHeadshot = true;
    }

    //private GameObject weapon = GameObject.FindGameObjectWithTag("UMP");
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        //Debug.Log("Enemy Created");
        setHealth(100);
        animator.SetBool("isWalk", true);
        this.enemyRow = Mathf.RoundToInt(transform.position.x);
        this.enemyCol = Mathf.RoundToInt(transform.position.z);
        Debug.Log("Enemy " + enemyRow + "," + enemyCol);
        if(enemyRow!= -1 && enemyCol != -1) DisplayMap.map.coor[enemyRow, enemyCol].value = DesignController.ENEMY;
    }


	// Update is called once per frame
	void Update () {

        if (enemyRow != -1 && enemyCol != -1 && this.health > 0) aStar();
        if (path.Count > 0 && path[path.Count - 1] != null)
        {
            Node tmp = path[path.Count - 1];
            path.RemoveAt(path.Count - 1);

            int destRow = tmp.row;
            int destCol = tmp.col;

            Debug.Log("Player : " + destRow + "," + destCol);
            Debug.Log("Enemy : " + enemyRow + "," + enemyCol);

            Vector3 target = new Vector3(destRow, 0, destCol);
            Vector3 curr = new Vector3(enemyRow, 0, enemyCol);
            Vector3 targetDirection = target - curr;

            float singleStep = 4f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);


            //if (destRow > this.enemyRow)
            //{
            //    transform.rotation = Quaternion.Euler(0, 90, 0);
            //}
            //else if (destCol < this.enemyCol)
            //{
            //    transform.rotation = Quaternion.Euler(0, 180, 0);
            //}
            //else if (destRow < this.enemyRow)
            //{
            //    transform.rotation = Quaternion.Euler(0, -90, 0);
            //}
            //else if (destCol > this.enemyCol)
            //{
            //    transform.rotation = Quaternion.Euler(0, 0, 0);
            //}
            transform.Translate(Vector3.forward * Time.deltaTime * 4f);
        }
        if(path.Count <= 0)
        {
            int targetCol = PlayerCode.playerCol, targetRow = PlayerCode.playerRow;
            Vector3 target = new Vector3(targetRow, 0, targetCol);
            Vector3 curr = new Vector3(enemyRow, 0, enemyCol);
            Vector3 targetDirection = target - curr;

            float singleStep = 4f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }


        //Debug.Log("Counted: " + count);
        //if (animator.GetBool("isWalk") && count >0 )
        //{
        //    gameObject.GetComponentInChildren<Weapon>().transform.position = new Vector3(gameObject.GetComponentInChildren<Weapon>().transform.position.x, 1.55f, gameObject.GetComponentInChildren<Weapon>().transform.position.z);
        //}
        //else if (!animator.GetBool("isWalk") && count > 0)
        //{
        //    gameObject.GetComponentInChildren<Weapon>().transform.position = new Vector3(gameObject.GetComponentInChildren<Weapon>().transform.position.x, 1.85f, gameObject.GetComponentInChildren<Weapon>().transform.position.z);
        //    //gameObject.GetComponentInChildren<Weapon>().transform.Translate(new Vector3(Time.deltaTime * -2f, 0, 0));
        //}


        if(!animator.GetBool("isWalk")) animator.SetBool("isWalk", true);

        if (this.health <= 0)
        {
            if (isHeadshot == false)
            {
                //Weapon.StartTimer = Time.time;
                animator.SetBool("isWalk", false);
                animator.SetBool("isDeath", true);
                //gameObject.GetComponentInChildren<Weapon>().drop(true, 0.001f);
            } 
            else if(isHeadshot == true)
            {
                animator.SetBool("isWalk", false);
                animator.SetBool("isHeadshot", true);
                //gameObject.GetComponentInChildren<Weapon>().drop(true, 0.55f);
            }
                
           // Debug.Log("Enemy Death");
            
            Destroy(gameObject, 3);
            if (isDeathCounted == 0) isDeathCounted = -2;
            //Debug.Log("Death : Count : "+ count);
        }

        //if (move<=10)
        //{
        //    animator.SetBool("isWalk", true);
        //    transform.Translate(new Vector3(0, 0, Time.deltaTime * 4f));
        //    move++;
        //}


        if (isDeathCounted == -2)
        {
            count--;
            isDeathCounted = 1;
        }

    }
}
