namespace Assets.Scripts
{
    public static class Const
    {
        public enum CustomObjects
        {
            Entity,
            Well,
            Spawn,
            Woodcutter,
            House,
            Church
        }

        public enum Gender
        {
            Male,
            Female
        }

        public enum Items
        {
            Wood,
            Stone
        }

        public enum Parents
        {
            Entities,
            Buildings
        }

        public const int WaterDecreaseChance = 400;
        public const int SleepDecreaseChance = 600;

        public static string[] MaleNames;
        public static string[] FemaleNames;
        public static int GameSpeed = 1;
    }
}