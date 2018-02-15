using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;

public class Inventory : AttachToController {

	public GameObject[] inventoryItems;
	public GameObject currentItem;

	int selectedItem = 0;
	float angle;

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
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
	{
		if (obj.state.source.handedness == handedness)
		{
			if (obj.state.touchpadTouched)
			{
				//Make objects appear depending of the amount of items we may want to use
				//Make the touchpad position offset so top item is in middle
				Vector2 from = new Vector2(0.0f, 1.0f);
				angle = Vector2.SignedAngle(from, obj.state.touchpadPosition);
				if (angle < 0.0f)
					angle = -angle;
				else
					angle = 180.0f + (180.0f - angle);
				ObjectSelect();
				//Debug.Log("Angle: " + angle);
			}
		}
	}


	private void ObjectSelect()
	{
		float divisions = 360.0f / inventoryItems.Length;

		// Offset angle
		angle -= divisions / 2.0f;
		if (angle < 0.0f)
		{
			angle = 360.0f + angle;
		}

		float selectedDivisionF = angle / divisions;
		int selectedDivision = Mathf.FloorToInt(selectedDivisionF);
		Debug.Log(selectedDivision);

		if (selectedDivision != selectedItem)
		{
			// Switch to new item
			Destroy(currentItem);
			selectedItem = selectedDivision;
			currentItem = Instantiate(inventoryItems[selectedDivision]);
		}
	}
}
