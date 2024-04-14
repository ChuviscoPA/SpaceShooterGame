using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Control : MonoBehaviour
{
    float yThrow;
    float xThrow;

    float fire = 1f;

    Vector2 curentImputVector;
    Vector2 smoothImputVelocity;

    [Header ("Imput Settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction imputActions;


    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float controlSpeed = 20f;
    [Tooltip("How far player moves horizontally")][SerializeField] float xRange = 10f;
    [Tooltip("How far player moves vertically")][SerializeField] float yRange = 7f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] Lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;  
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFact = -10f;
    [SerializeField] float controlRollFact = -20f;
    [SerializeField] float lerpSpeed = .1f;

    void OnEnable()
    {
        movement.Enable();
        imputActions.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        imputActions.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFire();
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

    public void ProcessTranslation()
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

    void ProcessFire()
    {
        fire = imputActions.ReadValue<float>();

        if (imputActions.IsPressed())
        {
            ActivateLasers(true);
            
        }
        else
        {
            ActivateLasers(false);
        }

    }

    void ActivateLasers(bool isActive)
    {       
            foreach (GameObject laser in Lasers)
            {
                laser.SetActive(true);

                var emissionModule = laser.GetComponent<ParticleSystem>().emission;
                emissionModule.enabled = isActive;
            }             
    }
        
}
