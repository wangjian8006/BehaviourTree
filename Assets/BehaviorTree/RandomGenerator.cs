using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTFrame
{
    internal class RandomGenerator
    {
        private static RandomGenerator s_Instance = null;

        private uint m_seed;

        public static RandomGenerator Instance
        {
            get
            {
                if (s_Instance == null) s_Instance = new RandomGenerator(0);
                return s_Instance;
            }
        }

        //[0, 1)
        public float GetRandom()
        {
            m_seed = 214013 * m_seed + 2531011;
            float r = (m_seed * (1.0f / 4294967296.0f));
            return r;
        }

        //[low, high)
        public float InRange(float low, float high)
        {
            float r = this.GetRandom();
            float ret = r * (high - low) + low;
            return ret;
        }

        public void SetSeed(uint seed)
        {
            this.m_seed = seed;
        }

        protected RandomGenerator(uint seed)
        {
            m_seed = seed;
        }
    };
}
