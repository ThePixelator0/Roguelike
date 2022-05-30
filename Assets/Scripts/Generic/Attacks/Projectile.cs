using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Projectile : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public GameObject gameObject;

    [HideInInspector] public List<string> ignoredTags = new List<string> {
                                                                            "SpawnPoint",
                                                                            "Untagged",
                                                                            "Projectile"
                                                                        };
    
    [Space]
    public float moveSpeed;
    public float damage;
    public float knockbackStrength;
    [HideInInspector] public int rotationOffset = 90;

    public bool charged = false;

    [Space]
    public float scale = 1;

    [Header("Box Collider")] [Space]
    public Vector2 offset;
    public Vector2 size;
}
