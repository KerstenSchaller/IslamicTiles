using Godot;
using System;

public class main : Node2D
{

	public override void _Ready()
	{
		Tesselator tesselator = new Tesselator();
		AddChild(tesselator);
		tesselator.init();
	}

	public override void _Process(float delta)
	{
		Update();
	}

	public override void _Draw()
	{
		Vector2 po = new Vector2(50,50);
		Vector2 pe = new Vector2(100,100);



		Vector2 p1 = new Vector2(500,500);
		DrawCircle(p1,5, Colors.Red );
		float angleGrad = 280f;
		float angleRad = angleGrad * (float)Math.PI/180f;
		Vector2 p2 = p1 + 200*new Vector2((float)Math.Cos(angleRad),(float)Math.Sin(angleRad));
		DrawLine(p1, p2, Colors.Green, 4);

		var ac = Math.Atan2((p1.x-p2.x), (p1.y-p2.y));
		//GD.Print("angle: " + ac*(180/Math.PI));
	}


}


