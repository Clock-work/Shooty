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
    public int points = 0;
    public float movementSpeed = 2.5f;
    public float fireRate = 0.5f;

    [SerializeField]
    public PlayerScript player;

    void Start()
    {
        points = 10;
        MovementSpeed.onClick.AddListener(IncreaseMovement);
        CooldownShoot.onClick.AddListener(DecreaseCooldown);
    }

    void Update()
    {
        pointsShow.text = points.ToString();
    }

    void IncreaseMovement()
    {
        Debug.Log("Increase Movement");
        if (points > 0)
        {
            movementSpeed += 1;
            points--;
            player.ReloadStats(movementSpeed, fireRate);
        }
    }

    void DecreaseCooldown()
    {
        Debug.Log("Decrease Cooldown");
        if (points > 0 && fireRate - 0.1f > 0)
        {
            fireRate -= 0.1f;
            points--;
            player.ReloadStats(movementSpeed, fireRate);
        }
    }
}