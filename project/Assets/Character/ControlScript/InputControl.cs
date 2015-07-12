//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;


namespace AssemblyCSharp
{
	/* Class to manage inputs and mapping */
	public class InputControl
	{
		// Double Tap variables
		public float tapCooldown = 0.3f;
		public int tapCount = 0;
		public float runPressTime = 0.3f;
		public float lastTapTime = 0;
		public bool doubleTap = false;
		public bool doubleTapDown = false;
		public KeyCode lastKeyDown = KeyCode.None;
		public float lastDashTime = 0f;
		
		
		// Keycode Mapping variables
		public KeyCode left;
		public KeyCode up;
		public KeyCode right;
		public KeyCode down;
		public KeyCode punch;
		
		// State variables
		private bool canMove;
		private bool doubleTapLeft;
		private bool doubleTapRight;
		private bool doubleTapDownLeft;
		private bool doubleTapDownRight;
		public KeyCode doubleTapDirection;
		
		// Constructor
		public InputControl()
		{
			/*left = KeyCode.A;
			up = KeyCode.W;
			right = KeyCode.D;
			punch = KeyCode.Keypad1;*/

			left = KeyCode.Q;
			up = KeyCode.Z;
			right = KeyCode.D;
			down = KeyCode.S;
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

		public bool isCrouching()
		{
			if (Input.GetKey (down)) 
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
		public bool isPunching()
		{
			if (Input.GetKeyDown (punch))
				return true;
			return false;
		}
		
		public void checkInputStates()
		{

			if (doubleTap) {
				if(Input.GetKey(lastKeyDown))
				{
					doubleTapDown = true;
					doubleTap = false;
					Debug.Log("RUN");
				}
				else
				{
					doubleTap = false;
				}
				return;
			}

			if (doubleTapDown) {
				if(!Input.GetKey(lastKeyDown))
				{
					doubleTapDown = false;
				}
				return;
			}

			if (Input.anyKeyDown) {
				KeyCode currentKey = getCurrentKey ();
				if (tapCount == 1 && currentKey == lastKeyDown) {
					
					if (Time.time - lastTapTime < tapCooldown) {
						doubleTapDirection = lastKeyDown;
						Debug.Log("DASH");
						doubleTap = true;
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

