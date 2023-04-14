namespace DefaultNamespace
{
    public class Golds
    {
        private static int golds;
        private static int maxGolds;

        public static int GetGolds() => golds;

        public void AddGolds(int count) => golds += count;

        public void PayGolds(int count) => golds -= count;

        public bool TryPayGolds(int count) => golds - count >= 0;
    }
}
