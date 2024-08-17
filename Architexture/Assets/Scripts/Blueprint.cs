using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Blueprint", menuName = "Blueprint")]
public class Blueprint : ScriptableObject
{
    public new string name;

    public Sprite icon;
}
