using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTurnsHint : MonoBehaviour
{
    public enum PassType { LeftUp, RightUp, LeftBottom, RightBottom };
    public List<PassType> availablePasses;
}
