using Godot;
using System;

public class ColorPickerButton4 : ColorPickerButton
{
	public override void _Ready()
	{
		this.Color = Colors.Gray;
		_on_ColorPickerButton4_color_changed(this.Color);
	}
	private void _on_ColorPickerButton4_color_changed(Color color)
	{
		HankinsOptions.colors[0] = color;
	}
}


