using UnityEngine;

public class DamagePlatform : MonoBehaviour
{
    [SerializeField] private ColourablePlatform colourable;
    [SerializeField] private int damage;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player)
            && (player as IColourable).GetCurrentColourIndex(out int playerColourIndex)
            && (colourable as IColourable).GetCurrentColourIndex(out int colourIndex))
        {
            if(playerColourIndex == -colourIndex) player.Combat.Damage(damage);
        }
    }
}
