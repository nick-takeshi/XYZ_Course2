using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    [SerializeField] private LayerCheck _vision;
    [SerializeField] private LayerCheck _canAttack;

    [SerializeField] private float _alarmDelay;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _missDelay;
    [SerializeField] private Cooldown _exclDelay;
    [SerializeField] private Cooldown _misDelay;
 
    private Coroutine _current;
    private GameObject _target;
    private Creature _creature;
    private Animator _animator;
    private bool _isDead;
    private Patrol _patrol;
    private Collider2D _collider2D;
    private Vector3 _direction;

    

    protected static readonly int IsDead = Animator.StringToHash("IsDead");

    private SpawnListComponent _particles;

    private void Awake()
    {
        _particles = GetComponent<SpawnListComponent>();
        _creature = GetComponent<Creature>();
        _animator = GetComponent<Animator>();
        _patrol = GetComponent<Patrol>();
        

    }
    private void Start()
    {
        StartState(_patrol.DoPatrol());
    }

    public void OnHeroInVision(GameObject go)
    {
        if (_isDead) return;
       
        _target = go;

        StartCoroutine(AgroToHero());
    }

    public void LookAtHero()
    {
        var direction = GetDirectionToTarget();
        _creature.SetDirection(Vector2.zero);
        _creature.UpdateSpriteDir(direction);
    }

    private IEnumerator AgroToHero()
    {
        LookAtHero();

        if (_exclDelay.IsReady) _particles.Spawn("Exclamation"); _exclDelay.Reset();
        
        yield return new WaitForSeconds(_alarmDelay);
        StartState(GoToHero());
    }
    private IEnumerator GoToHero()
    {
        
        while (_vision._isTouchingLayer)
        {
            if (_canAttack._isTouchingLayer)
            {
                StartState(Attack());
            }
            else
            {
                SetDirectionToTarget();
            }
            
            yield return null;
        }

        _creature.SetDirection(Vector2.zero);
        if (_misDelay.IsReady) _particles.Spawn("Miss"); _misDelay.Reset();
        yield return new WaitForSeconds(_missDelay);
        StartState(_patrol.DoPatrol());
    }

    private IEnumerator Attack()
    {
        while (_canAttack._isTouchingLayer)
        {
            _creature.Attack();
            yield return new WaitForSeconds(_attackDelay);
        }

        StartState(GoToHero());
    }

    private void SetDirectionToTarget()
    {
        
        var direction = GetDirectionToTarget();
        _creature.SetDirection(direction);
            
    }

    private Vector2 GetDirectionToTarget()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        return direction.normalized;
    }

    private void StartState(IEnumerator coroutine)
    {
        _creature.SetDirection(Vector2.zero);

        if (_current != null)
        {
            StopCoroutine(_current);
        }
        _current = StartCoroutine(coroutine);
    }

    public void OnDie()
    {
        _isDead = true;
        _animator.SetBool("IsDead", true);

        StopAllCoroutines();
        _creature.SetDirection(Vector2.zero);

        var coll = GetComponent<CapsuleCollider2D>();
        coll.size = new Vector2(0.44f, 0.44f);

        var rigid = GetComponent<Rigidbody2D>();


        gameObject.layer = 7;

    }

}
