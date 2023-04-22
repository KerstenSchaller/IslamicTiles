using Godot;
using System;


using System.Collections.Generic; //for list

public class GraphNode : Node2D
{

    int id;

    public int Id{get{return id;} set{id = value;}}
    List<int> linkedNodeIds = new List<int>();

    Vector2 position;

    public List<int> getLinkedNodeIds()
    {
        return linkedNodeIds;
    }

    //public Vector2 Position{get{return this.position;}}
     
    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        Update();
    }

    public void setPosition(Vector2 position)
    {
        this.position = position;
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

        string text = "";
        foreach(var id in linkedNodeIds)
        {
            text = text + " " + id;
        }
        if((this.Id == 2 || this.Id == 7 || this.Id == 9 || this.Id == 17 || this.Id == 1 || this.Id == 6 || this.Id == 11 || this.Id == 16))
        {
            DrawString(font, 3*this.position + new Vector2(200,200),text, Colors.White);
            DrawString(font, 3*this.position + new Vector2(200,210),this.Id.ToString(), Colors.Red);
        }
    }


}
