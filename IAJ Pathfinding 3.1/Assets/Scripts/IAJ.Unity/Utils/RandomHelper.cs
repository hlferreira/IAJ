<<<<<<< HEAD
﻿using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Utils
{
    public static class RandomHelper
    {
        public static float RandomBinomial(float max)
        {
            return Random.Range(0, max) - Random.Range(0, max);
        }

        public static float RandomBinomial()
        {
            return Random.Range(0, 1.0f) - Random.Range(0, 1.0f);
        }
    }
}
=======
﻿using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Utils
{
    public static class RandomHelper
    {
        public static float RandomBinomial(float max)
        {
            return Random.Range(0, max) - Random.Range(0, max);
        }

        public static float RandomBinomial()
        {
            return Random.Range(0, 1.0f) - Random.Range(0, 1.0f);
        }
    }
}
>>>>>>> 32342d1e6b1bad9cac424f01f79a5163fc7d6324
