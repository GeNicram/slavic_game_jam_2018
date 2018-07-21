using UnityEngine;
using UnityEditor;

public class Common : MonoBehaviour
{
    public Sprite[] _dishSprites;

    static Common common;
    private void Awake()
    {
        common = this;
    }

    public static int dishTypeCount
    {
        get { return dishSprites.Length; }
    }

    public static Sprite[] dishSprites
    {
        get { return common._dishSprites; }
    }
}