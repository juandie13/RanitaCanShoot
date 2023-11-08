using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(CharacterController))]


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 3;
    [SerializeField]
    private float RotationSpeed = 2f;

    private Vector3 direction = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private CharacterController characterController;
    private Transform myCamera;

    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject rocketLauncher;

    [SerializeField] GameObject rocketLauncherCanvas;
    [SerializeField] GameObject shotgunCanvas;
    public GameObject mainCamera;
    public int weaponSelect=0;
    private float ultimoEjeVertical;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myCamera = transform.Find("Main Camera");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shotgunCanvas.GetComponent<Image>().color = Color.green;
        rocketLauncherCanvas.GetComponent<Image>().color = Color.white;
    }

    private void Update()
    {
        // Movimiento
        characterController.Move(
            transform.forward * direction.normalized.z * Time.deltaTime * MovementSpeed
            + transform.right * direction.normalized.x * Time.deltaTime * MovementSpeed
        );

        // Rotacion Horizontal
        transform.Rotate(
            0f,
            rotation.y * RotationSpeed * Time.deltaTime,
            0f
        );
        characterController.Move(Vector3.down * 9.82f * Time.deltaTime);

        // Rotacion vertical (camara)
        
        var rotationAngle = -rotation.x * RotationSpeed * Time.deltaTime;
        float ejevertcal = myCamera.eulerAngles.x;
        /*var desiredRotationQuat = Quaternion.Euler(transform.rotation.x + rotationAngle,
            0f,
            0f);
        

        Vector3 desiredRotation = desiredRotationQuat.eulerAngles;
        desiredRotation.x = desiredRotation.x > 180f 
            ? desiredRotation.x - 360f
            : desiredRotation.x ;

        desiredRotation.x = Mathf.Clamp(desiredRotation.x, -20f, 20f);
        /*myCamera.rotation = Quaternion.Euler(desiredRotation); */
        Debug.Log(myCamera.eulerAngles.x);
        //Mathf.Clamp(myCamera.eulerAngles.x, 90f, -68f);

        if (ejevertcal <= 360f && ejevertcal >= 290f || ejevertcal >= 0 && ejevertcal <= 70f)
        {
            myCamera.Rotate(rotationAngle, 0f, 0f);
            ultimoEjeVertical = ejevertcal;
        }
        else
        {
            myCamera.localEulerAngles = new Vector3(ultimoEjeVertical,0f,0f);
        }
        


        if (weaponSelect==0){
            shotgun.SetActive(true);
            rocketLauncher.SetActive(false);
        }else{
            shotgun.SetActive(false);
            rocketLauncher.SetActive(true);
        }
        if (GameManager.Instance.playerLifeCurrent <= 0)
        {
            Die();
            SceneManager.LoadScene("PrimeraEscena");
        }
    }

    private void OnMove(InputValue value)
    {
        var data = value.Get<Vector2>();
        direction = new Vector3(
            data.x,
            0f,
            data.y
        );
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            if(weaponSelect==0){
                myCamera.GetComponent<PlayerFire>().Fire();
            }else{
                myCamera.GetComponent<PlayerFire>().Fire();
                rocketLauncher.GetComponent<GunfireController>().shoot=true;
            }
            
        }
    }

    private void OnReload(InputValue value)
    {
        if (value.isPressed)
        {
            myCamera.GetComponent<PlayerFire>().Reload();
        }
    }

    private void OnLook(InputValue value)
    {
        var data = value.Get<Vector2>();
        rotation = new Vector3(
            data.y,
            data.x, // rotacion horizontal (sobre eje Y)
            0f
        );
    }

    private void OnSwitch(InputValue value){
        if(value.isPressed){
            if(weaponSelect==0){
                weaponSelect++;
                shotgunCanvas.GetComponent<Image>().color = Color.white;
                rocketLauncherCanvas.GetComponent<Image>().color = Color.green;
            }else{
                weaponSelect--;
                shotgunCanvas.GetComponent<Image>().color = Color.green;
                rocketLauncherCanvas.GetComponent<Image>().color = Color.white;
            }
            myCamera.GetComponent<PlayerFire>().Reload();
        }
    }
    public void Die()
    {
        GameManager.Instance.perdiste = true;
    }
}
