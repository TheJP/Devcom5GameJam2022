public enum LightKind
{
    Red = 1,
    Green = 2,
    Blue = 4,
}

public enum LightColour
{
    Black = 0,
    Red = 1,
    Green = 2,
    Blue = 4,
    Yellow = Red | Green,
    Pink = Red | Blue,
    Teal = Green | Blue,
    White = Red | Green | Blue,
}

public static class LightExtensions
{
    //public static bool IsVisible(LightColour light, LightColour objectColour) => light == objectColour;
}
