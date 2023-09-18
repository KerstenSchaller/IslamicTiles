using Godot;
using System;

public class PatternOptionButton : OptionButton
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// populate enum
		foreach (int i in Enum.GetValues(typeof(HankinsOptions.Shapes)))
		{
			AddItem(Enum.GetName(typeof(HankinsOptions.Shapes), i));
		}
		
	}

	private void _on_OptionButton_item_selected(int index)
	{
		HankinsOptions.shape =(HankinsOptions.Shapes)index;
		GD.Print("index: " + index);
		HankinsOptions.resetRequest = true;
	}
}



