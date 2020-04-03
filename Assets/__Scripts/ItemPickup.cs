﻿using UnityEngine;

public class ItemPickup : Inter
{

	public Item item;   // Item to put in the inventory on pickup
	public int xpPoints = 1;
	public int hpPoints = 5;

	// When the player interacts with the item
	public override void Interact()
	{
		base.Interact();

		PickUp();   // Pick it up!
	}

	// Pick up the item
	void PickUp()
	{
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp = Inventory.instance.Add(item);    // Add to inventory

		// If successfully picked up
		if (wasPickedUp)
			Destroy(gameObject);    // Destroy item from scene
	}

}