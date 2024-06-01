using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	private Vector2 direction;
	private float speed = 1000;
	
	public void SetDirection(Vector2 dir)
	{
		direction = dir.Normalized();
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Velocity += direction * speed * (float)delta;
		MoveAndSlide();
	}
}
