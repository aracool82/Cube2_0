using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Ray _ray;
    
    public event Action<Cube> DetectedCube;
    
    private void Update()
    {
        int leftMouseButton = 0;

        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit))
                if (hit.collider.TryGetComponent(out Cube cube))
                    DetectedCube?.Invoke(cube);
        }
    }
}