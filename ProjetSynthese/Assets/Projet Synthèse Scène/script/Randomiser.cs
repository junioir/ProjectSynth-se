using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Randomiser : MonoBehaviour
{

    [SerializeField] private GameObject _interactiveElement;
    [SerializeField] private GameObject _melemEnemyGenerator;
    [SerializeField] private GameObject _RangeGenerator;

    private List<Vector3> _allPosition = new List<Vector3>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            _allPosition.Add(child.position);
        }

        int RandomIndex = Random.Range(0, _allPosition.Count);
        Vector3 RandomPos = _allPosition[RandomIndex];
        Instantiate(_melemEnemyGenerator, RandomPos, Quaternion.identity, transform);
        _allPosition.RemoveAt(RandomIndex);

        RandomIndex = Random.Range(0, _allPosition.Count);
        RandomPos = _allPosition[RandomIndex];
        Instantiate(_RangeGenerator, RandomPos, Quaternion.identity, transform);
        _allPosition.RemoveAt(RandomIndex);

        foreach (Vector3 position in _allPosition)
        {
            Instantiate(_interactiveElement, position, Quaternion.identity, transform);

        }
    }
}
