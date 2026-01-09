using UnityEngine;
using System.Collections;

// je voulais faire un systeme de porte avec un levier à distance qui se referme pour l'ex 4 mais j'ai changé d'avis entre temps
public class TimedDoorController : TriggerController
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_Delay = 10f; 
    private static readonly string PARAM_NAME = "Knock Knock"; 

    protected override void Interact()
    {
        StartCoroutine(OpenAndCloseDoor());
    }

    private IEnumerator OpenAndCloseDoor()
    {
        m_Animator.SetBool(PARAM_NAME, true);
        CanInteract = false;

        yield return new WaitForSeconds(m_Delay);

        m_Animator.SetBool(PARAM_NAME, false);

        CanInteract = true;
    }
}