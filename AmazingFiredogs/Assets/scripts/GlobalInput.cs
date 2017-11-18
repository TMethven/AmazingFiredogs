using UnityEngine;

public class GlobalInput
{
    public static string[] Vertical = { "Keyboard0Vertical", "Keyboard1Vertical", "Joy0Vertical", "Joy1Vertical", "Joy2Vertical", "Joy3Vertical" };
    public static string[] Horizontal = { "Keyboard0Horizontal", "Keyboard1Horizontal", "Joy0Horizontal", "Joy1Horizontal", "Joy2Horizontal", "Joy3Horizontal" };
    public static string[] Jets = { "Keyboard0Jets", "Keyboard1Jets", "Joy0Jets", "Joy1Jets", "Joy2Jets", "Joy3Jets" };
    public static string[] Fire = { "Keyboard0Fire", "Keyboard1Fire", "Joy0Fire", "Joy1Fire", "Joy2Fire", "Joy3Fire" };
    public static string[] Tether = { "Keyboard0Tether", "Keyboard1Tether", "Joy0Tether", "Joy1Tether", "Joy2Tether", "Joy3Tether" };
    public static string[] Shield = { "Keyboard0Shield", "Keyboard1Shield", "Joy0Shield", "Joy1Shield", "Joy2Shield", "Joy3Shield" };
    public static string[] Pause = { "Keyboard0Pause", "Keyboard1Pause", "Joy0Pause", "Joy1Pause", "Joy2Pause", "Joy3Pause" };
    public static string[] Back = { "Keyboard0Back", "Keyboard1Back", "Joy0Back", "Joy1Back", "Joy2Back", "Joy3Back" };

    public static int Player1Controller = -1;
    public static int Player2Controller = -1;

	public static DogType Player1DogType = DogType.None;
    public static DogType Player2DogType = DogType.None;

    public enum DogType { None, Corgi, Husky, GermanShep, Lab };

	public static Color[] DogColours = new Color[]{new Color(1, 1, 1), new Color(0xF3 / 255.0f, 0xA8 / 255.0f, 0x5D / 255.0f), new Color(0x8E / 255.0f, 0x92 / 255.0f, 0x9B / 255.0f), new Color(1, 1, 1), new Color(1, 1, 1)};

	public static string GameMode = "TwoBuildings";
}
