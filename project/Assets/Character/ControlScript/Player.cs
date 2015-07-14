﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Player : MonoBehaviour {

	private AnimationControl anim;
	private CharacterController controller;
	private InputControl input;
	private Vector3 moveDirection = Vector3.zero;
	public int id = 1;
	public float speed = 4.0f;
	public float gravity = 15.0f;
	public float jumpPower = 5.0f;
	public float airJumpPower = 4.5f;
	public float dashPower = 4.0f;
	public float runSpeed = 8f;
	public float antiDashFactor = 3f;
	private float antiDashPower;
	public float runBoostPower = 3f;
	private bool runBoost;

	private float directionX;
	private float directionY;
	public KeyCode dashDirection;

	// State variables
	private bool canJump = true;
	private bool canRun = true;
	private bool canAirDashLeft = true;
	private bool canAirDashRight = true;

	public bool isRunning;
	public bool isAirDashing;
	public bool isJumping;
	public bool isIdle;
	public bool isMoving;
	public bool isGrounded;
	public bool isFalling;




	// Use this for initialization
	void Start () {
		input = new InputControl (id);
		anim = new AnimationControl(gameObject.GetComponentInChildren<Animator>());
		controller = gameObject.GetComponent<CharacterController> ();

		antiDashPower = antiDashFactor*dashPower;


	}
	
	// Update is called once per frame
	void Update () {

		InitStates ();
		SetAnimations ();
		MoveCharacter ();

	}

	// Reinitialization of states for each frames
	void InitStates()
	{
		isIdle = true;
		isMoving = false;

		// ON THE GROUND
		if (controller.isGrounded) 
		{
			moveDirection.x = Global.NONE;
			moveDirection.y = Global.NONE;
			canAirDashLeft = true;
			canAirDashRight = true;
			isAirDashing = false;
			canJump = true;
			isFalling = false;
			isGrounded = true;
			anim.setGrounded(true);
			anim.setFall(false);

		} 
		// IN THE AIR
		else 
		{
			isGrounded = false;
			isFalling = (moveDirection.y < -0.0015f * Time.deltaTime);
			anim.setGrounded(false);
			anim.setCrouch (false);
		}

	}

	// Set the acceleration to move the character
	void MoveCharacter()
	{
 		// Normal acceleration
		Vector3 acceleration = moveDirection * speed * Time.deltaTime;

		// Dash before run
		if(runBoost) 
		{
			if(moveDirection.x>0) moveDirection.x += runBoostPower;
			if(moveDirection.x<0) moveDirection.x -= runBoostPower;
			runBoost = false;
		}

		// Running
		if (isRunning && canRun) {
			acceleration.x = moveDirection.x * runSpeed * Time.deltaTime; // running only affects horizontal speed
		}
		controller.Move (acceleration);

	}
	
	// Set up the animation depending on the states & inputs recorded
	void SetAnimations()
	{
		// print all commands to the console
		if (Input.anyKeyDown) 
		{
			Debug.Log(input.getCurrentKey().ToString()); 
			
		}

		input.checkInputStates (); // set input states

		// check airdash state
		if (input.doubleTap && !controller.isGrounded) {
			if ((input.doubleTapDirection == input.left && canAirDashLeft) || (input.doubleTapDirection == input.right && canAirDashRight)) {
				dashDirection = input.doubleTapDirection;
				isAirDashing = true;
			}
		}
		// check running state
		if (input.doubleTap && controller.isGrounded) {
			isRunning = true;
			runBoost = true;
		}
		// stop running state when run button is released
		if (!(input.doubleTapDown || (input.doubleTap && controller.isGrounded)))
		{
			isRunning = false;
		}
		
		anim.setAirDash (isAirDashing);
		
		// ON THE GROUND
		if(controller.isGrounded)
		{
			GroundControls();
			
		}
		
		// IN THE AIR
		if (!controller.isGrounded) {
			AirControls();
		}
	}

	// Set up groundcontrols animations
	void GroundControls()
	{

		if (input.isPunching()) {
			isIdle = false;
			isRunning = false;
			anim.setPunch();
		}
		
		if (!input.isMovingRight()&& !input.isMovingLeft())
		{
			anim.setDirectionX(Global.NONE);
		}
		
		if (!input.isCrouching ()) {
			anim.setCrouch (false);
		}

		if (input.isMovingRight()) {
			isIdle = false;
			isMoving = true;
			if (input.isCrouching ()) moveDirection.x = Global.NONE;
			else moveDirection.x = 1;
			anim.setDirectionX(Global.RIGHT);
		}
		if (input.isMovingLeft()) {
			isIdle = false;
			isMoving = true;
			if (input.isCrouching ()) moveDirection.x = Global.NONE;
			else moveDirection.x = -1;
			anim.setDirectionX(Global.LEFT);
		} 
		if (input.isCrouching ()) {
			isIdle = false;
			isMoving = false;
			isRunning = false;
			anim.setCrouch (true);
		}
		if (input.isJumping() && canJump) {
			isIdle = false;
			moveDirection.y = jumpPower;
			anim.setJump();
		}
		anim.setMoving (isMoving);
		if (isIdle) {anim.setMoving (false);}
		anim.setIdle (isIdle);
		
		moveDirection.y -= 0.001f * Time.deltaTime; // To compensate the IsGrounded bug
	}

	// Set up aircontrols animations
	void AirControls()
	{
		
		if (input.isPunching()) {
			isIdle = false;
			anim.setPunch();
		}

		if (!input.isMovingRight()&& !input.isMovingLeft())
		{
			anim.setDirectionX(Global.NONE);
		}
		// AIR - DASHING
		if (isAirDashing) {
			
			isIdle = false;
			isMoving = true;
			moveDirection.y = Global.NONE;


			if(canAirDashRight && dashDirection == input.right)
			{
				moveDirection.x = dashPower;
				canAirDashRight = false;
			}
			if(canAirDashLeft && dashDirection == input.left)
			{
				moveDirection.x = -dashPower;
				canAirDashLeft = false;
			}

			if(dashDirection == input.right) 
			{
				anim.setDirectionX(Global.RIGHT);
				moveDirection.x -= antiDashPower * Time.deltaTime;
				if(moveDirection.x<1 || input.isMovingLeft()){
					isAirDashing = false;
				}
			}
			if(dashDirection == input.left){
				anim.setDirectionX(Global.LEFT);
				moveDirection.x += antiDashPower * Time.deltaTime;
				if(moveDirection.x>-1  || input.isMovingRight()){
					isAirDashing = false;
				}
			}
			if(input.isJumping () && canJump){
				isAirDashing = false;
				moveDirection.y = airJumpPower;
				anim.setJump ();
				if(dashDirection == input.left) moveDirection.x = Global.LEFT;
				else if(dashDirection == input.right) moveDirection.x = Global.RIGHT;
				else moveDirection.x = Global.NONE;
				canJump = false;
			}
			

		} 
		// AIR - NORMAL
		else 
		{

			if (input.isMovingRight ()) {
					isIdle = false;
					isMoving = true;
				moveDirection.x = Global.RIGHT;
					anim.setDirectionX (moveDirection.x);
			}
			if (input.isMovingLeft ()) {
					isIdle = false;
					isMoving = true;
				moveDirection.x = Global.LEFT;
					anim.setDirectionX (moveDirection.x);
			}

			if (input.isJumping () && canJump) {
					isIdle = false;
					isMoving = false;
					moveDirection.y = airJumpPower;
				if(input.isMovingLeft()) moveDirection.x = Global.LEFT;
				else if(input.isMovingRight()) moveDirection.x = Global.RIGHT;
				else moveDirection.x = Global.NONE;
					anim.setJump ();
					canJump = false;
			}

			if (isFalling) {
					isIdle = false;
					anim.setFall (isFalling);
			}


			moveDirection.y -= gravity * Time.deltaTime; // Gravity effect

		}
		anim.setMoving (isMoving);
	}

	void OnTouched(int damageType)
	{
		if (damageType == Global.HIGH) 
		{

		}
		if (damageType == Global.MIDDLE) 
		{
		
		}
		if (damageType == Global.LOW) 
		{

		}
	}
	/*
	void OnTouchedExit(int damageType)
	{
		if (type == -1) 
		{
			anim.anim.SetBool ("hitboxHigh", false);
			anim.anim.SetBool ("hitboxLow", false);
			
		}
		if(type == 0)
			anim.anim.SetBool ("hitboxHigh",false);
		if(type == 2)
			anim.anim.SetBool ("hitboxLow",false);
	}*/
}
