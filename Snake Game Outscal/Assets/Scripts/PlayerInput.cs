using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;

    private int horizontal = 0, vertical = 0;

    public enum Axis
    {
        VERTICAL,
        HORIZONTAL
    }
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = 0;
        vertical = 0;

        GetKeyBoardInput();
        SetMovement();
    }

    private void GetKeyBoardInput()
    {
        //horizontal = (int)Input.GetAxisRaw("Horizontal");
        //vertical   = (int)Input.GetAxisRaw("Vertical");

        horizontal = GetAxisraw(Axis.HORIZONTAL);
        vertical = GetAxisraw(Axis.VERTICAL);

        if (horizontal != 0)
        {
            vertical = 0;
        }
    }

    void SetMovement()
    {
        if (vertical != 0)
        {
            playerController.SetInputDirection(vertical == 1 ? PlayerDirection.UP : PlayerDirection.DOWN);
        }
        else if (horizontal != 0)
        {
            playerController.SetInputDirection(horizontal == 1 ? PlayerDirection.RIGHT : PlayerDirection.LEFT);
        }
    }

    int GetAxisraw(Axis axis)
    {
        if (axis == Axis.HORIZONTAL)
        {
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);

            if (left)
            {
                return -1;
            }
            if (right)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        if (axis == Axis.VERTICAL)
        {
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            if (down)
            {
                return -1;
            }
            if (up)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        return 0;
    }
}