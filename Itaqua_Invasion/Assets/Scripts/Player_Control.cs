using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Control : MonoBehaviour
{
    float yThrow;
    float xThrow;
    

    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFact = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFact = -20f;

    [SerializeField] float lerpSpeed = .1f;

    Vector2 curentImputVector;
    Vector2 smoothImputVelocity;

    


    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
       // SmoothControler();
    }

    void SmoothControler()
    {
        Vector2 throw_ = movement.ReadValue<Vector2>();
        curentImputVector = Vector2.SmoothDamp(curentImputVector,throw_, ref smoothImputVelocity, lerpSpeed);
        
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFact;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float roolDueTOControl = xThrow * controlRollFact;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = roolDueTOControl;
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * lerpSpeed);
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;


        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampXPos, clampYPos, transform.localPosition.z);
    }
}
