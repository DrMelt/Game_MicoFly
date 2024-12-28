using Godot;
using System;

public partial class PlayerRoot : Node3D
{
	[ExportGroup("Refs")]
	[Export]
	Character_Controller body3D = null;

	static PlayerRoot playerRootRef = null;
	public static PlayerRoot PlayerRootRef => playerRootRef;
	PlayerRoot()
	{
		playerRootRef = this;
	}


	public override void _Process(double delta)
	{
		GlobalPosition = body3D.GlobalPosition;
		body3D.Position = Vector3.Zero;

		if (Input.IsActionJustReleased("ReStart"))
		{
			Restart();
		}
	}

	public void Restart()
	{
		body3D.GlobalPosition = GameSceneInfo.GameInfoInstance.CurrentSaveCube.RestartPoint;

		body3D.Velocity = Vector3.Zero;
	}
}
