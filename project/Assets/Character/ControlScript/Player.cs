using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Linq;
using System;


public class Player : MonoBehaviour {

	private AnimationControl anim;
	private Rigidbody2D controller;
	private InputControl input;
	private Vector2 moveDirection = Vector2.zero;
	public int id = 1;
	public float speed = 4.0f;
	public float gravity = 15.0f;
	public float jumpPower = 4500f;
	public float airJumpPower = 4.5f;
	public float dashSpeed = 6f;
	public float runSpeed = 6f;
	public float antiDashPower = 20f;
	public float runBoostPower = 2.5f;
	private float gravityScale = 5;
	public int dashDuration = 5;
	public int dashCounter = 0;
	public int airDashDuration = 10;
    public int airDashCounter = 0;
    public float attackTimer = 0f;
    public float attackCooldown = 0.2f;
    public float velocityX;
    public float velocityY;
    public float distance;
    Transform armature;

	public Direction dashDirection;

    public bool doubleJump;


    public float currentPV;
    public float PVMax;

    public int currentSP;
    public int SPMax;

    public Command lastAttack = null;

	// State variables
	public bool canJump = true;
	private bool canRun = true;
	private bool canAirDashLeft = true;
	private bool canAirDashRight = true;

	public bool isRunning;
    public bool isDashing;
    public bool isJumping;
    public bool isDoubleJumping;
	public bool isAirDashing;
	public bool up;
	public bool isGravity;
	public bool isIdle;
	public bool isMoving;
	public bool isGrounded;
	public bool isFalling;

    public bool isAttacking;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

    private Character character = new Character();
    private Transform enemy;

