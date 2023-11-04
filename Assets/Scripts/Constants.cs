using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public static class Constants
    {
        //Constants go here
    }

    public enum UnitType
    {
        Infantry = 1,
        Tank = 2,
        Artillery = 3
    }

    public enum OwnerType
    {
        Neutral = 0,
        Enemy = 1,
        Ally = 2,
    }
}