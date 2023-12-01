using Cities;
using System;
using System.Collections;
using Constants;
using UnityEngine;
using System.Linq;
using Assets.Scripts;

namespace Scripts
{
    public class Unit : MonoBehaviour, IDamager, IDamagable
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private GameObject attackRangeColliderObject;

        private CityViewPresenter targetCity;
        private Color color;

        private UnitType type;

        private float price;
        private float visionRadius;

        private float damage;
        private float attackSpeed;
        private float attackRange;

        private float health;
        private float currentHealth;
        private float healthRegenPerSecond;

        private float speed;

        public OwnerType owner;
        public float Strenght { get; private set; }

        public bool HasHealth => this.health > 0;

        public event Action<OwnerType> OnOutOfHealth;

        public void GiveDamage(IDamagable damageReciever, OwnerType owner, float amount)
        {
            StartCoroutine(DamageGiveCoroutine());

            IEnumerator DamageGiveCoroutine()
            {
                while (damageReciever.HasHealth)
                {
                    damageReciever.TakeDamage(owner, this.damage);
                    Debug.Log($"Damage given: {damage}, To {owner}");
                    yield return new WaitForSeconds(attackSpeed);
                }
            }
        }

        public void TakeDamage(OwnerType owner, float amount)
        {
            this.health -= amount;
            if(this.health <= 0) 
            {
                OnOutOfHealth?.Invoke(owner);
            }

            StartCoroutine(colorChangeRoutine());

            IEnumerator colorChangeRoutine()
            {
                var oldColor = this.color;
                this.SetColor(Color.red);
                yield return new WaitForSeconds(attackSpeed / 2);
                this.SetColor(oldColor);
            }
        }

        private IEnumerator MoveToTarget(Vector2 target)
        {
            while (true)
            {
                var distance = Vector2.Distance(this.transform.position, target);
                if (distance <= this.attackRange)
                {
                    Debug.Log("Stopped Moving");
                    break;
                }

                Debug.Log("Moving to target");
                this.transform.position = Vector2.MoveTowards(this.transform.position, target, this.speed * Time.deltaTime);
                yield return null;
            }
        }

        private void MoveToTargetUnit(IDamagable damageReciever)
        {
            var target = damageReciever as Unit;
            StartCoroutine(MoveToTarget(target.transform.position));
        }

        private void OnAttackRangeEnterColliderEnter(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("City"))
            {
                var city = collision.gameObject.GetComponent<CityViewPresenter>();
                if (city.Owner == targetCity.Owner)
                {
                    this.AttackTarget(city, this.owner, this.damage);
                }
            }
            
            if (collision.gameObject.CompareTag("Unit"))
            {
                var unit = collision.gameObject.GetComponent<Unit>();
                if (unit.owner != this.owner)
                {
                    this.AttackTarget(unit, this.owner, this.damage);
                }
            }
        }

        private void OnTriggerEnter(UnityEngine.Collider collision)
        {
            if (collision.gameObject.CompareTag("Unit"))
            {
                var unit = collision.gameObject.GetComponent<Unit>();
                if (unit.owner != this.owner)
                {
                    this.MoveToTargetUnit(unit);
                }
            }
        }

        private void AttackTarget(IDamagable damageReciever, OwnerType owner, float damage)
        {
            //stop moving when target in range
            StopCoroutine(moveCoroutine);

            TaskRoutine t = new TaskRoutine(attackCoroutine());
            t.Finished += (bool attacking) =>
            {
                if (!attacking)
                {
                    moveCoroutine = StartCoroutine(MoveToTarget(targetCity.transform.position + Helpers.RandomVector(-2, 2)));
                }
            };

            IEnumerator attackCoroutine()
            {
                while (damageReciever.HasHealth)
                {
                    damageReciever.TakeDamage(owner, damage);
                    Debug.Log($"Damage given: {damage}, To {owner}");
                    yield return new WaitForSeconds(attackSpeed);
                }
            }
        }

        public void SetColor(Color newColor)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = newColor;
        }

        Coroutine moveCoroutine;

        public void Init(CityViewPresenter target)
        {
            var targetRandmizedPosition = target.transform.position + Helpers.RandomVector(-5, 5);
            moveCoroutine = StartCoroutine(MoveToTarget(targetRandmizedPosition));
            this.targetCity = target;
        }

        public void SetConfig(UnitScriptableObject config)
        {
            this.price = config.Price;
            this.visionRadius = config.VisionRadius;

            this.health = config.HealthPoints;
            this.currentHealth = config.HealthPoints;
            this.healthRegenPerSecond = config.HealthRegenPerSecond;

            this.damage = config.Damage;
            this.attackSpeed = config.AttackSpeed;
            this.attackRange = config.AttackRange;

            this.speed = config.Speed;

            this.icon = config.Icon;

            this.type = config.Type;

            var collider = this.gameObject.GetComponent<CircleCollider2D>();
            var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            var attackRangeColliderScript = this.attackRangeColliderObject.GetComponent<AttackRangeColliderScript>();
            var attackRangeCollider = this.attackRangeColliderObject.GetComponent<CircleCollider2D>();

            collider.radius = this.visionRadius;
            spriteRenderer.sprite = this.icon;
            attackRangeCollider.radius = this.attackRange;
            attackRangeColliderScript.ColliderEnter += OnAttackRangeEnterColliderEnter;
        }

    }
}
