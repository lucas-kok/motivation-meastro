using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Sprite hearthFull;
    public Sprite hearthEmpty;
    public GameObject[] healthSprites;
    private int _lives;

    private Color _fullHearthColor = new Color(1, 1, 1, 1);
    private Color _emptyHearthColor = new Color(0.7647059f, 0.1137255f, 0.1137255f, 1);

private void Start()
    {
        for (int i = 0; i < healthSprites.Length; i++)
        {
            var sprite = healthSprites[i].GetComponent<SpriteRenderer>();
            sprite.sprite = hearthFull;
            sprite.color = _fullHearthColor;
        }

        _lives = healthSprites.Length;
    }

    public void TakeDamage()
    {
        if (!IsAlive())
        {
            return;
        }

        _lives--;
        var sprite = healthSprites[_lives].GetComponent<SpriteRenderer>();
        sprite.sprite = hearthEmpty;
        sprite.color = _emptyHearthColor;
    }

    public bool IsAlive()
    {
        return _lives > 0;
    }

    public void Heal()
    {
        if (_lives < healthSprites.Length)
        {
            var sprite = healthSprites[_lives].GetComponent<SpriteRenderer>();
            sprite.sprite = hearthFull;
            sprite.color = new Color(1, 1, 1, 1);

            _lives++;
        }
    }
}
