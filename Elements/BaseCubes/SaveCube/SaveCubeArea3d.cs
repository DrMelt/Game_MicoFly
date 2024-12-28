using Godot;
using System;


public partial class SaveCubeArea3d : Area3D
{
	[Export]
	SaveCube saveCubeRef = null;
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
		saveCubeRef.CallOnSave();
	}

}

