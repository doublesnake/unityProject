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
	public bool isIdle = true;
	public bool isGrounded;
	public bool isFalling = false;

	// Use this for initialization
	void Start () {
		input = new InputControl ();
		anim = new AnimationControl(gameObject.GetComponentInChildren<Animator>());
		controller = gameObject.GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 acceleration = moveDirection * speed * Time.deltaTime;
		
		if (input.isRunning () && canRun) {
			acceleration.x = moveDirection.x * runSpeed * Time.deltaTime; // running only affects horizontal speed
			controller.Move (acceleration);
		}
		else
			controller.Move (acceleration);

		CheckInputs ();
		SetAnimations ();


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
		isIdle = true;
		anim.initParameters ();

		moveDirection.x = 0;

		// ON THE GROUND
		if(controller.isGrounded)
		{
			isFalling = false;
			anim.setFall(isFalling);
			isGrounded = true;
			canJump = true;
			canDash = true;
			moveDirection.y = 0;
			anim.setIsGrounded(true);
			if (input.isPunching()) {
				isIdle = false;
				anim.setPunch();
			}
			if (!anim.inAttackStance()) {
				if (input.isMovingRight()) {
					isIdle = false;
						moveDirection.x = 1;
						anim.setDirection(moveDirection.x);
					if(input.isDashingRight && canDash)
					{
						moveDirection.x *= dashPower;
					}
				}
				if (input.isMovingLeft()) {
					isIdle = false;
					moveDirection.x = -1;
					anim.setDirection(moveDirection.x);
					if(input.isDashingLeft && canDash)
					{
						moveDirection.x *= dashPower;
					}
				}

			
				if (input.isJumping() && canJump) {
					isIdle = false;
					moveDirection.y = jumpPower;
					anim.setJump();
					}

				moveDirection.y -= 0.001f * Time.deltaTime;
				anim.setIdle (isIdle);
			}
		}

		// IN THE AIR
		if(!controller.isGrounded)
		{
			isGrounded = false;
			isIdle = false;
			anim.setIsGrounded(false);
			isFalling = (moveDirection.y < 0);

			if (input.isPunching()) {
				anim.setPunch();
			}

			if (input.isMovingRight()) {
				moveDirection.x = 1;
				anim.setDirection(moveDirection.x);
				if(input.isDashingRight && canDash)
				{
					canDash = false;
					moveDirection.x *= dashPower;
				}

			}
			if (input.isMovingLeft()) {
				moveDirection.x = -1;
				anim.setDirection(moveDirection.x);
				if(input.isDashingLeft && canDash)
				{
					moveDirection.x *= dashPower;
					canDash = false;
				}
			}
			
			if (input.isJumping() && canJump)
			{
				moveDirection.y = airJumpPower;
				anim.setJump();
				canJump = false;
			}
			else if (isFalling) {
				anim.setFall(isFalling);
			}

			if(!input.isDashing()) moveDirection.y -= gravity * Time.deltaTime;

		}
		if(moveDirection.x == 0) anim.setIsMoving(false);
		else anim.setIsMoving(true);
	}
}
