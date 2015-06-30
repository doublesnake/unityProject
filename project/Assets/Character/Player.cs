using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;
	public float speed = 4f;
	public float gravity = 15.0f;
	public float jumpPower = 5.0f;
	public bool facingRight = true;
	public bool canJump = false;
	public bool canHitAgain = true;
	public bool isFalling = false;
	public bool isIdle = true;
	public Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponentInChildren<Animator>();
		controller = gameObject.GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		CheckStates ();
		SetAnimations ();
		moveDirection.x = Input.GetAxisRaw("Horizontal");
		controller.Move (moveDirection *speed* Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;
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

	}

	void SetAnimations()
	{
		isIdle = true;
		anim.SetBool ("walk_forward", false);
		anim.SetBool ("walk_backward", false);
		anim.SetBool ("idle",false);

		
		
		if (Input.GetKey (KeyCode.D)) {
			isIdle = false;
			anim.SetTrigger("punch");
				} else {

						if (Input.GetKey ("right")) {
								isIdle = false;
								if (controller.isGrounded)
										anim.SetBool ("walk_forward", true);
						} else if (Input.GetKey ("left")) {
								isIdle = false;
								if (controller.isGrounded)
										anim.SetBool ("walk_backward", true);

						}

			
						if (Input.GetKey ("up") && canJump) {
								isIdle = false;
								//anim.SetInteger ("AnimPar", 2);
								moveDirection.y = Input.GetAxisRaw ("Vertical") * jumpPower;
						} else if (isFalling) {
								//anim.SetInteger ("AnimPar", 3);
						}

				}
		
		if (isIdle) 
		{
			if(controller.isGrounded)
				anim.SetBool ("idle",true);
		}
	}

	void Flip()
		        {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
			transform.localScale = theScale;
		}
}
