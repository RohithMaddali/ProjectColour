using System.Collections;
using System.Collections.Generic;
using AJ;
using UnityEngine;

namespace AJ
{
    public abstract class FilteredFlockBehaviour : FlockBehaviour
    {
        public ContextFilter filter;
    }
}