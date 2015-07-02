using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Player : MonoBehaviour {

	private AnimationControl anim;
	private CharacterController controller;
	private InputControl input;
	private Vector3 moveDirection = Vector3.zero;
	public float speed = 4f;
	public float gravity = 15.0f;
	public float jumpPower = 5.0f;
	public float airJumpPower = 4.5f;
	public float dashPower = 20.0f;
	public float runSpeed = 8f;

	// State variables
	public bool canJump = false;
	public bool canRun = true;
	public bool canDash = false;
	public bool isFalling = false;

	// Use this for initialization
	void Start () {
		input = new InputControl ();
		anim = new AnimationControl(gameObject.GetComponentInChildren<Animator>());
		controller = gameObject.GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		CheckInputs ();
		SetAnimations ();
		
		Vector3 acceleration = moveDirection * speed * Time.deltaTime;

		if (input.isRunning () && canRun) {
			acceleration.x = moveDirection.x * runSpeed * Time.deltaTime; // running only affects horizontal speed
			controller.Move (acceleration);
		}
		else
			controller.Move (acceleration);
	}

	// Check inputs and initializes inputController's states
	void CheckInputs()
	{
		if (Input.anyKeyDown) 
		{
			Debug.Log(input.getCurrentKey().ToString()); // print commands to the console
			
		}
		input.checkInputStates ();


	}

	// Set up the animation depending on the states & inputs recorded
	void SetAnimations()
	{
		anim.initParameters ();

		moveDirection.x = 0;

		// ON THE GROUND
		if(controller.isGrounded)
		{
			canJump = true;
			canDash = true;
			moveDirection.y = 0;

			if (input.isPunching()) {
				anim.setPunch();
			}
			if (!anim.inAttackStance()) {
				if (input.isMovingRight()) {
						moveDirection.x = 1;
					if(input.isDashingRight && canDash)
					{
						moveDirection.x *= dashPower;
					}
					else
						anim.setWalkF(true);
				}
				if (input.isMovingLeft()) {
							moveDirection.x = -1;
					if(input.isDashingLeft && canDash)
					{
						moveDirection.x *= dashPower;
					}
					anim.setWalkB(true);
					}

			
				if (input.isJumping() && canJump) {
							moveDirection.y = jumpPower;
					}
			}
		}

		// IN THE AIR
		if(!controller.isGrounded)
		{
			isFalling = (moveDirection.y < 0);

			if (input.isPunching()) {
				anim.setPunch();
			}

			if (input.isMovingRight()) {
					moveDirection.x = 1;
				if(input.isDashingRight && canDash)
				{
					canDash = false;
					moveDirection.x *= dashPower;
				}

			}
			if (input.isMovingLeft()) {
				moveDirection.x = -1;
				if(input.isDashingLeft && canDash)
				{
					moveDirection.x *= dashPower;
					canDash = false;
				}
			}
			
			if (input.isJumping() && canJump)
			{
				moveDirection.y = airJumpPower;
				canJump = false;
			}
			else if (isFalling) {

			}

			if(!input.isDashing()) moveDirection.y -= gravity * Time.deltaTime;
		}
	}
}
