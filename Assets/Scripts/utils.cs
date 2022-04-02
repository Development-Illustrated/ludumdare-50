using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utils
{
    public bool randomBoolean()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }
}
