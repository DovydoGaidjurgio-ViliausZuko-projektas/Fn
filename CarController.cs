using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFarctor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;
    public float decelerationFactor = 20.0f; 

    float accelerationInput = 0;
    float steeringInput = 0; 


    float rotationAngle = 0;
    float velocityVsUp = 0;
    


   

    Rigidbody2D carRigidbody2D;

    private void Awake() 
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            Decelerate();
        }

       
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApllySteering();

        
        
    }

    // Judejimas i prieki
    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFarctor ;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    // Ratu sukimasis
    void ApllySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        carRigidbody2D.MoveRotation(rotationAngle);
    }


    void KillOrthogonalVelocity()
    {
        Vector2 fowardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = fowardVelocity + rightVelocity * driftFactor;
    }

    //Deceleracija
    void Decelerate()
    {
        carRigidbody2D.velocity -= carRigidbody2D.velocity.normalized * decelerationFactor * Time.deltaTime;
    }


    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

}