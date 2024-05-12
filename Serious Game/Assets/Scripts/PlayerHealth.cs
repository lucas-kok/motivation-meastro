using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Sprite HearthFull;
    public Sprite HearthEmpty;
    public GameObject[] HealthSprites;
    private int _lives;

    private void Start()
    {
        for (int i = 0; i < HealthSprites.Length; i++)
        {
            var sprite = HealthSprites[i].GetComponent<SpriteRenderer>();
            sprite.sprite = HearthFull;
            sprite.color = new Color(1, 1, 1, 1);
        }

        _lives = HealthSprites.Length;
    }

    public void TakeDamage()
    {
        if (!IsAlive())
        {
            return;
        }
        
        _lives--;
        var sprite = HealthSprites[_lives].GetComponent<SpriteRenderer>();
        sprite.sprite = HearthEmpty;
        sprite.color = new Color(0.7647059f, 0.1137255f, 0.1137255f, 1);
    }

    public bool IsAlive()
    {
        return _lives > 0;
    }

    public void Heal()
    {
        if (_lives < HealthSprites.Length)
        {
            var sprite = HealthSprites[_lives].GetComponent<SpriteRenderer>();
            sprite.sprite = HearthFull;
            sprite.color = new Color(1, 1, 1, 1);

            _lives++;
        }
    }
}
