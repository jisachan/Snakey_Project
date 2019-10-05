using UnityEngine;
using UnityEngine.Assertions;

public class Snakey : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    Foods foods;
    [SerializeField]
    Body bodyPrefab;
    Body head;
    GameManager gameManager;
    Event e;
    KeyCode lastKeyPressed = KeyCode.None;
    Vector3 direction;
    Vector2 spawnPosition;

    private void Awake()
    {
        head = GetComponent<Body>();
        Assert.IsNotNull(head, "Failed to find body component \"head\"");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        GetInput();
    }
    // Using "OnTrigger" instead of "OnCollision" 
    // because Snakey otherwise got a bit dislocated when eating food.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Snakey eats food.
        if (collision.gameObject.tag == "foodables")
        {
            Destroy(collision.gameObject);
            foods.SpawnFoods();
            bodyPrefab.SetColor(collision.gameObject);
            SpawnBody();
            gameManager.Score += 15;
        }
        // If Snakey touches the outer wall or a spawned block.
        if (collision.gameObject.tag == "gwall" || collision.gameObject.tag == "block")
        {
            // Stop the game from progressing.
            Time.timeScale = 0;
            gameManager.GameOver();
        }
    }
    public void SpawnBody()
    {
        // Spawns a new bodypart where Snakey is.
        spawnPosition = transform.position;
        Body newBody = Instantiate(bodyPrefab, spawnPosition, Quaternion.identity);
        // Stores the body next to the head in a temp variable.
        Body tempBody = head.next;
        // Assigns the newly spawned body to the bodypart next to the head.
        head.next = newBody;
        // Puts the body part stored in the temp variable, previously next to the head, next to the newly spawned body instead.
        newBody.next = tempBody;
    }
    public void SnakeMovement()
    {
        Vector3 position = head.transform.position + (direction * speed * Time.deltaTime);
        head.Move(position);
    }
    // Necessary method for using the Event class.
    void OnGUI()
    {
        e = Event.current;

        // Checks if the player has pressed a Key on the keyboard.
        // Also checks the very last key pressed, even if two keys are being held down.
        if (e.type.Equals(EventType.KeyDown) && e.keyCode.ToString().Length == 1)
        {
            // Stores the pressed key in a variable.
            lastKeyPressed = e.keyCode;
        }

        // Prevents Snakey from continuing to move forward when the player stops holding down the key
        // Aka. Makes Snakey stop moving when the key is no longer pressed.
        // Also makes sure Snakey moves in the direction of the last key pressed
        // Without stopping if the keys are being held down and switched between simultaniously.
        if (Input.GetKeyUp(lastKeyPressed))
        {
            lastKeyPressed = KeyCode.None;
            if (Input.GetKey(KeyCode.W))
                lastKeyPressed = KeyCode.W;
            if (Input.GetKey(KeyCode.A))
                lastKeyPressed = KeyCode.A;
            if (Input.GetKey(KeyCode.S))
                lastKeyPressed = KeyCode.S;
            if (Input.GetKey(KeyCode.D))
                lastKeyPressed = KeyCode.D;
        }
    }
    private void GetInput()
    {
        // Prevents Snakey from thinking she's Sonic The Hedgehog.
        direction = Vector3.zero;

        if (lastKeyPressed == KeyCode.W)
        {
            direction += Vector3.up;
            SnakeMovement();
        }
        if (lastKeyPressed == KeyCode.A)
        {
            direction += Vector3.left;
            SnakeMovement();
        }
        if (lastKeyPressed == KeyCode.S)
        {
            direction += Vector3.down;
            SnakeMovement();
        }
        if (lastKeyPressed == KeyCode.D)
        {
            direction += Vector3.right;
            SnakeMovement();
        }
    }
}
