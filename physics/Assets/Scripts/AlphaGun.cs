using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaGun : MonoBehaviour
{
    [SerializeField] private GameObject _aplhaParticle;

    [SerializeField] private float _delayDefault;
    [SerializeField] private float _radiusDefault;
    [SerializeField] private Transform _muzzle;

    private Text _velocityText;
    [SerializeField] private float _minVelocityRate, _maxVelocityRate;
    private float _velocityRate = 1f;

    private float _delay;
    private float _radius;

    private void Start()
    {
        _velocityText = GameObject.FindGameObjectWithTag("Rate").GetComponent<Text>();

        if (PlayerPrefs.HasKey("EnegryRate"))
            _velocityRate = PlayerPrefs.GetFloat("EnergyRate");
       
        _delay = _delayDefault;
        _radius = _radiusDefault;
        StartCoroutine(FireCourutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _velocityRate *= 1.25f;
            _velocityRate = Mathf.Clamp(_velocityRate, _minVelocityRate, _maxVelocityRate);
            _velocityText.text = "Energy rate x" + _velocityRate.ToString("0.00");
            PlayerPrefs.SetFloat("EnergyRate", _velocityRate);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _velocityRate /= 1.25f;
            _velocityRate = Mathf.Clamp(_velocityRate, _minVelocityRate, _maxVelocityRate);
            _velocityText.text = "Energy rate x" + _velocityRate.ToString("0.00");
            PlayerPrefs.SetFloat("EnergyRate", _velocityRate);
        }
    }

    private IEnumerator FireCourutine()
    {
        yield return new WaitForSeconds(_delay);
        Fire();
        StartCoroutine(FireCourutine());
    }

    private void Fire()
    {
        float angel = Random.Range(0f, Mathf.PI * 2);
        float r = Random.Range(0f, _radius);
        Vector3 v = new Vector3(r * Mathf.Cos(angel), r * Mathf.Sin(angel), 0f);
        Instantiate(_aplhaParticle, _muzzle.position + v, _muzzle.rotation);
    }
}
