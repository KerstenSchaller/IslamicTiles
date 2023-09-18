using Godot;
using System;


using System.Collections.Generic; //for list

public class GraphNode : Node2D
{

	int id;

	public int Id{get{return id;} set{id = value;}}
	List<int> linkedNodeIds = new List<int>();

	public Vector2 pos;

	public List<int> getLinkedNodeIds()
	{
		return linkedNodeIds;
	}

	public Vector2 getPosition(){return this.pos;}
	
	public IShape shape;
	public void setShape(IShape _shape){shape = _shape;}

	public override void _Ready()
	{

	}

	public override void _Process(float delta)
	{
		Update();
	}

	public void setPosition(Vector2 position)
	{
		this.pos = position;
	}

	public void addLink(int linkId)
	{
		linkedNodeIds.Add(linkId);
	}

	public void clearNeighbours()
	{
		linkedNodeIds.Clear();
	}

	public override void _Draw()
	{
		
		var font = new DynamicFont();
		font.FontData = ResourceLoader.Load<DynamicFontData>("res://VelomiaVanora.ttf");
		font.Size = 20;
		
		//DrawString(font, 3*this.pos + new Vector2(200,200), id.ToString(), Colors.Black);
		
		
		
	}


}
