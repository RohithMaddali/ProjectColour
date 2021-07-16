using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Events : MonoBehaviour
{
    public delegate void MachineryMove();
    public static event MachineryMove MachineryMoveEvent;
    public static void MachineryMoveFunction()
    {
        if (MachineryMoveEvent != null)
        {
            MachineryMoveEvent();
        }
    }
    public delegate void ConveyerBelt();
    public static event ConveyerBelt ConveyerBeltEvent;
    public static void ConveyerBeltFunction()
    {
        if (ConveyerBeltEvent != null)
        {
            ConveyerBeltEvent();
        }
    }
}
