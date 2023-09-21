using Godot;
using System;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class HankinsOptions
{

	private HankinsOptions() { }

	static HankinsOptions hankinsOptions;

	public static HankinsOptions getHankinsOptions()
	{
		if (hankinsOptions == null)
		{
			hankinsOptions = new HankinsOptions();
			hankinsOptions.DeSerializeNow();
			return hankinsOptions;
		}
		else
		{
			return hankinsOptions;
		}
	}

	public double angleFromBase = Math.PI / 4;

	public double AngleGrad
	{
		get { return angleFromBase * 180 / Math.PI; }
		set { angleFromBase = value * Math.PI / 180; }
	}


	public float offset;


	public enum Shapes
	{
		Hexagon,
		Square,
		Triangle,
		MultiTile
	}

	public bool resetRequest = false;

	public float ShapesSideLength = -1;

	public Shapes shape = Shapes.Hexagon;

	public Color[] colors = new Color[4] { Colors.Red, Colors.Green, Colors.Black, Colors.Blue };
	[Export]
	public Color[] _Colors
	{
		get { return colors; }
		set { colors = (Color[])value; }
	}

	public bool showPoly = true;
	public bool showFrame = true;
	public bool printSingleTileOnly = false;

	public void SerializeNow()
	{
		HankinsOptions c = hankinsOptions;
		IFormatter formatter = new BinaryFormatter();
    	System.IO.Stream stream = new FileStream(@"options.txt",FileMode.Create, FileAccess.Write);

    	formatter.Serialize(stream, c);
    	stream.Close();
	}

	public void DeSerializeNow()
	{
		IFormatter formatter = new BinaryFormatter();
		if(System.IO.File.Exists(@"options.txt"))
		{
			System.IO.Stream stream = new FileStream(@"options.txt",FileMode.Open,FileAccess.Read);
			hankinsOptions = (HankinsOptions)formatter.Deserialize(stream);
			stream.Close();
		}
	}

}
