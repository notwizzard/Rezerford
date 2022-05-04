using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyYourself : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
