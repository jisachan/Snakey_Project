using UnityEngine;

public class Body : MonoBehaviour
{
    public Body next;
    public void Move(Vector3 position)
    {
        // Save my old position.
        Vector3 oldPosition = transform.position;
        // Move to my new position.
        transform.position = position;
        // If I have a tail.
        if (next != null)
        {
            // Tell the tail to move to my old position.
            next.Move(oldPosition);
        }
    }
    // Customized colors to use for the Colour method.
    Color kiwiC = new Color(0.6480517f, 0.9056604f, 0.4058384f, 1f);
    Color cakeC = new Color(0.7075472f, 0.3622267f, 0.1835618f, 1f);
    Color cherryC = new Color(1f, 0.3509313f, 0.2028302f, 1f);
    Color lemonC = new Color(1f, 0.8630052f, 0.2768779f, 1f);
    Color eggC = Color.white;
    Color garlicC = new Color(1f, 0.6745283f, 0.764688f, 1f);
    Color grapeC = new Color(0.7341785f, 0.5568262f, 0.9150943f, 1f);
    Color pumpkinC = new Color(1f, 0.5581784f, 0.240566f, 1f);
    
    // Method for changing the snake body's color depending on what food they eat.
    public void SetColor(GameObject foodie)
    {
        if (foodie.name == "kiwi(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = kiwiC;
        }
        if (foodie.name == "cake(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = cakeC;
        }
        if (foodie.name == "cherry(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = cherryC;
        }
        if (foodie.name == "lemon(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = lemonC;
        }
        if (foodie.name == "egg(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = eggC;
        }
        if (foodie.name == "garlic(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = garlicC;
        }
        if (foodie.name == "grape(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = grapeC;
        }
        if (foodie.name == "pumpkin(Clone)")
        {
            gameObject.GetComponent<SpriteRenderer>().color = pumpkinC;
        }
    }
}