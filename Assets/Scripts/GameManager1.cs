using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage1 : MonoBehaviour
{
    //Creating the puzzle class, contains width, height, and all pipes it may include
    [System.Serializable]
    public class Puzzle
    {
        public int width;
        public int height;
        public rotatePipe[,] pipes;
    }

    //Setting the canvas to a variable to disable and reenable as needed
    public GameObject wincanvas;

    //Bool to say whether or not to generate random
    public bool generate;

    //Initialising a puzzle
    public Puzzle puzzle;

    public GameObject[] PipePrefabs;

    //First thing to happen on start up
    void Start()
    {
        wincanvas.SetActive(false);

        if (generate)
        {
            if(puzzle.width == 0 || puzzle.height == 0)
            {
                Debug.LogError("No dimensions set");
                Debug.Break();
            }

            GeneratePuzzle();

        }
        else { 
            Vector2 dimensions = CheckDimension();
            
            puzzle.width = (int)dimensions.x;
            puzzle.height = (int)dimensions.y;
            
            puzzle.pipes = new rotatePipe[puzzle.width, puzzle.height];
            
            foreach (var item in GameObject.FindGameObjectsWithTag("Pipes"))
            {
                puzzle.pipes[(int)item.transform.position.x, (int)item.transform.position.y] = item.GetComponent<rotatePipe>();
            }
            
            foreach (var item in puzzle.pipes)
            {
                Debug.Log(item.gameObject.name);
            }
        }

        Shuffle();
        CheckConnections();
    }

    void GeneratePuzzle()
    {
        puzzle.pipes = new rotatePipe[puzzle.width, puzzle.height];

        int[] auxValues = { 0, 0, 0, 0 };
        bool isEnd = false;
        bool isStart = false;
        bool isNull = false;

        for (int h = 0; h < puzzle.height; h++)
        {
            for (int w = 0; w < puzzle.width; w++)
            {
                if (w == 0 && h == 0)
                {
                    isStart = true;
                }
                else if (w != 0 && h == 0)
                {
                    isNull = true;
                }
                else if (w != puzzle.width - 1 && h == puzzle.height - 1)
                {
                    isNull = true;
                }
                else if (w == puzzle.width - 1 && h == puzzle.height - 1)
                {
                    isEnd = true;
                }

                if (isStart == true)
                {
                    GameObject go = Instantiate(PipePrefabs[1], new Vector3(w, h, 0), Quaternion.identity);
                    isStart = false;
                    puzzle.pipes[w, h] = go.GetComponent<rotatePipe>();
                }
                else if (isEnd == true)
                {
                    GameObject go = Instantiate(PipePrefabs[8], new Vector3(w, h, 0), Quaternion.identity);
                    isEnd = false;
                    puzzle.pipes[w, h] = go.GetComponent<rotatePipe>();
                }
                else if (isNull == true)
                {
                    GameObject go = Instantiate(PipePrefabs[0], new Vector3(w, h, 0), Quaternion.identity);
                    isNull = false;
                    puzzle.pipes[w, h] = go.GetComponent<rotatePipe>();
                }
                else
                {
                    GameObject go = Instantiate(PipePrefabs[Random.Range(2,8)], new Vector3(w, h, 0), Quaternion.identity);
                    puzzle.pipes[w, h] = go.GetComponent<rotatePipe>();
                }
            }
        }
    }

    void Shuffle()
    {
        foreach (var pipe in puzzle.pipes)
        {
            int i = Random.Range(0, 4);
            
            for (int k = 0; k < i; k++)
            {
                pipe.rotation();
            }
        }
    }

    public void CheckConnections()
    {
        for (int i = 0; i < puzzle.height; i++)
        {
            for(int k = 0; k < puzzle.width; k++)
            {
                if (puzzle.pipes[k, i].isStart == true)
                {
                    puzzle.pipes[k, i].connectedToStart = true;
                }
                else
                {
                    puzzle.pipes[k, i].connectedToStart = false;
                }

                //Check Up
                if (i != puzzle.height - 1)
                {
                    if (puzzle.pipes[k, i].sides[0] == 1 && puzzle.pipes[k, i + 1].sides[2] == 1 && puzzle.pipes[k, i + 1].connectedToStart == true)
                    {
                        puzzle.pipes[k, i].connectedup = true;
                        puzzle.pipes[k, i].connectedToStart = true;
                    }
                    else if (puzzle.pipes[k, i].sides[0] == 1 && puzzle.pipes[k, i + 1].sides[2] == 1)
                    {
                        puzzle.pipes[k, i].connectedup = true;
                    }
                    else
                    {
                        puzzle.pipes[k, i].connectedup = false;
                    }
                }

                //Check Right
                if (k != puzzle.width - 1)
                {
                    if (puzzle.pipes[k, i].sides[1] == 1 && puzzle.pipes[k + 1, i].sides[3] == 1 && puzzle.pipes[k + 1, i].connectedToStart == true)
                    {
                        puzzle.pipes[k, i].connectedright = true;
                        puzzle.pipes[k, i].connectedToStart = true;
                    }
                    else if (puzzle.pipes[k, i].sides[1] == 1 && puzzle.pipes[k + 1, i].sides[3] == 1)
                    {
                        puzzle.pipes[k, i].connectedright = true;
                    }
                    else
                    {
                        puzzle.pipes[k, i].connectedright = false;
                    }
                }

                //Check Down
                if (i != 0)
                {
                    if (puzzle.pipes[k, i].sides[2] == 1 && puzzle.pipes[k, i - 1].sides[0] == 1 && puzzle.pipes[k, i - 1].connectedToStart == true)
                    {
                        puzzle.pipes[k, i].connecteddown = true;
                        puzzle.pipes[k, i].connectedToStart = true;
                    }
                    else if (puzzle.pipes[k, i].sides[2] == 1 && puzzle.pipes[k, i - 1].sides[0] == 1 && puzzle.pipes[k, i - 1].connectedToStart != true)
                    {
                        puzzle.pipes[k, i].connecteddown = true;
                    }
                    else
                    {
                        puzzle.pipes[k, i].connecteddown = false;
                    }
                }

                //Check Left
                if (k != 0)
                {
                    if (puzzle.pipes[k, i].sides[3] == 1 && puzzle.pipes[k - 1, i].sides[1] == 1 && puzzle.pipes[k - 1, i].connectedToStart == true)
                    {
                        puzzle.pipes[k, i].connectedleft = true;
                        puzzle.pipes[k, i].connectedToStart = true;
                    }
                    else if (puzzle.pipes[k, i].sides[3] == 1 && puzzle.pipes[k - 1, i].sides[1] == 1)
                    {
                        puzzle.pipes[k, i].connectedleft = true;
                    }
                    else
                    {
                        puzzle.pipes[k, i].connectedleft = false;
                    }
                }

                //Check Win
                if(puzzle.pipes[k, i].isEnd == true && puzzle.pipes[k, i].connectedToStart == true)
                {

                    WinFunction();
                    Debug.Log("END");
                }
            }
                
        }
    }

    void WinFunction()
    {
        wincanvas.SetActive(true);
    }

    public int QuickSweep(int k, int i)
    {
        int value = 0;

        puzzle.pipes[k, i].connectedToStart = false;

        if (puzzle.pipes[k, i].isStart == true){
            puzzle.pipes[k, i].connectedToStart = true;
        }

        //Check Up
        if (i != puzzle.height - 1)
        {
            if (puzzle.pipes[k, i].sides[0] == 1 && puzzle.pipes[k, i + 1].sides[2] == 1 && puzzle.pipes[k, i + 1].connectedToStart == true)
            {
                puzzle.pipes[k, i].connectedup = true;
                puzzle.pipes[k, i].connectedToStart = true;
            }
            else if (puzzle.pipes[k, i].sides[0] == 1 && puzzle.pipes[k, i + 1].sides[2] == 1)
            {
                puzzle.pipes[k, i].connectedup = true;
            }
            else
            {
                puzzle.pipes[k, i].connectedup = false;
            }
        }

        //Check Right
        if (k != puzzle.width - 1)
        {
            if (puzzle.pipes[k, i].sides[1] == 1 && puzzle.pipes[k + 1, i].sides[3] == 1 && puzzle.pipes[k + 1, i].connectedToStart == true)
            {
                puzzle.pipes[k, i].connectedright = true;
                puzzle.pipes[k, i].connectedToStart = true;
            }
            else if (puzzle.pipes[k, i].sides[1] == 1 && puzzle.pipes[k + 1, i].sides[3] == 1)
            {
                puzzle.pipes[k, i].connectedright = true;
            }
            else
            {
                puzzle.pipes[k, i].connectedright = false;
            }
        }

        //Check Down
        if (i != 0)
        {
            if (puzzle.pipes[k, i].sides[2] == 1 && puzzle.pipes[k, i - 1].sides[0] == 1 && puzzle.pipes[k, i - 1].connectedToStart == true)
            {
                puzzle.pipes[k, i].connecteddown = true;
                puzzle.pipes[k, i].connectedToStart = true;
            } 
            else if(puzzle.pipes[k, i].sides[2] == 1 && puzzle.pipes[k, i - 1].sides[0] == 1 && puzzle.pipes[k, i - 1].connectedToStart != true)
            {
                puzzle.pipes[k, i].connecteddown = true;
            }
            else
            {
                puzzle.pipes[k, i].connecteddown = false;
            }
        }

        //Check Left
        if (k != 0)
        {
            if (puzzle.pipes[k, i].sides[3] == 1 && puzzle.pipes[k - 1, i].sides[1] == 1 && puzzle.pipes[k - 1, i].connectedToStart == true)
            {
                puzzle.pipes[k, i].connectedleft = true;
                puzzle.pipes[k, i].connectedToStart = true;
            }
            else if (puzzle.pipes[k, i].sides[3] == 1 && puzzle.pipes[k - 1, i].sides[1] == 1)
            {
                puzzle.pipes[k, i].connectedleft = true;
            }
            else
            {
                puzzle.pipes[k, i].connectedleft = false;
            }
        }

        return value;
    }

    Vector2 CheckDimension()
    {
        Vector2 tmp = Vector2.zero;

        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipes");

        foreach (var pipe in pipes)
        {
            if (pipe.transform.position.x > tmp.x)
            {
                tmp.x = pipe.transform.position.x;
            }

            if (pipe.transform.position.y > tmp.y)
            {
                tmp.y = pipe.transform.position.y;
            }
        }

        tmp.y++;
        tmp.x++;

        return tmp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
