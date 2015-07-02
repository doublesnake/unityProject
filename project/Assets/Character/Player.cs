using UnityEngine;
using System.Collections;

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

	// State variables
	public bool canJump = false;
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

		controller.Move (moveDirection * speed * Time.deltaTime);
	}

	// Check inputs and initializes inputController's states
	void CheckInputs()
	{

		// Check doubleTap state
		input.checkDash ();
		if (Input.anyKeyDown) 
		{
			Debug.Log(input.getCurrentKey().ToString()); // print commands to the console

		}

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

	/* Class to manage animations */
	public class AnimationControl
	{
		public Animator anim;

		// Animations ID
		public string walk_f = "walk_forward";
		public string walk_b = "walk_backward";
		public string punch = "punch";
		public string idle = "idle";
		
		// Constructor
		public AnimationControl( Animator a)
		{
			anim = a;
		}

		public bool inAttackStance()
		{
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
				return true;
			return false;
		}
		public bool inMoveStance()
		{
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkForward"))
				return true;
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkBackward"))
				return true;
			return false;
		}
		public void initParameters()
		{
			setWalkF (false);
			setWalkB (false);
		}

		public void setWalkF(bool value){
				anim.SetBool (walk_f, value);
		}
		public void setWalkB(bool value){
			anim.SetBool (walk_b, value);
		}
		public void setIdle(bool value){
			anim.SetBool (idle, value);
		}
		public void setPunch(){
			//anim.SetTrigger (punch);
			anim.CrossFade("Punch",0);
		}
	}

	/* Class to manage inputs and mapping */
	public class InputControl
	{
		// Double Tap variables
		public float tapCooldown = 0.3f;
		public int tapCount = 0;
		public float lastTapTime = 0;
		public bool doubleTap = false;
		public KeyCode lastKeyDown = KeyCode.None;


		// Keycode Mapping variables
		public KeyCode left;
		public KeyCode up;
		public KeyCode right;
		public KeyCode punch;

		// State variables
		private bool canMove;
		public bool isDashingLeft;
		public bool isDashingRight;

		// Constructor
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
				return true;
			}
			return false;
		}

		public bool isJumping()
		{
			if (Input.GetKeyDown (up)) 
					return true;
			return false;
		}

		public bool isMovingRight()
		{
			if (Input.GetKey (right)) 
			{
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

		public void checkDash()
		{
			doubleTap = false;
			isDashingLeft = false;
			isDashingRight = false;
			if (Input.anyKeyDown) {
								KeyCode currentKey = getCurrentKey ();
								if (tapCount == 1 && currentKey == lastKeyDown) {
					
										if (Time.time - lastTapTime < tapCooldown) {
												doubleTap = true;
												if (currentKey == left)
														isDashingLeft = true;
												if (currentKey == right)
														isDashingRight = true;
												tapCount = 0;
										}
								}
								if (!doubleTap) {
										lastKeyDown = (currentKey != KeyCode.None) ? currentKey : lastKeyDown;
										lastTapTime = Time.time;
					
										tapCount = 1;
								}
						}
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
