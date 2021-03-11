using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapMove : MonoBehaviour
{
    private bool selected = false;
    private float startPosY;
    private float startPosX;
    private float newPosY;
    private float newPosX;

    // Update is called once per frame
    void Update(){
        if(selected == true){
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosY = cursorPos.y - startPosY;
            newPosX = cursorPos.x - startPosX;
            transform.position = new Vector2(newPosX, newPosY);
        }
    }

    void OnMouseDown(){
        if(Input.GetMouseButtonDown(0)){
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selected = true;
            startPosX = cursorPos.x - transform.position.x;
            startPosY = cursorPos.y - transform.position.y;
        }
    }

    void OnMouseUp(){
        selected = false;
    }

}
