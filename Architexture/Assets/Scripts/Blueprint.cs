using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public enum BlueprintType
{
    Bridge,
    Ladder,
    Stairs,
    Launchpad
};

[CreateAssetMenu(fileName = "New Blueprint", menuName = "Blueprint")]
public class Blueprint : ScriptableObject
{
    public GameObject prefab;

    public new string name;

    public Sprite icon;

    public int amount;

    public BlueprintType type;

    public GameObject placedObjectPrefab;

    public Vector2 placeDistance;
}
