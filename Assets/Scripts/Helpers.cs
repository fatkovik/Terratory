using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class Helpers
    {
        public static Vector3 RandomVector(float min, float max)
        {
            return new Vector3
            (
                UnityEngine.Random.Range(min, max),
                UnityEngine.Random.Range(min, max),
                UnityEngine.Random.Range(min, max)
            );
        }
    }
}
