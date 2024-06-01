using Godot;
using System;

public partial class world : Node2D
{
	[Export] public PackedScene mobScene;
	private Camera2D camera;
	private Vector2 screenSize;
	
	public override void _Ready()
	{
		mobScene = (PackedScene)ResourceLoader.Load("res://mob.tscn");
		
		camera = GetNode<Camera2D>($"Player/Camera2D");
		screenSize = GetViewportRect().Size;
	}
	
	private void SpawnMobs(int count)
	{
		for (int i = 0; i < count; i++)
		{
			CharacterBody2D mob = (CharacterBody2D)mobScene.Instantiate();
			Vector2 spawnPosition = GetRandomSpawnPositionOutsideCamera();
			AddChild(mob);
			mob.GlobalPosition = spawnPosition;
		}
	}
	private Vector2 GetRandomSpawnPositionOutsideCamera()
	{
		Vector2 cameraPosition = camera.GlobalPosition;

		float spawnMargin = 100f;

		RandomNumberGenerator rng = new RandomNumberGenerator();
		rng.Randomize();
		int edge = rng.RandiRange(0, 3);

		float x, y;
		switch (edge)
		{
			case 0: // Top edge
				x = rng.RandfRange(cameraPosition.X - screenSize.X / 2 - spawnMargin, cameraPosition.X + screenSize.X / 2 + spawnMargin);
				y = cameraPosition.Y - screenSize.Y / 2 - spawnMargin;
				break;
			case 1: // Bottom edge
				x = rng.RandfRange(cameraPosition.X - screenSize.X / 2 - spawnMargin, cameraPosition.X + screenSize.X / 2 + spawnMargin);
				y = cameraPosition.Y + screenSize.Y / 2 + spawnMargin;
				break;
			case 2: // Left edge
				x = cameraPosition.X - screenSize.X / 2 - spawnMargin;
				y = rng.RandfRange(cameraPosition.Y - screenSize.Y / 2 - spawnMargin, cameraPosition.Y + screenSize.Y / 2 + spawnMargin);
				break;
			case 3: // Right edge
				x = cameraPosition.X + screenSize.X / 2 + spawnMargin;
				y = rng.RandfRange(cameraPosition.Y - screenSize.Y / 2 - spawnMargin, cameraPosition.Y + screenSize.Y / 2 + spawnMargin);
				break;
			default:
				x = 0;
				y = 0;
				break;
		}
		return new Vector2(x, y);
	}
	private void _OnMobSpawnTimeout()
	{
		SpawnMobs(1);
	}
}



