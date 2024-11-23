using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Observer _observer;
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private int _initCubeCount;
    [SerializeField] private float _radius = 2f;

    private int divider = 2;
    
    public event Action<Vector3, float> Exploding;

    private void Start()
    {
        _cubePrefab.transform.position = transform.position;
        CreateCubes(_cubePrefab, _initCubeCount);
    }
    
    private void OnEnable()
    {
        _observer.ClickedCube += OnClick;
    }

    private void OnDisable()
    {
        _observer.ClickedCube -= OnClick;
    }

    private void OnClick(Cube cube)
    {
        if (cube.TrySplit())
            CreateCubes(cube, GetRandomCount(),true);
        else
            Exploding?.Invoke(cube.transform.position, cube.transform.localScale.y);

        Destroy(cube.gameObject);
    }

    private void CreateCubes(Cube cube, int count,bool isChangingParamCube = false)
    {
        int chance = cube.ChanceSplit;
        Vector3 scale = cube.transform.localScale;
       
        if (isChangingParamCube)
        {
            chance = cube.ChanceSplit / divider;
            scale = cube.transform.localScale / divider;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 position = cube.transform.position + Random.onUnitSphere * _radius;
            position.y = Mathf.Abs(position.y);
            
            Cube newCube = Instantiate(cube, position, Quaternion.identity);
            newCube.Initialize(chance, scale);
        }
    }

    private int GetRandomCount()
    {
        int minValue = 2;
        int maxValue = 6;

        return Random.Range(minValue, maxValue + 1);
    }
}