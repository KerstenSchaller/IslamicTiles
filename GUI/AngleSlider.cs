using Godot;
using System;

public class AngleSlider : HSlider
{
	private void _on_HSlider_value_changed(float value)
	{
		GD.Print("Angle changed: " + value);
		HankinsOptions.AngleGrad = value;
	}
}

