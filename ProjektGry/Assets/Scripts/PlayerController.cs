using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int coinsCount;
    private int keys1Count;
    private int keys2Count;
    private int flaskCount;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI keys1Text;
    [SerializeField] private TextMeshProUGUI keys2Text;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI flaskText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int health;
    [SerializeField] private HealthBar healthBar;

    private bool buttonPressing;
    private float appearTime = 2f;
    private bool nextToDoor;
    private GameObject door;
    private bool nextToChest;
    private int maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        nextToDoor = false;
        coinsCount = 0;
        keys1Count = 0;
        keys2Count = 0;
        flaskCount = 0;
        Invoke("SetCountText", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            buttonPressing = true;
        else
            buttonPressing = false;
        if (nextToDoor)
            OpenDoor(door);
        else if (nextToChest)
            OpenChest();
        if (health == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().gameObject.SetActive(false);
            FindObjectOfType<GameManager>().Invoke("EndGame", 1);
        }
        if (Input.GetButtonDown("Fire1") && flaskCount > 0 && health < maxHealth)
            DrinkFlask(30);
    }

    private void SetCountText()
    {
        coinsText.text = coinsCount.ToString() + "/50";
        keys1Text.text = keys1Count.ToString() + "/1";
        keys2Text.text = keys2Count.ToString() + "/" + GameObject.FindGameObjectsWithTag("Key2").Length;
        flaskText.text = flaskCount.ToString() + "/" + GameObject.FindGameObjectsWithTag("Flask").Length;
        healthText.text = health.ToString() + "/100";
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Coin"))
        {
            coinsCount++;
            FindObjectOfType<AudioManager>().Play("CollectCoin");
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name.Equals("Key1"))
        {
            keys1Count++;
            FindObjectOfType<AudioManager>().Play("CollectKey");
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name.Equals("Key2"))
        {
            keys2Count++;
            FindObjectOfType<AudioManager>().Play("CollectKey");
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name.Equals("Flask"))
        {
            flaskCount++;
            FindObjectOfType<AudioManager>().Play("Flask");
            Destroy(col.gameObject);
        }
        else if (col.gameObject.name.Equals("Door"))
        {
            nextToDoor = true;
            door = col.gameObject;
        }
        else if (col.gameObject.name.Equals("ChestTrigger"))
        {
            nextToChest = true;
        }
        else if (col.gameObject.name.Equals("Peaks"))
        {
            actionText.text = "Health -40";
            actionText.gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Hurt");
            Invoke("Hide", appearTime);
            TakeDamage(40);
        }

        Invoke("SetCountText", 0.1f);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Door"))
        {
            nextToDoor = false;
            actionText.gameObject.SetActive(false);
        }
        else if (col.gameObject.name.Equals("ChestTrigger"))
        {
            nextToChest = false;
            actionText.gameObject.SetActive(false);
        }
    }

    private void OpenDoor(GameObject door)
    {
        if (buttonPressing)
        {
            if (keys2Count > 0)
            {
                FindObjectOfType<AudioManager>().Play("DoorOpen");
                Destroy(door);
                keys2Count--;
                SetCountText();
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("DoorLocked");
                actionText.text = "Silver key required.";
                actionText.gameObject.SetActive(true);
            }
        }
    }

    private void OpenChest()
    {
        if (buttonPressing)
        {
            if (keys1Count > 0 && coinsCount >= 30)
            {
                FindObjectOfType<AudioManager>().Play("ChestOpen");
                GameObject.Find("Chest").GetComponent<Animator>().Play("ChestOpenAnimation");
                FindObjectOfType<GameManager>().Invoke("WinGame", 1);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("DoorLocked");
                actionText.text = "Gold key and min. 30 coins required.";
                actionText.gameObject.SetActive(true);
            }
        }
    }

    private void Hide()
    {
        actionText.gameObject.SetActive(false);
    }

    private void TakeDamage(int damage)
    {
        if (damage > 0 && health - damage < 0)
            health = 0;
        else if (damage < 0 && health - damage > 100)
            health = 100;
        else
            health -= damage;
        healthBar.SetHealth(health);
    }

    private void CollectItem()
    {
    }

    private void DrinkFlask(int recovery)
    {
        FindObjectOfType<AudioManager>().Play("Drink");
        flaskCount--;
        TakeDamage(-recovery);
        Invoke("SetCountText", 0.1f);
    }
}