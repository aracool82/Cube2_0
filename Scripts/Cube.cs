using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(Exploder))]
public class Cube : MonoBehaviour
{
    private Exploder _exploder;
    private Spawner _spawner;

    public int ChanceSplit { get; private set; } = 100;
    public Rigidbody Rigidbody { get; private set; }

    private void OnMouseDown()
    {
        if (TrySplit())
            _spawner.CreateCubes(this);
        else
            _exploder.Explode(transform.position, transform.localScale.y);
            
        Destroy(gameObject);
    }
    
    public void Initialize(int chance, Vector3 scale, Spawner spawner)
    {
        ChanceSplit = chance;
        transform.localScale = scale;
        
        _spawner = spawner;
        _exploder = GetComponent<Exploder>();
        Rigidbody = GetComponent<Rigidbody>();

        ChangeColor();
    }

    private void ChangeColor()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }


    private bool TrySplit()
    {
        return GetChance() <= ChanceSplit;
    }

    private int GetChance()
    {
        int multiplier = 100;

        return (int)(Random.value * multiplier);
    }
}