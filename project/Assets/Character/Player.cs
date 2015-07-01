using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;
	private InputControl input;
	private Vector3 moveDirection = Vector3.zero;
	public float speed = 4f;
	public float gravity = 15.0f;
	public float jumpPower = 5.0f;
	public float dashPower = 20.0f;
	// State variables
	public bool facingRight = true;
	public bool canJump = false;
	private bool enableMotion = true;
	public bool isFalling = false;

	// Use this for initialization
	void Start () {
		input = new InputControl ();
		anim = gameObject.GetComponentInChildren<Animator>();
		controller = gameObject.GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		CheckStates ();
		CheckInputs ();
		SetAnimations ();

		controller.Move (moveDirection * speed * Time.deltaTime);
		/*if (moveDirection.x > 0 && !facingRight)
						Flip ();
		else if(moveDirection.x < 0 && facingRight)
		        Flip ();
		        */
	}

	void CheckStates()
	{
		
		if (controller.isGrounded) canJump = true;
		else canJump = false;

		if (!controller.isGrounded && moveDirection.y < 0)
						isFalling = true;
				else
						isFalling = false;

		enableMotion = true;
	}

	void CheckInputs()
	{
		
		input.doubleTap = false;
		if (Input.anyKeyDown) 
		{
			KeyCode currentKey = input.getCurrentKey();
			if(input.tapCount == 1 && currentKey == input.lastKeyDown)
			{

				if(Time.time - input.lastTapTime < input.tapCooldown)
				{
					input.doubleTap = true;
					input.tapCount = 0;
				}
			}
			if(!input.doubleTap)
			{
				input.lastKeyDown = (currentKey!=KeyCode.None)? currentKey: input.lastKeyDown;
				input.lastTapTime = Time.time;
			
			 	input.tapCount = 1;
			}

			
			Debug.Log(currentKey.ToString());

		}


	}
	void SetAnimations()
	{
		anim.SetBool ("walk_forward", false);
		anim.SetBool ("walk_backward", false);
		anim.SetBool ("idle",false);



		moveDirection.x = 0;

		if(controller.isGrounded)
		{
			moveDirection.y = 0;
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
			{
				enableMotion = false;
			}

			if (input.isPunching()) {
				enableMotion = false;
				anim.CrossFade("Punch",0);
			}
			if (enableMotion) {
				if (input.isMovingRight()) {
						moveDirection.x = 1;
					if(input.isDashing ())
					{
						moveDirection.x *= dashPower;
					}
					else
						anim.SetBool ("walk_forward", true);
				}
				if (input.isMovingLeft()) {
							moveDirection.x = -1;
					if(input.isDashing ())
					{
						moveDirection.x *= dashPower;
					}
					anim.SetBool ("walk_backward", true);
					}

			
				if (input.isJumping() && canJump) {
							moveDirection.y = jumpPower;
					}
			}
		}

		if(!controller.isGrounded)
		{
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
			{
				enableMotion = false;
			}
			
			if (input.isPunching()) {
				enableMotion = false;
				//anim.SetTrigger("punch");
				anim.CrossFade("Punch",0);
			}

			if (input.isMovingRight()) {
					moveDirection.x = 1;
				if(input.isDashing ())
					moveDirection.x *= dashPower;

			}
			if (input.isMovingLeft()) {
				moveDirection.x = -1;
				if(input.isDashing ())
					moveDirection.x *= dashPower;
			}
			
			
			if (isFalling) {

			}

			if(!input.isDashing()) moveDirection.y -= gravity * Time.deltaTime;
		}
	}
	void Flip()
		        {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
			transform.localScale = theScale;
		}

	public class InputControl
	{
		
		public float tapCooldown = 0.3f;
		public int tapCount = 0;
		public float lastTapTime = 0;
		public bool doubleTap = false;
		public KeyCode lastKeyDown = KeyCode.None;



		public KeyCode left;
		public KeyCode up;
		public KeyCode right;
		public KeyCode punch;

		private bool canMove;
		private bool isDashingLeft;
		private bool isDashingRight;


		public InputControl()
		{
			left = KeyCode.A;
			up = KeyCode.W;
			right = KeyCode.D;
			punch = KeyCode.Keypad1;
		}

		public bool isMovingLeft()
		{
			if (Input.GetKey (left)) 
			{
				isDashingLeft = doubleTap;
				return true;
			}
			return false;
		}
		public bool isJumping()
		{
			if (Input.GetKey (up)) 
					return true;
			return false;
		}
		public bool isMovingRight()
		{
			if (Input.GetKey (right)) 
			{
				isDashingRight = doubleTap;
				return true;
			}
			return false;
		}
		public bool isDashing()
		{
			if (isDashingLeft || isDashingRight)
				return true;
			return false;
		}

		public bool isPunching()
		{
			if (Input.GetKeyDown (punch))
				return true;
			return false;
		}

		public KeyCode getCurrentKey()
		{
			if (Input.GetKeyDown (left))
					return left;
			if (Input.GetKeyDown (right))
					return right;
			if (Input.GetKeyDown (up))
					return up;
			if (Input.GetKeyDown (punch))
					return punch;
			return KeyCode.None;
		}
	}
}