	// Use this for initialization
	void Start () {
		input = new InputControl (character.commandList, id);
		anim = new AnimationControl(gameObject.GetComponentInChildren<Animator>());
		controller = gameObject.GetComponent<Rigidbody2D> ();
        armature = transform.Find("Armature");
		controller.freezeRotation = true;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.transform != this.transform)
            {
                enemy = player.transform;
                //break;
            }
        }

        PVMax = character.PV;
        SPMax = character.SPMax;

        canJump = true;

	}
    void Update()
    {
        FollowTarget();
        InitStates();
        CheckInput();
    }
	// Update is called once per frame
    void FixedUpdate()
    {
       // FollowTarget();
		SetAnimations ();
		MoveCharacter ();
	}

	// Reinitialization of states for each frames
	void InitStates()
	{
		isIdle = true;
		isMoving = false;
		
		doubleJump = false;
		isGravity = true;

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		// ON THE GROUND
		if (isGrounded) 
		{
			moveDirection.x = (int)Direction.NONE;
			moveDirection.y = (int)Direction.NONE;
			canAirDashLeft = true;
			canAirDashRight = true;
            isAirDashing = false;
			isFalling = false;
			isGrounded = true;
			anim.setGrounded(true);
			anim.setFall(false);
            isDoubleJumping = false;

		} 
		// IN THE AIR
		else 
		{
            isJumping = false;
			isDashing = false;
			isGrounded = false;
			isFalling = (controller.velocity.y < -0.001f);
			anim.setGrounded(false);
			anim.setCrouch (false);
		}

	}
    void FollowTarget()
    {
        distance = transform.position.x - enemy.position.x;
        if(transform.position.x < enemy.position.x)
        {
            armature.localScale = new Vector3(Math.Abs(armature.localScale.x), armature.localScale.y, armature.localScale.z);
            anim.characterFlip(false);
        }
        else
        {
            armature.localScale = new Vector3(-Math.Abs(armature.localScale.x), armature.localScale.y, armature.localScale.z);
            anim.characterFlip(true); 
        }
        
    }

	// Set the acceleration to move the character
	void MoveCharacter()
	{
		//if(isGravity) moveDirection.y -= gravity; // Gravity effect


		if (isGravity) controller.gravityScale = gravityScale;
		else controller.gravityScale = 0;

        if (isAttacking)
        {
            if (isGrounded)
            {
                if (isDashing) isDashing=false;
                return;
            }
        }

		if (isAirDashing) {
            controller.velocity = new Vector2(moveDirection.x * dashSpeed, 0);

            velocityX = controller.velocity.x;
            velocityY = controller.velocity.y;
            anim.setVelocityX(velocityX);
            anim.setVelocityY(velocityY);

			airDashCounter -= 1;
			if (airDashCounter <= 0) isAirDashing = false;
			return;
		}
		else if (isDashing) {
            controller.velocity = new Vector2(moveDirection.x * dashSpeed, controller.velocity.y);

            anim.setRun(true);
            velocityX = controller.velocity.x;
            velocityY = controller.velocity.y;
            anim.setVelocityX(velocityX);
            anim.setVelocityY(velocityY);
			dashCounter -= 1;
			if (dashCounter <= 0) isDashing = false;
			return;
		}

		// Normal acceleration
		float accelerationX = moveDirection.x * speed;


        if(!isRunning || !isGrounded)
        {
            anim.setRun(false);
        }

		// Running
		if (isRunning && canRun) {
            accelerationX = moveDirection.x * runSpeed; // running only affects horizontal speed
            anim.setRun(true);
		}
        
        
		controller.velocity = new Vector2(accelerationX, controller.velocity.y);

        velocityX = controller.velocity.x;
        velocityY = controller.velocity.y;
        anim.setVelocityX(velocityX);
        anim.setVelocityY(velocityY);
	}
	void CheckInput()
	{
		
		/*// print all commands to the console
		if (Input.anyKeyDown) 
		{
			if(Input.GetButtonDown("Horizontal1"))
			   Debug.Log("lol"); 
			   
			   }*/


        /* MOVE INPUTS*/


        // Check move
        input.checkInputStates();

        // check airdash state
        if (input.isDoubleTap() && !isGrounded)
        {
            if ((input.lastTap == Direction.LEFT && canAirDashLeft) || (input.lastTap == Direction.RIGHT && canAirDashRight))
            {
                dashDirection = input.lastTap;
                isAirDashing = true;
            }
        }
        // check running state
        if (input.isDoubleTap() && isGrounded)
        {
            isRunning = true;
            isDashing = true;
            dashDirection = input.lastTap;
            dashCounter = dashDuration;
        }
        // stop running state when run button is released
        if (!(input.isDoubleTapDown() || (input.isDoubleTap() && isGrounded)))
        {
            isRunning = false;
        }

        /* ATTACK INPUTS*/

        // Return if AttackCooldown isn't over
        bool canAttack = ((Time.time - attackTimer) >= attackCooldown);
        if (!canAttack) return;

        // Return if Attack animation isn't over
        if (lastAttack != null && isAttacking)
        {
            isAttacking = !anim.isAnimationOver(lastAttack);
            if (isAttacking) return;
        }

        // Check attack
        input.checkAttack();

        Command attack = null; 
        isAttacking = false;

        // Check if the player is executing an attack
        foreach(Command available in input.availableAttack)
        {
            if(available.condition == null)
            {
                attack = available;
                break;
            }
            else if(available.condition.checkPreCondition(this))
            {
                attack = available;
                //this = attack.condition.setPostCondition(this);
                break;
            }
        }

        // Set the attack to execute if existing
        if (attack != null)
        {
            lastAttack = attack;
            isAttacking = true;

            attackTimer = Time.time;
            anim.setAttack(attack);
            Debug.Log("Attack");
            return;

        }

	}

	// Set up the animation depending on the states & inputs recorded
	void SetAnimations()
	{

		
		// ON THE GROUND
		if(isGrounded)
		{
			GroundControls();
			
		}
		
		// IN THE AIR
		if (!isGrounded) {
			AirControls();
		}
	}

	// Set up groundcontrols animations
	void GroundControls()
	{

		// DASHING
		if (isDashing) {
			isIdle = false;
			isMoving = true;
			moveDirection.y = (int)Direction.NONE;

						if (dashDirection == Direction.RIGHT) {
								anim.setDirectionX ((int)Direction.RIGHT);
								moveDirection.x = (int)Direction.RIGHT;
						}
						else if (dashDirection == Direction.LEFT) {
								anim.setDirectionX ((int)Direction.LEFT);
								moveDirection.x = (int)Direction.LEFT;
						}
                        if (input.up() && !isJumping && canJump)
                        {
								isDashing = false;
                                isJumping = true;
								controller.velocity = new Vector2 (controller.velocity.x, 0);
								controller.AddForce (new Vector2 (0, jumpPower));
								anim.setJump ();
								if (dashDirection == Direction.LEFT)
										moveDirection.x = (int)Direction.LEFT;
								else if (dashDirection == Direction.RIGHT)
										moveDirection.x = (int)Direction.RIGHT;
								else
										moveDirection.x = (int)Direction.NONE;
						}
						if (input.down ()) {
							isIdle = false;
							isMoving = false;
							isDashing = false;
							isRunning = false;
							moveDirection.x = (int)Direction.NONE;
							anim.setCrouch (true);
						}
			
				} else {
						if (!input.right () && !input.left ()) {
								anim.setDirectionX ((int)Direction.NONE);
						}
			
						if (!input.down ()) {
								anim.setCrouch (false);
						}

						if (input.right ()) {
								isIdle = false;
								isMoving = true;
								if (input.down ())
										moveDirection.x = (int)Direction.NONE;
								else
										moveDirection.x = 1;
								anim.setDirectionX ((int)Direction.RIGHT);
						}
						if (input.left ()) {
								isIdle = false;
								isMoving = true;
								if (input.down ())
										moveDirection.x = (int)Direction.NONE;
								else
										moveDirection.x = -1;
								anim.setDirectionX ((int)Direction.LEFT);
						} 
						if (input.down ()) {
								isIdle = false;
								isMoving = false;
								isRunning = false;
								anim.setCrouch (true);
						}
                        if (input.up() && !isJumping && canJump)
                        {
								isIdle = false;
                                isJumping = true;
								controller.velocity = new Vector2 (controller.velocity.x, 0);
								controller.AddForce (new Vector2 (0, jumpPower));
                                anim.setJump();
						}
		}
		anim.setMoving (isMoving);
		if (isIdle) {
			anim.setMoving (false);
		}
		anim.setIdle (isIdle);

	}

	// Set up aircontrols animations
	void AirControls()
	{
		anim.setAirDash (isAirDashing);

		if (isFalling && canJump)
		{
			doubleJump = true;
		}

		if (!input.right()&& !input.left())
		{
			anim.setDirectionX((int)Direction.NONE);
		}
		// AIR - DASHING
		if (isAirDashing) {
			

			isGravity = false;
			isIdle = false;
			isMoving = true;
			moveDirection.y = (int)Direction.NONE;


			if(canAirDashRight  && dashDirection == Direction.RIGHT)
			{
				airDashCounter = airDashDuration;
				moveDirection.x = (int)Direction.RIGHT;
				canAirDashRight = false;
			}
			if(canAirDashLeft && dashDirection == Direction.LEFT)
			{
				airDashCounter = airDashDuration;
				moveDirection.x = (int)Direction.LEFT;
				canAirDashLeft = false;
			}

			if(dashDirection == Direction.RIGHT) 
			{
				anim.setDirectionX((int)Direction.RIGHT);
				moveDirection.x = (int)Direction.RIGHT;
				if(input.left()){
					moveDirection.x = (int)Direction.NONE;
					isAirDashing = false;
				}
			}
			if(dashDirection == Direction.LEFT){
				anim.setDirectionX((int)Direction.LEFT);
				moveDirection.x = (int)Direction.LEFT;
				if(input.right()){
					moveDirection.x = (int)Direction.NONE;
					isAirDashing = false;
				}
			}
            if (input.up() && doubleJump && !isDoubleJumping && canJump)
            {
				isAirDashing = false;
				doubleJump = false;
                isDoubleJumping = true;
				controller.velocity = new Vector2(controller.velocity.x,0);
				controller.AddForce(new Vector2(0,jumpPower));
				anim.setJump ();
				if(dashDirection == Direction.LEFT) moveDirection.x = (int)Direction.LEFT;
				else if(dashDirection == Direction.RIGHT) moveDirection.x = (int)Direction.RIGHT;
				else moveDirection.x = (int)Direction.NONE;
			}
			

		} 
		// AIR - NORMAL
		else 
		{

			if (input.right ()) {
				isIdle = false;
				isMoving = true;
				moveDirection.x = (int)Direction.RIGHT;
				anim.setDirectionX (moveDirection.x);
			}
			if (input.left ()) {
				isIdle = false;
				isMoving = true;
				moveDirection.x = (int)Direction.LEFT;
				anim.setDirectionX (moveDirection.x);
			}

			if (input.up () && doubleJump && !isDoubleJumping && canJump) {
				isIdle = false;
                isMoving = false;
                doubleJump = false;
                isDoubleJumping = true;
				controller.velocity = new Vector2(controller.velocity.x,0);
				controller.AddForce(new Vector2(0,jumpPower));
				if(input.left()) moveDirection.x = (int)Direction.LEFT;
				else if(input.right()) moveDirection.x = (int)Direction.RIGHT;
				else moveDirection.x = (int)Direction.NONE;
				anim.setJump ();
			}

			if (isFalling) {
					isIdle = false;
					anim.setFall (isFalling);
			}

		}
		anim.setMoving (isMoving);
	}

	void OnTouched(int damageType)
	{/*
		if (damageType == (int)Direction.HIGH) 
		{

		}
		if (damageType == (int)Direction.MIDDLE) 
		{
		
		}
		if (damageType == (int)Direction.LOW) 
		{

		}*/
	}

	void OnCollisionEnter2D(Collision2D collision) {
		
		/*if (collision.collider.tag == "Ground")
		{
				//Debug.Log ("ground collision");
				isGrounded = true;
		}*/

		/*
		CharacterController enemyController = hit.collider as CharacterController;
		if (enemyController != null) {
				Vector3 hitDirection = Vector3.zero;
				hitDirection.x = hit.moveDirection.x;
				hitDirection.y = hit.moveDirection.y;
				hitDirection.z = hit.moveDirection.z;

				Debug.Log ("hitDirection.x = " + hitDirection.x);
				Debug.Log ("hitDirection.y = " + hitDirection.y);
				if (isRunning) {
						enemyController.SimpleMove (hitDirection * runSpeed);
				} else {
						enemyController.SimpleMove (hitDirection * speed);
				}
				Debug.Log ("collision");
		}*/
	}
	void OnCollisionExit2D(Collision2D collision)
	{
		/*if (collision.collider.tag == "Ground")
		{
			//Debug.Log ("ground collision");
			isGrounded = false;
		}*/
	}

}
