namespace Dtwo.API
{
    public class Random
    {
        private static System.Random m_random = new System.Random();
        private static object m_lock = new object();

        public static int Range(int a, int b)
        {
            lock(m_lock)
            {
                if (a > b)
                {
                    LogManager.LogWarning("Utilisation d'une valeur aléatoire avec a > b");
                    int oldA = a;
                    a = Math.Min(a, b);
                    b = Math.Max(oldA, b);
                }

                return m_random.Next(a, b);
            }
        }
    }
}
