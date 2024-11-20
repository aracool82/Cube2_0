using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(Exploder))]
public class Cube : MonoBehaviour
{
    private Exploder _exploder;

    public event Action<Cube> OnSplited;

    public int ChanceSplit { get; private set; } = 100;
    public Rigidbody Rigidbody { get; private set; }

    public void Initialize(int chance, Vector3 scale)
    {
        ChanceSplit = chance;
        transform.localScale = scale;

        _exploder = GetComponent<Exploder>();
        Rigidbody = GetComponent<Rigidbody>();

        ChangeColor();
    }

    private void ChangeColor()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    private void OnMouseDown()
    {
        if (TrySplit())
        {
            OnSplited?.Invoke(this);
        }
        else
        {
            _exploder.Explode(transform.position, transform.localScale.y);
            Destroy(gameObject);
        }
    }

    private bool TrySplit()
    {
        return GetChance() <= ChanceSplit ? true : false;
    }

    private int GetChance()
    {
        int multiplier = 100;

        return (int)(Random.value * multiplier);
    }
}