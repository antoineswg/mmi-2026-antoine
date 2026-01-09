using UnityEngine;

// de base je voulais que le personnage reste le meme entre les scenes mais je n'arrivais pas à reset sa position en 0 0 0 donc j'ai fini par juste le placer manuellement dans chaque scene
public class PersistentObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}