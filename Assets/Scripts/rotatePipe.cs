using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePipe : MonoBehaviour
{
    //Creating the array of sides
    public int[] sides;

    //Booleans to check conditions
    public bool connectedup;
    public bool connecteddown;
    public bool connectedright;
    public bool connectedleft;
    public bool connectedToStart;
    public bool isStart;
    public bool isEnd;

    //Rotation speed and current rotation
    public float rotationSpeed;
    float currentRot;

    //Game manager
    public GameManager gm;

    void Start()
    {
        //Inserting the game manager into the game manager variable to use its components
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
}

    //Every frame this happens
    void Update()
    {
        //If the Unity current rotation isn't what we want it to be, we use Quaternion to move it to where we want using the rotation speed, a bit of a nicer animation
        if (transform.root.eulerAngles.z != currentRot)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, currentRot), rotationSpeed);
        }

        gm.CheckConnections();
    }

    //Every time we click it checks these
    void OnMouseDown()
    {
        //Calls rotation function
        rotation();

        //Calls the CheckConnections function in the game manager, which checks connections to pipes
        //gm.CheckConnections();

        FindObjectOfType<SoundManager>().Play("pipeClick");
    }

    public void rotation()
    {
        //Current rotation + 90
        currentRot += 90;

        //To avoid infinite numbers, small optimisation
        if (currentRot == 360)
        {
            currentRot = 0;
        }

        //Calls rotateSideValues function
        rotateSideValues();
    }

    public void rotateSideValues()
    {
        //Temporary integer to rotate the 1 or 0 mechanic of a pipe, moves the final integer to the start and bumps one up, like a stack
        int tmp = sides[0];

        for(int i = 0; i < sides.Length-1; i++)
        {
            sides[i] = sides[i + 1];
        }

        sides[3] = tmp;
    }
}
