using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class GameSceneInfo : Node
{

	static GameSceneInfo gameInfoRef = null;
	public static GameSceneInfo GameInfoInstance => gameInfoRef;
	GameSceneInfo()
	{
		gameInfoRef = this;
	}
	[Export]
	AnimationPlayer animationPlayerRef = null;


	public override void _Ready()
	{
	}


	SaveCube currentSaveCube = null;
	Action callback_setSavedFalse = null;

	public delegate void OnSaveCubeChangedDelegate(SaveCube saveCube);
	public event OnSaveCubeChangedDelegate OnSaveCubeChangedEvent;

	public void SetCurrentSaveCube(SaveCube saveCube, Action callback_setSavedFalse)
	{
		if (this.callback_setSavedFalse != null)
		{
			this.callback_setSavedFalse.Invoke();
		}
		this.callback_setSavedFalse = callback_setSavedFalse;

		currentSaveCube = saveCube;

		OnSaveCubeChangedEvent.Invoke(saveCube);
	}
	public SaveCube CurrentSaveCube
	{
		get => currentSaveCube;
	}


	[Export]
	double timeOutOfAreaLimit = 0.5;
	double timeCounter_OutArea = 0;


	public double TimeCounterPercent { get => timeCounter_OutArea / timeOutOfAreaLimit; }


	Area3D enterNewArea = null;
	public void SetEnterNewArea(Area3D newArea)
	{
		enterNewArea = newArea;
	}
	public void ExitedArea(Area3D area3D)
	{
		if (area3D == enterNewArea)
		{
			enterNewArea = null;
		}
	}

	public override void _Process(double delta)
	{
		if (enterNewArea == null && !animationPlayerRef.IsPlaying())
		{
			timeCounter_OutArea += delta;
		}
		else
		{
			timeCounter_OutArea -= delta;
			timeCounter_OutArea = Mathf.Max(0, timeCounter_OutArea);
		}
		if (timeCounter_OutArea > timeOutOfAreaLimit)
		{
			animationPlayerRef.Play("RestartGame");
		}
	}

	public void CallPlayerRestart()
	{
		timeCounter_OutArea = 0;
		PlayerRoot.PlayerRootRef.Restart();
	}


}
