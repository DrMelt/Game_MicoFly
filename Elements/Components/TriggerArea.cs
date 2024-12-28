using Godot;
using Godot.Collections;
using System;

public partial class TriggerArea : Area3D
{
	[Export]
	Array<TriggerMaskArea> triggerMaskAreas = new Array<TriggerMaskArea>();

	bool isTriggered = false;
	public bool IsTriggered => isTriggered;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node3D body)
	{
		Character_Controller character_Controller = body as Character_Controller;
		if (character_Controller == null)
		{
			return;
		}
	}

	public override void _Process(double delta)
	{
	}


}
