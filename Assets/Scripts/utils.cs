using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class utils
{
    public static bool randomBoolean()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }
}
