using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Brick : PoolableMono,IDamageable
{
    private static int _maxHp = 10;
    public UnityEvent OnBrickBreak;
    private Renderer _renderer;


    [SerializeField] private int _hp;

    public int HP
    {
        get => _hp;
        set
        {
            _hp = value;
            _hpText.SetText(value.ToString());

            if (_hp <= 0)
            {
                OnBrickBreak?.Invoke();
            }
        }
    }
    private TMP_Text _hpText;
    private float _hpTime;

    public override void Setting()
    {
        _hpTime = 0f;
        _hpText = transform.Find("HPText").GetComponent<TMP_Text>();
        _renderer = transform.Find("Renderer").GetComponent<Renderer>();
        //HP = Random.Range(1,_maxHp);
        HP = _hp;
        OnBrickBreak.AddListener(() => PoolManager.SInstance.Push(this));
    }

    private void Update()
    {
        if(Time.time < _hpTime)
        {
            _renderer.material.color = Color.red;
        }
        else
        {
            _renderer.material.color = Color.white;
        }
    }
    public override void Push()
    {
        
    }

    public override void Pop()
    {
            
    }
    public void Damaged()
    {
        HP--;
        _hpTime = Time.time + 0.1f;
    }
}
