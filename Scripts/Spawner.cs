using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MouseInput))]
[RequireComponent(typeof(Exploder))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _initCubeCount;
    [SerializeField] private float _radius = 2f;

    private MouseInput _mouseInput;
    private int divider = 2;

    public event Action<Vector3, float> Exploding; 
    
    private void Awake()
    {
        _mouseInput = GetComponent<MouseInput>();
    }

    private void OnEnable()
    {
        _mouseInput.DetectedCube += OnCubeHandler;
    }

    private void OnDisable()
    {
        _mouseInput.DetectedCube -= OnCubeHandler;
    }

    private void Start()
    {
        _cubePrefab.transform.position = transform.position;
        int initChance = 100;

        for (int i = 0; i < _initCubeCount; i++)
            CreateNewCube(_cubePrefab, initChance, Vector3.one);
    }

    private void OnCubeHandler(Cube cube)
    {
        if (cube.TrySplit())
            CreateCubes(cube);
        else
            Exploding?.Invoke(cube.transform.position, cube.transform.localScale.y);

        Destroy(cube.gameObject);
    }

    private void CreateCubes(Cube cube)
    {
        int chance = cube.ChanceSplit / divider;
        Vector3 scale = cube.transform.localScale / divider;

        for (int i = 0; i < GetRandomCount(); i++)
            CreateNewCube(cube, chance, scale);
    }

    private void CreateNewCube(Cube cube, int chance, Vector3 scale)
    {
        Vector3 position = cube.transform.position + Random.onUnitSphere * _radius;
        position.y = Mathf.Abs(position.y);

        Cube newCube = Instantiate(cube, position, Random.rotation);
        newCube.Initialize(chance, scale);
    }

    private int GetRandomCount()
    {
        int minValue = 2;
        int maxValue = 6;

        return Random.Range(minValue, maxValue + 1);
    }
}