namespace Assets.Scripts
{
    public static class Const
    {
        public enum CustomObjects
        {
            Entity,
            Well,
            Spawn,
            Woodcutter
        }

        public enum Gender
        {
            Male,
            Female
        }

        public enum Parents
        {
            Entities,
            Buildings
        }

        public const int WaterDecreaseChance = 400;

        public static string[] MaleNames;
        public static string[] FemaleNames;
        public static int GameSpeed = 1;
    }
}