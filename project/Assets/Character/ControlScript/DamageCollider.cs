using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class DamageCollider : MonoBehaviour {

	private int type;

	void Start()
	{
		
		type = -1;
		/*if (this.gameObject.name == "High")
			type = Global.HIGH;
		if (this.gameObject.name == "Medium")
			type = Global.MIDDLE;
		if (this.gameObject.name == "Low")
			type = Global.LOW;
        */
	}

	void OnTriggerEnter(Collider collider) {
		if(gameObject.transform.root != collider.gameObject.transform.root && !collider.CompareTag("Ground") && !collider.isTrigger)
		{
			Debug.Log ("collision!");
			collider.gameObject.SendMessageUpwards("OnTouched",type);
		}
	}

	/*
	void OnTriggerExit(Collider collider) {
		if(gameObject.transform.root != collider.gameObject.transform.root && !collider.CompareTag("Ground") && !collider.isTrigger)
		{
			collider.gameObject.SendMessageUpwards("OnTouchedExit",type);
		}
	}*/
}
