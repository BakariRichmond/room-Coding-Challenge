using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator anim;
    public GameObject MainCam;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.5f;
    [SerializeField]
    public AudioSource audioSource1;
    [SerializeField]
    public AudioSource audioSource2;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float updateAngle;

    private void Start () {

    }

    void Awake(){
        audioSource1.Pause();

    }

    void Update () {
        //set the player rotation to most updated camera angle
        if (updateAngle != 0) {
            this.transform.eulerAngles = new Vector3 (0, updateAngle, 0);
        } else if (Input.GetAxis ("Vertical") == 1) {
            this.transform.eulerAngles = new Vector3 (0, 0, 0);

        }
        //check if grounded and set bool for jump animation accordingly
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
            anim.SetBool ("isGrounded", true);
        } else {
            anim.SetBool ("isGrounded", false);
        }
        //sets the player movement based off of input and camera direction
        Vector3 move = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

        if (move != Vector3.zero) {

            move = MainCam.transform.TransformDirection (move);

            updateAngle = Mathf.Atan2 (move.x, move.z) * Mathf.Rad2Deg;
            move.y = 0.0f;
            //gameObject.transform.forward = move;

            controller.Move (move * Time.deltaTime * playerSpeed);

            anim.SetBool ("isWalking", true);
           
            audioSource1.UnPause();

        } else {
            anim.SetBool ("isWalking", false);
            audioSource1.Pause();
        }

        //Updates Y position of player(jumping)
        if (Input.GetButtonDown ("Jump") && groundedPlayer) {
            audioSource2.Play(0);
            playerVelocity.y += Mathf.Sqrt (jumpHeight * -3.0f * gravityValue);
        }
        //implements gravity force
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move (playerVelocity * Time.deltaTime);
    }
}