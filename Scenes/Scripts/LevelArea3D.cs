using Godot;
using System;
using System.Collections.Generic;

public partial class LevelArea3D : Area3D
{
	bool isBodyIn = false;

	List<LevelAreaMesh> levelAreaMeshes = new List<LevelAreaMesh>();


	public override void _Ready()
	{
		var children = Units.AllChildren(this);
		foreach (Node node in children)
		{
			if (node is LevelAreaMesh)
			{
				levelAreaMeshes.Add((LevelAreaMesh)node);
			}
		}



		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;


	}

	private void OnBodyExited(Node3D body)
	{
		Character_Controller character_Controller = body as Character_Controller;
		if (character_Controller == null)
		{
			return;
		}
		isBodyIn = false;
		GameSceneInfo.GameInfoInstance.ExitedArea(this);
	}


	private void OnBodyEntered(Node3D body)
	{
		Character_Controller character_Controller = body as Character_Controller;
		if (character_Controller == null)
		{
			return;
		}
		isBodyIn = true;
		GameSceneInfo.GameInfoInstance.SetEnterNewArea(this);
	}

	public override void _Process(double delta)
	{
		{
			foreach (LevelAreaMesh levelAreaMeshRef in levelAreaMeshes)
				levelAreaMeshRef.LerpColor((float)GameSceneInfo.GameInfoInstance.TimeCounterPercent);
		}
	}
}
