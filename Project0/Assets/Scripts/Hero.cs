using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class Hero : Creature
{
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private LayerMask _interactionLayer;
    

    [SerializeField] private AnimatorController _armed;
    [SerializeField] private AnimatorController _disarmed;
    [SerializeField] private Cooldown _throwCooldown;
    [SerializeField] private Cooldown _meleeCooldown;
    [SerializeField] private SpawnComponent _throwSpawner;

    [SerializeField] private CheckCircleOverlap _interactionCheck;
    [SerializeField] private SpawnComponent _timer;
    private bool _allowDoubleJump;
    private bool _getDoubleJump;
    private bool _isDodge;
    private Rigidbody2D rigid;
    private CapsuleCollider2D coll;
    private const string SwordId = "Sword";


    public GameSession _session;

    private int CoinCount => _session.Data.Inventory.Count("Coin");
    private int SwordCount => _session.Data.Inventory.Count(SwordId);
    private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;
    private bool CanThrow
    {
        get
        {
            if (SelectedItemId == SwordId) 
                return SwordCount > 1;
            
            var def = DefsFacade.I.Items.Get(SelectedItemId);
            return def.HasTag(ItemTag.Throwable);
        }
    }


    protected override void Awake()
    {
        base.Awake();
        _sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        
    }
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _session.Data.Inventory.OnChanged += OnInventoryChanged;
        UpdateHeroWeapon();
        gameObject.transform.localScale = Vector3.one;
    }

    private void OnInventoryChanged(string id, int value)
    {
        if (id == SwordId) UpdateHeroWeapon();
        
    }

    private void OnDestroy()
    {
        _session.Data.Inventory.OnChanged -= OnInventoryChanged;

    }
    protected override void Update()
    {
        base.Update();

        if (rigid.velocity.x > 0 | rigid.velocity.y > 0)
        {
            coll.size = new Vector2(0.66f, 0.8f);
            _animator.SetBool("isDodge", false);
        }
    }
    protected override float CalculateYVelocity()
    {
        var isJump = _direction.y > 0;
        var Yvelocity = _rigidbody.velocity.y;

        if (_getDoubleJump)
        {
            if (_isGrounded) _allowDoubleJump = true;
        }

        return base.CalculateYVelocity();
    }
    protected override float CalculateJumpVelocity(float Yvelocity)
    {

        if (_isGrounded)
        {
            _sounds.Play("Jump");

            _JumpParticles.Spawn();
            _allowDoubleJump = false;
            return _jumpSpeed;
        }

        return base.CalculateJumpVelocity(Yvelocity);
    }

    public void AddInInventory(string id, int value)
    {
        _session.Data.Inventory.Add(id, value);

        if (id == "Sword")
        {
            _sounds.Play("Sword");
        }
    }

    public override void TakeDamage()
    {
        base.TakeDamage();


    }
    public void Interact()
    {
        _interactionCheck.Check();

    }
    public void GetPower()
    {
        _getDoubleJump = true;
        Invoke("LosePower", 5);
    }
    private void LosePower()
    {
        _getDoubleJump = false;
    }
    public void SpawnFootDust()
    {
        _particles.Spawn("Run");
    }
    public void SpawnLandingDust()
    {
        _particles.Spawn("Landing");
    }
    public void SpawnAttackPartickle()
    {
        _particles.Spawn("Attack");
    }
    public override void Attack()
    {
        if (SwordCount <= 0) return;
        if (_meleeCooldown.IsReady)
        {
            base.Attack();
            _meleeCooldown.Reset();
        }
        
    }
    public void UpdateHeroWeapon()
    {
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        Debug.Log(_animator.runtimeAnimatorController);
    }
    public void NextItem()
    {
        _session.QuickInventory.SetNextItem();
    }
    public void OnHealthChanged(int currentHealth)
    {
        _session.Data.Hp.Value = currentHealth;
    }
    public void OnDoThrow()
    {
        //_particles.Spawn("Throw");
        var throwableId = _session.QuickInventory.SelectedItem.Id;
        var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
        _throwSpawner.SetPrefab(throwableDef.Projectile);

        _throwSpawner.Spawn();
        _session.Data.Inventory.Remove(throwableId, 1);
    }
    public void Throw()
    {
        if (_throwCooldown.IsReady & CanThrow)
        {
            _animator.SetTrigger("Throw");
            _throwCooldown.Reset();
            _sounds.Play("Range");
        }
    }
    public void Dodge()
    {
        coll.size = new Vector2(0.7f, 0.25f);
        _animator.SetBool("isDodge", true);
    }

    //public void Heal(string id)
    //{
    //    if (_session.Data.Inventory.Count(id) > 0 & _session.QuickInventory.SelectedItem.Id == "BigHealPotion")
    //    {
    //        _session.Data.Inventory.Remove(id, 1);
    //        Healing(8);
    //    }
    //    else if (_session.Data.Inventory.Count(id) > 0 & _session.QuickInventory.SelectedItem.Id == "HealPotion")
    //    {
    //        _session.Data.Inventory.Remove(id, 1);
    //        Healing(5);
    //    }
    //    else return;
    //}
    //public void Healing(int value)
    //{
    //    var healthComponent = GetComponent<HealthComponent>();
    //    if (healthComponent != null)
    //    {
    //        healthComponent.HealHP(value);
    //        GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>()._session.Data.Hp.Value += value;
    //        
    //    }
    //}
    
    //public void OnUsePotion()
    //{
    //    var usableId = _session.QuickInventory.SelectedItem.Id;

    //    if (usableId == "BigHealPotion")
    //    {
    //        Heal(usableId);
    //    }
    //    else if (usableId == "HealPotion")
    //    {
    //        Heal(usableId);
    //    }
    //    else if(usableId == "SpeedPotion")
    //    {
    //        IncreaseSpeed();
    //    }
    //}
    //public void IncreaseSpeed()
    //{
    //    if (_session.Data.Inventory.Count("SpeedPotion") > 0)
    //    {
    //        _session.Data.Inventory.Remove("SpeedPotion", 1);

    //        speed += 2;
    //        Invoke("DecreaseSpeed", 5);
    //        _timer.SpawnTimer();

    //    }
    //}
    //public void DecreaseSpeed()
    //{
    //    speed -= 2;
    //}

    public void UseInventory()
    {
        if (IsSelectedItem(ItemTag.Throwable))
        {
            Throw();
        }
        else if (IsSelectedItem(ItemTag.Potion))
        {
            UsePotion();
        }
    }
    private bool IsSelectedItem(ItemTag tag)
    {
        return _session.QuickInventory.SelectedDef.HasTag(tag);
    }
    public void UsePotion()
    {
        var potion = DefsFacade.I.Potions.Get(SelectedItemId);

        switch (potion.Effect)
        {
            case Effect.AddHp:
                _session.Data.Hp.Value += (int)potion.Value;
                _sounds.Play("Heal");
                break;
                    case Effect.SpeedUp:
                        _speedUpCooldown.Value = _speedUpCooldown.TimeLasts + potion.Time;
                        _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                        _speedUpCooldown.Reset();
                break;
            default:
                break;
        }

        _session.Data.Inventory.Remove(potion.Id, 1);
    }
    private readonly Cooldown _speedUpCooldown = new Cooldown();
    private float _additionalSpeed;

    protected override float CaplculateSpeed()
    {
        if (_speedUpCooldown.IsReady)
            _additionalSpeed = 0f;

        return base.CaplculateSpeed() + _additionalSpeed;
    }
}
