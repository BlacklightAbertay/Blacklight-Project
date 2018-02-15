using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;

public class Movement : AttachToController
{
	Vector2 selectorPosition;

	public float force = 5.0f;

	public Rigidbody rb;

	protected override void OnAttachToController()
    {
        // Subscribe to input now that we're parented under the controller
        InteractionManager.InteractionSourceUpdated += InteractionSourceUpdated;
    }

    protected override void OnDetachFromController()
    {
		// Unsubscribe from input now that we've detached from the controller
		InteractionManager.InteractionSourceUpdated -= InteractionSourceUpdated;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMove();
	}

	public void PlayerMove()
	{
		Vector3 moveDirection = new Vector3(selectorPosition.x, 0.0f, selectorPosition.y);
		moveDirection = moveDirection.normalized;
		float moveAmount = selectorPosition.magnitude;
		moveDirection = transform.rotation * moveDirection;
		//Debug.Log("Hand rotation: " + transform.rotation);

		if (moveAmount > 0.3)
		{
			rb.velocity = moveDirection * force * moveAmount * Time.deltaTime;
			//rb.AddForce(handRotation * transform.forward * moveAmount * 10.0f);
		}
		else
		{
			rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}

	}

	private void InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        if (obj.state.source.handedness == handedness)
        {
            selectorPosition = obj.state.thumbstickPosition;
        }
    }
}
