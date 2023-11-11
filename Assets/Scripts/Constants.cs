using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class Constants
    {
        //Constants go here
    }

    public static class ExceptionMessages
    {
        public const string UnitTypeNotSpecifiedError = "Unit type not specified";
        public const string NullException = "Neccesary property is Null";
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