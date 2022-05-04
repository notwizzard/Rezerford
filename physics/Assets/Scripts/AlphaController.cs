using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaController : MonoBehaviour
{
    [Header("Phisics")]
    [SerializeField] private float _startDefaultVelocity;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _mass;
    [SerializeField] private float _aplhaQ;
    [SerializeField] private float _aurumQ;
    [SerializeField] private float _k;
    [SerializeField] private float _maxForceRadius;
    [SerializeField] private float _minForceRadius;

    [Header("Color")]
    [SerializeField] private Color _colorTo;
    private Color _colorFrom;
    [SerializeField] private float _minAngle, _maxAngle;
    [SerializeField] private float _secondAtomZ;

    private Gistogram _gistogram;

    private float _startVelocity;
    private float _velocityRate = 1f;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private bool _underForce = false;
    private Vector3 _forcePosition = Vector3.zero;

    private void Start()
    {

        if (PlayerPrefs.HasKey("EnegryRate"))
            _velocityRate = PlayerPrefs.GetFloat("EnergyRate");
        _startVelocity = _startDefaultVelocity * _velocityRate;

        _meshRenderer = GetComponent<MeshRenderer>();
        _colorFrom = _meshRenderer.material.color;
        _meshRenderer.material.EnableKeyword("_EMISSION");
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = _direction * _startVelocity;
        _gistogram = GameObject.FindGameObjectWithTag("Gistogram").GetComponent<Gistogram>();
    }

    private void Update()
    {
        if (!_underForce)
            return;

        CountDelta();
    }

    private void CountDelta()
    {
        Vector3 forceDirection = (transform.position - _forcePosition);
        float force = _k * _aplhaQ * _aurumQ / Mathf.Clamp(Mathf.Pow(forceDirection.magnitude, 2), _minForceRadius, _maxForceRadius);
        float acceleration = force / _mass;
        float deltaVelocity = acceleration * Time.deltaTime;
        Vector3 delta = forceDirection.normalized * deltaVelocity;
        _rigidbody.velocity += delta;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Aurum"))
        {
            _forcePosition = other.transform.position;
            _underForce = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Aurum"))
        {
            _underForce = false;
            float angle = Vector3.Angle(_direction, _rigidbody.velocity);
            
            if (transform.position.z >= _secondAtomZ)
            {
                _gistogram.AddAngle(angle);
            }

            float t = (Mathf.Clamp(angle, _minAngle, _maxAngle) - _minAngle) / (_maxAngle - _minAngle);
            Color newColor = Color.Lerp(_colorFrom, _colorTo, t);
            _meshRenderer.material.SetColor("_Color", newColor);
            _meshRenderer.material.SetColor("_EmissionColor", newColor);
        }
    }
}
