using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private Renderer _renderer;

    public int ChanceSplit { get; private set; } = 100;
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(int chance, Vector3 scale)
    {
        ChanceSplit = chance;
        transform.localScale = scale;
        ChangeColor();
    }

    public bool TrySplit()
    {
        return GetChance() <= ChanceSplit;
    }

    private void ChangeColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    private int GetChance()
    {
        int multiplier = 100;

        return (int)(Random.value * multiplier);
    }
}