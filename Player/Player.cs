using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public const float SPEED = 300.0f;
	[Export] public PackedScene bulletScene;
	[Export] public float shootRange = 300.0f; 
	[Export] public float shootInterval = 0.5f;

	private void _on_ready()
	{
		bulletScene = (PackedScene)ResourceLoader.Load("res://Player/bullet.tscn");
	}	
	public override void _PhysicsProcess(double delta){
		var direction = Vector2.Zero;
		
		if (Input.IsActionPressed("move_right")){
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left")){
			direction.X -= 1.0f;
		}
		if (Input.IsActionPressed("move_up")){
			direction.Y -= 1.0f;
		}
		if (Input.IsActionPressed("move_down")){
			direction.Y += 1.0f;
		}

		Velocity = direction * SPEED;
		MoveAndSlide();
	}

	private void OnShootTimeout()
	{
		Node2D closestEnemy = GetClosestEnemy();
		if (closestEnemy != null){
			ShootAt(closestEnemy);
		}
	}

	private Node2D GetClosestEnemy()
	{
		Node2D closestEnemy = null;
		float closestDistance = shootRange;

		foreach (Node2D enemy in GetTree().GetNodesInGroup("enemies"))
		{
			float distance = GlobalPosition.DistanceTo(enemy.GlobalPosition);
			if (distance < closestDistance)
			{
				closestEnemy = enemy;
				closestDistance = distance;
			}
		}
		return closestEnemy;
	}

	private void ShootAt(Node2D enemy)
	{
		if (bulletScene != null)
		{
			Bullet bullet = (Bullet)bulletScene.Instantiate();
			GetParent().AddChild(bullet);
			bullet.GlobalPosition = GlobalPosition;
			Vector2 direction = enemy.GlobalPosition - GlobalPosition;
			bullet.SetDirection(direction);
		}
		//else{
			//GD.Print("bulletScene is null");
		//}
	}
}



