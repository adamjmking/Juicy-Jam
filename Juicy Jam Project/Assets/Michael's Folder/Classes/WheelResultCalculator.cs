using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelResultCalculator<T>
{
    public T getResult(T[] optionList, float spinResult)
    {
        if (spinResult < 0)
        {
            spinResult = 360 + spinResult;
        }

        float sectionLength = 360 / optionList.Length;
        int indexOfOption = (int)(spinResult / sectionLength);
        return optionList[indexOfOption];

    }

}
