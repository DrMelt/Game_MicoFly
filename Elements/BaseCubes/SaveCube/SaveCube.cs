using Godot;
using System;


public partial class SaveCube : MeshInstance3D
{
	[Export]
	Node3D restartPointRef = null;

	public Vector3 RestartPoint => restartPointRef.GlobalPosition;

	[Export]
	Node3D saveTargetRef = null;
	[Export]
	Godot.Collections.Array<SaveCube> nextFromCubes = new Godot.Collections.Array<SaveCube>();


	StandardMaterial3D savedEmitterMaterial = null;
	[Export]
	Color saveEmitterColor = new Color(0.5f, 0.5f, 0.5f);
	[Export]
	Color noSaveEmitterColor = new Color(0.1f, 0.1f, 0.1f);

	public override void _Ready()
	{
		GameSceneInfo.GameInfoInstance.OnSaveCubeChangedEvent += OnSaveCubeChanged;

		savedEmitterMaterial = GetSurfaceOverrideMaterial(1) as StandardMaterial3D;


		
		SetEmitionSaved(_isSaved);
	}

	void SetEmitionSaved(bool isSaved)
	{
		if (isSaved)
		{
			savedEmitterMaterial.Emission = saveEmitterColor;
		}
		else
		{
			savedEmitterMaterial.Emission = noSaveEmitterColor;
		}
	}

	void OnSaveCubeChanged(SaveCube saveCube)
	{
		if (nextFromCubes.Contains(saveCube))
		{
			saveTargetRef.Visible = true;
		}
		else
		{
			saveTargetRef.Visible = false;
		}
	}

	bool _isSaved = false;
	public bool IsSaved { get; }

	void _SetSavedFalse_Callback()
	{
		_isSaved = false;
		SetEmitionSaved(_isSaved);
	}

	internal void CallOnSave()
	{
		if (_isSaved == false)
		{
			_isSaved = true;
			GameSceneInfo.GameInfoInstance.SetCurrentSaveCube(this, _SetSavedFalse_Callback);
			SetEmitionSaved(_isSaved);
		}
	}


}

