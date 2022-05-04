using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gistogram : MonoBehaviour
{

    private int[] _spreading = new int[18];
    private Image[] _columns = new Image[18]; 
    private int _angles = 0;

    [SerializeField] private float _frameWidth;
    [SerializeField] private float _frameHeight;
    [SerializeField] private float _columnWidth;
    [SerializeField] private float _columnPartHeight;
    [SerializeField] private float _columnMinHeight;
    [SerializeField] private float _shiftX, _shiftY;
    [SerializeField] private float _spacing;
    [SerializeField] private GameObject _columnObject;
    [SerializeField] private Image _panel;

    private void Start()
    {
        for (int i = 0; i < 18; i++)
        {
            _spreading[i] = 0;
            
        }
        GenerateColumns();
    }

    public void AddAngle(float angle)
    {
        for (float i = 0f; i < 180f; i += 10f)
        {
            if (angle > i && angle <= i + 10f)
            {
                _spreading[Mathf.FloorToInt(i / 10f)]++;
                Recalculate(Mathf.FloorToInt(i / 10f));
            }
        }
        _angles++;
    }

    private void Recalculate(int angle)
    {
        float height = _columnPartHeight * Mathf.Log10(_spreading[angle] + 1);
        _columns[angle].rectTransform.localScale = new Vector3(_columnWidth, height + _columnMinHeight, 1);
    }

    private void GenerateColumns()
    {
        for (int i = 0; i < 18; i++)
        {
            _columns[i] = Instantiate(_columnObject, _panel.transform).GetComponent<Image>();
            _columns[i].rectTransform.localScale = new Vector3(_columnWidth, _columnMinHeight, 1);
            _columns[i].rectTransform.localPosition = new Vector3((_columnWidth * i + _spacing * (i + 1)) - (_frameWidth / 2) + _shiftX, _spacing - (_frameHeight / 2) + _shiftY);
        }
    }

    


}
