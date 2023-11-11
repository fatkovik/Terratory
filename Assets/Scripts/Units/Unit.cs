using Cities;
using System;
using System.Collections;
using Constants;
using UnityEngine;

namespace Scripts
{
    public class Unit : MonoBehaviour, IDamager, IDamagable
    {
        [SerializeField]
        private Sprite icon;

        private UnitType type;

        private float price;
        private float visionRadius;

        private float damage;
        private float attackSpeed;
        private float attackRange;

        private float health;
        private float healthRegenPerSecond;

        private float speed;

        public OwnerType owner;

        //Testing
        private Color color;

        public float Strenght { get; private set; }

        public event Action<OwnerType> OnOutOfHealth;

        public void GiveDamage(IDamagable damageReciever, OwnerType owner, float amount)
        {
            var reciever = damageReciever as Unit;

            StartCoroutine(DamageGiveCoroutine());

            IEnumerator DamageGiveCoroutine()
            {
                while (reciever.health > 0)
                {
                    reciever.TakeDamage(owner, this.damage);
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

        private void SetColor(Color newColor)
        {
            this.color = newColor;
        }

        private City cityToAttack;

        public void Init()
        {
            //TODO: IMPLEMENT CITY TO ATTACK

            this.cityToAttack = null;
            if (this.cityToAttack == null)
            {
                throw new Exception(ExceptionMessages.NullException);
            }

            this.transform.position = Vector2.MoveTowards(this.transform.position, cityToAttack.transform.position, this.speed * Time.deltaTime);
            this.transform.LookAt(cityToAttack.transform.position);
        }

        public void SetConfig(UnitScriptableObject config)
        {
            this.price = config.Price;
            this.visionRadius = config.VisionRadius;

            this.health = config.HealthPoints;
            this.healthRegenPerSecond = config.HealthRegenPerSecond;

            this.damage = config.Damage;
            this.attackSpeed = config.AttackSpeed;
            this.attackRange = config.AttackRange;

            this.speed = config.Speed;

            this.icon = config.Icon;

            this.type = config.Type;

            var collider = this.gameObject.GetComponent<CircleCollider2D>();
            collider.radius = this.visionRadius;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Unit"))
            {
                var unit = collision.gameObject.GetComponent<Unit>();
                if (unit.owner != this.owner)
                {
                    this.MoveAttackInSight(unit, this.owner, this.damage);
                }
            }
        }

        private void MoveAttackInSight(IDamagable damageReciever, OwnerType owner, float amount)
        {
            var target = damageReciever as Unit;

            var distance = Vector2.Distance(this.transform.position, target.transform.position);

            var attackRangeVector = new Vector3(this.attackRange, this.attackRange);

            //move to unit in vision range
            this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position - attackRangeVector, this.speed * Time.deltaTime);
            this.transform.LookAt(target.transform.position);

            if (distance < attackRange)
            {
                this.GiveDamage(damageReciever, owner, amount);
            }
        }
    }
}
