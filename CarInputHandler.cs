using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    CarController topDownCarController;


    private void Awake()
    {
        topDownCarController = GetComponent<CarController>();
    }


    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.Space)) {
            inputVector.x = Input.GetAxis("Horizontal");
            topDownCarController.SetInputVector(inputVector);
        } else
        {


            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            topDownCarController.SetInputVector(inputVector);

        }


    }
}
