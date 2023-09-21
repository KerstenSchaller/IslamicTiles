using Godot;
using System;

public class PatternOptionButton : OptionButton
{
	public override void _Ready()
	{
		// populate enum
		foreach (int i in Enum.GetValues(typeof(HankinsOptions.Shapes)))
		{
			AddItem(Enum.GetName(typeof(HankinsOptions.Shapes), i));
		}
		OptionButton node = GetNode<OptionButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer6/OptionButton");
		node.Select((int)HankinsOptions.getHankinsOptions().shape);
	}


	private void _on_OptionButton_item_selected(int index)
	{
		HankinsOptions.getHankinsOptions().shape =(HankinsOptions.Shapes)index;
		GD.Print("index: " + index);
		HankinsOptions.getHankinsOptions().resetRequest = true;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}



