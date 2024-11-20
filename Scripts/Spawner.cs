using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _initCubeCount;
    [SerializeField] private float _radius = 2f;
    
    private int divider = 2;
    
    private void Start()
    {
        _cubePrefab.transform.position = transform.position;
        int initChance = 100;
        
        for (int i = 0; i < _initCubeCount; i++)
            CreateNewCube(_cubePrefab,initChance,Vector3.one);
    }
    
    public void CreateCubes(Cube cube)
    {
        int chance = cube.ChanceSplit / divider;
        Vector3 scale = cube.transform.localScale / divider;
        
        for (int i = 0; i < GetRandomCount(); i++)
            CreateNewCube(cube, chance, scale);
    }

    private void CreateNewCube(Cube cube,int chance,Vector3 scale)
    {
        Vector3 position = cube.transform.position + Random.onUnitSphere * _radius;
        position.y = Mathf.Abs(position.y);
        
        Cube newCube = Instantiate(cube, position, Random.rotation);
        newCube.Initialize(chance,scale, this);
    }

    private int GetRandomCount()
    {
        int minValue = 2;
        int maxValue = 6;
        
        return Random.Range(minValue, maxValue + 1);
    }
}