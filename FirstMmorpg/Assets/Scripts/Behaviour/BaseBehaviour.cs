using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBehaviour : BaseController
{
	
	public Slider energySlider;
	public Slider healthSlider;

	GlobalSettings global;

	// Use this for initialization
	void Start ()
	{
		hitpoint = MaxHitpoint;
		StartCoroutine (HitpointRegen ());
		StartCoroutine (UpdateHealthBar ());
		StartCoroutine (UpdateEnergyBar ());
		StartCoroutine (ManaRegen ());
	}

	public void StartRoutines ()
	{
		global = GameObject.Find ("UserInterface").GetComponent<GlobalSettings> ();
		Hitpoint = MaxHitpoint;
		StartCoroutine (HitpointRegen ());
		StartCoroutine (UpdateHealthBar ());
		StartCoroutine (UpdateEnergyBar ());
		StartCoroutine (ManaRegen ());
	}

	public void StartEnemyRoutines ()
	{
		global = GameObject.Find ("UserInterface").GetComponent<GlobalSettings> ();
		Hitpoint = MaxHitpoint;
		StartCoroutine (HitpointRegen ());
		//StartCoroutine (UpdateHealthBar ());
	}

	protected int level = 1;

	public int Level {
		get { return level; }
		set { level = value; }
	}

	[SerializeField]
	protected Class playerClass;

	public Class PlayerClass {
		get { return playerClass; }
		set { playerClass = value; }
	}

	[SerializeField]
	protected float hitpoint = 10f;

	public float Hitpoint {
		set { hitpoint = value; }
		get { return hitpoint; }
	}

	//Max Health
	[SerializeField]
	protected float maxHitPoint = 100f;

	public float MaxHitpoint {
		set { maxHitPoint = value; }
		get { return maxHitPoint; }
	}

	[SerializeField]
	protected int mana = 10;

	public int Mana {
		set { mana = value; }
		get { return mana; }
	}

	//Max Health
	[SerializeField]
	protected int maxMana = 100;

	public int MaxMana {
		set { maxMana = value; }
		get { return maxMana; }
	}

	protected int manaReg = 10;

	public int ManaReg {
		get { return manaReg; }
		set { manaReg = value; }
	}

	public int energyReg = 10;

	public int EnergyReg {
		get { return energyReg; }
		set { energyReg = value; }
	}

	[SerializeField]
	protected int energy = 100;

	public int Energy {
		get { return energy; }
		set { energy = value; }
	}

	[SerializeField]
	protected int maxEnergy = 100;

	public int MaxEnergy {
		get { return maxEnergy; }
		set { maxEnergy = value; }
	}

	protected float bonusDamage;

	public float BonusDamage {
		get { return bonusDamage; }
		set { bonusDamage = value; }
	}

	[SerializeField]
	protected float baseDamageMax = 1f;

	public float BaseDamageMax {
		set { baseDamageMax = value; }
		get { return baseDamageMax; }
	}

	[SerializeField]
	protected float baseDamageMin = 1f;

	public float BaseDamageMin {
		set { baseDamageMin = value; }
		get { return baseDamageMin; }
	}

	[SerializeField]
	protected int attackRange = 1;

	public int AttackRange {
		set { attackRange = value; }
		get { return attackRange; }
	}

	[SerializeField]
	protected float critChance = 10f;

	public float CritChance {
		get { return critChance; }
		set { critChance = value; }
	}

	[SerializeField]
	protected float attackSpeed = 1.5f;

	public float AttackSpeed {
		set { attackSpeed = value; }
		get { return attackSpeed; }
	}

	[SerializeField]
	protected float offHandAttackSpeed = 1.5f;

	public float OffHandAttackSpeed {
		get { return offHandAttackSpeed; }
		set { offHandAttackSpeed = value; }
	}

	[SerializeField]
	protected float hpReg = 0f;
	//Healt Regen per second
	public float HpReg {
		set { hpReg = value; }
		get { return hpReg; }
	}

	protected bool combat = false;

	public bool Combat {
		get { return combat; }
		set { combat = value; }
	}

	protected Buffs myBuffs = new Buffs ();

	public Buffs MyBuffs {
		get { return myBuffs; }
		set { myBuffs = value; }
	}

	protected Buffs myDebuffs = new Buffs ();

	public Buffs MyDebuffs {
		get { return myDebuffs; }
		set { myDebuffs = value; }
	}

	protected float timerOne;

	public float TimerOne {
		get { return timerOne; }
		set { timerOne = value; }
	}

	protected float timerTwo;

	public float TimerTwo {	
		get { return timerTwo; }
		set { timerTwo = value; }
	}

	public void ApplyDamage (float minAmount, float maxAmount, float critChance, GameObject attacker, bool ability)
	{
		float amount = Random.Range (minAmount, maxAmount);
		float crit = Random.Range (0, 100);
		bool critted = false;
		if (crit < critChance) {
			amount *= 2f;
			critted = true;
		}
		
		Hitpoint -= amount;


		if (attacker.tag == "Player")
			global.InitiateEnemyCombatText (amount, ability, critted);
		
		if (Hitpoint < 0) {
			if (tag == "Player")
				OnPlayerDeath (attacker);
			else
				OnNPCDeath (attacker);
		}

	}

	public void ApplyHeal (float amount, GameObject attacker)
	{

		Hitpoint += amount;

	}

	void OnPlayerDeath (GameObject attacker)
	{

		print (attacker.name + " killed you. Resetting heatlh ");
		Hitpoint = MaxHitpoint;

	}

	void OnNPCDeath (GameObject attacker)
	{
		attacker.GetComponent<PlayerBehaviour> ().UpdateExperince (GetComponent<EnemyBehaviour> ().Experience);
		Destroy (gameObject);
		print ("Killed it, high five!");
	}


	IEnumerator HitpointRegen ()
	{
		while (true) {
			
			if (Hitpoint < MaxHitpoint)
				Hitpoint += HpReg;
			else
				hitpoint = maxHitPoint;

			yield return new WaitForSeconds (1f);
		}
	}

	IEnumerator ManaRegen ()
	{
		while (true) {

			if (Mana < MaxMana)
				Mana += ManaReg;
			else
				Mana = MaxMana;

			yield return new WaitForSeconds (1f);
		}
	}

	IEnumerator UpdateEnergyBar ()
	{

		while (true) {
			
			if (Energy < MaxEnergy)
				Energy += 10;
			else
				Energy = MaxEnergy;
			
			energySlider.value = energy;
			yield return new WaitForSeconds (0.5f);
		}
	}

	IEnumerator UpdateHealthBar ()
	{
		while (true) {
			
			healthSlider.maxValue = MaxHitpoint;
			healthSlider.value = Hitpoint;
			yield return new WaitForSeconds (0.2f);

		}
	}
}

public enum Class
{
	Wizard,
	Warrior,
	Assassin,
	Illusionist,
	Mender,
	Summon
}