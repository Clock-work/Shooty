using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateScript : MonoBehaviour
{
    [SerializeField]
    public Text pointsShow;

    [SerializeField]
    public Button MovementSpeed;

    [SerializeField]
    public Button CooldownShoot;

    [SerializeField]
    public Button healthUpgrade;

    [SerializeField]
    public Button damageUpgrade;

    [SerializeField]
    public Button pierceUpgrade;

    [SerializeField]
    public Button specialUpgrade;

    public int points;

    [SerializeField]
    public PlayerScript player;

    [SerializeField]
    public Text timeField;

    private int m_moveCost = 2;
    private int m_cooldownCost = 1;
    private int m_healthCost = 20;
    private int m_damageCost = 100;
    private int m_pierceCost = 75;
    private int m_projSpeedCost = 3;

    private void Awake()
    {
        points = 0;
        MovementSpeed.onClick.AddListener(IncreaseMovement);
        CooldownShoot.onClick.AddListener(DecreaseCooldown);
        healthUpgrade.onClick.AddListener(increaseHealth);
        damageUpgrade.onClick.AddListener(increaseDamage);
        pierceUpgrade.onClick.AddListener(increasePierce);
        specialUpgrade.onClick.AddListener(addProjSpeed);
    }

    void Start()
    {

    }

    void Update()
    {
        pointsShow.text = "Points: " + points.ToString();
        MovementSpeed.GetComponentInChildren<Text>().text = "Speed cost(1): " + m_moveCost;
        CooldownShoot.GetComponentInChildren<Text>().text = "Firerate cost(2): " + m_cooldownCost;

        healthUpgrade.GetComponentInChildren<Text>().text = "Health cost(3): " + m_healthCost;
        damageUpgrade.GetComponentInChildren<Text>().text = "Damage cost(4): " + m_damageCost;

        pierceUpgrade.GetComponentInChildren<Text>().text = "Pierce cost(5): " + m_pierceCost;
        specialUpgrade.GetComponentInChildren<Text>().text = "Proj Speed cost(6): " + m_projSpeedCost;

        timeField.GetComponentInChildren<Text>().text = "Time: " + player.getSecondsAlive() + "s";

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreaseMovement();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            DecreaseCooldown();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            increaseHealth();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            increaseDamage();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            increasePierce();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            addProjSpeed();
        }

    }

    void IncreaseMovement()
    {
        if (points >= m_moveCost)
        {
            points -= m_moveCost;
            m_moveCost *= 2;
            player.changeMovementSpeed(1.1f);
        }
    }

    void DecreaseCooldown()
    {
        if (points >= m_cooldownCost)
        {
            points-=m_cooldownCost;
            m_cooldownCost *= 2;
            player.changeShootCooldown(0.9f);
        }
    }

    void increaseHealth()
    {
        if (points >= m_healthCost)
        {
            points -= m_healthCost;
            m_healthCost = (int)(m_healthCost * 1.5);
            player.changeHealth(1);
        }
    }

    void increaseDamage()
    {
        if (points >= m_damageCost)
        {
            points -= m_damageCost;
            m_damageCost *= 2;
            player.changeDamage(1);
        }
    }

    void increasePierce()
    {
        if (points >= m_pierceCost)
        {
            points -= m_pierceCost;
            m_pierceCost *= 2;
            player.changePierce(1);
        }
    }

	void addProjSpeed()
    {
        if (points >= m_projSpeedCost)
        {
            points -= m_projSpeedCost;
            m_projSpeedCost *= 2;
            player.changeProjectileSpeed(1.15f);
        }
    }

}