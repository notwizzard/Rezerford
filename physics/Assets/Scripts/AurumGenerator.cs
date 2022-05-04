using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AurumGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _aurumParticle;
    [SerializeField] private float _plateSize;
    [SerializeField] private float _atomRadius;
    [SerializeField] private float _nucleusRadius;

    private void Start()
    {
        for (float i = transform.position.x -_plateSize; i <= transform.position.y + _plateSize; i += _atomRadius)
        {
            for (float j = transform.position.x - _plateSize; j <= transform.position.x + _plateSize; j += _atomRadius)
            {
                Instantiate(_aurumParticle, new Vector3(i, j, transform.position.z), Quaternion.identity);
            }
        }

        for (float i = transform.position.x - _plateSize + (_atomRadius / 2); i <= transform.position.y + _plateSize - (_atomRadius / 2); i += _atomRadius)
        {
            for (float j = transform.position.x - _plateSize + (_atomRadius / 2); j <= transform.position.x + _plateSize - (_atomRadius / 2); j += _atomRadius)
            {
                Instantiate(_aurumParticle, new Vector3(i, j, transform.position.z + _atomRadius), Quaternion.identity);
            }
        }
    }

}
