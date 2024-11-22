using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class Exploder : MonoBehaviour
{
    private Spawner _spawner;
    private float _baseRadius = 5f;
    private float _baseForce = 500f;
    private float _baseScaleY = 1f;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
    }

    private void OnEnable()
    {
        _spawner.Exploding += Explode;
    }

    private void OnDisable()
    {
        _spawner.Exploding -= Explode;
    }

    private void Explode(Vector3 position, float scaleY)
    {
        float coefficent = CalculateCoefficent(scaleY);
        float radius =  coefficent * _baseRadius;
        Collider[] allColliders = Physics.OverlapSphere(position, radius);
       
        foreach (var collider in allColliders)
        {
            if (collider.TryGetComponent(out Cube cube))
            {
                float force = CalculateForce(position, cube.transform.position) * coefficent;
                cube.Rigidbody.AddExplosionForce(force, position , radius);
            }
        }
    }

    private float CalculateCoefficent(float scaleY)
    {
        int multiply = 2;
        return (_baseScaleY - scaleY) * multiply;
    }

    private float CalculateForce(Vector3 startPosition,Vector3 endPosition)
    {
        float distance = Vector3.Distance(startPosition, endPosition);
        
        if(distance < 1)
            distance = 1;
        
        return _baseForce / distance;
    }
}