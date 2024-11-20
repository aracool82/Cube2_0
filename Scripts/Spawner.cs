using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _initCubeCount;
    [SerializeField] private float _radius = 2f;

    private void Start()
    {
        CreateCubes(_initCubeCount,_cubePrefab,transform.position,Vector3.one);
    }

    private void CreateCubes(int cubeCount,Cube cube,Vector3 position,Vector3 scale, int chance = 100)
    {
        for (int i = 0; i < cubeCount; i++)
            CreateCube(cube, chance, scale, transform.position);
    }

    private void CreateCube(Cube cube, int chance, Vector3 scale, Vector3 position)
    {
        position += Random.onUnitSphere * _radius;
        position.y = Mathf.Abs(position.y);
        
        Cube newCube = Instantiate(cube, position, Random.rotation);
        newCube.Initialize(chance, scale);

        newCube.OnSplited += OnChangeParameters;
    }

    private void OnChangeParameters(Cube cube)
    {
        int divider = 2;

        int chance = cube.ChanceSplit / divider;
        Vector3 scale = cube.transform.localScale / divider;

        int _maxCount = 6;
        int _minCount = 2;
        int count = Random.Range(_minCount, _maxCount + 1);
        
        CreateCubes(count,cube, cube.transform.position,scale, chance);
    }
}