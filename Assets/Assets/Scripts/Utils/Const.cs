namespace Assets.Scripts
{
    public static class Const
    {
        public enum Buildings
        {
            Well
        }

        public enum Gender
        {
            Male,
            Female
        }

        public const string EntityParent = "Entities";
        public const string BuildingsParent = "Buildings";

        public const int WaterDecreaseChance = 400;
        public static int GameSpeed = 1;

        public static string[] MaleNames;
        public static string[] FemaleNames;
    }
}