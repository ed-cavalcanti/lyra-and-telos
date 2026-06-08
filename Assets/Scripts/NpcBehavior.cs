using UnityEngine;
using UnityEngine.InputSystem;

public class NpcBehavior : MonoBehaviour
{
    [Header("Configurações do NPC")]
    public DialogueData dialogueData;

    private DialogueSystem dialogueSystem;
    private bool isPlayerInRange = false;

    void Start()
    {
        // Agora ele encontra o DialogueSystem mesmo se o Painel começar desligado na cena.
        dialogueSystem = FindAnyObjectByType<DialogueSystem>(FindObjectsInactive.Include);

        if (dialogueSystem == null)
        {
            Debug.LogError("DialogueSystem não encontrado na cena! Certifique-se de que ele está presente.");
        }
    }

    void Update()
    {
        // Se o jogador estiver perto e apertar a tecla "E"
        if (isPlayerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("A tecla 'E' foi pressionada corretamente dentro do alcance!");

            // 1. Verifica se achou o Sistema de Diálogo
            if (dialogueSystem == null)
            {
                Debug.LogError("ERRO: dialogueSystem está nulo! O Start() não conseguiu encontrá-lo.");
                return;
            }

            // 2. Verifica se o sistema acha que já tem um diálogo rolando
            if (dialogueSystem.IsDialogueActive)
            {
                Debug.LogWarning("AVISO: O sistema acha que um diálogo já está ativo.");
                return;
            }

            // 3. Verifica se o NPC tem falas configuradas no Inspector
            if (dialogueData == null)
            {
                Debug.LogError("ERRO: O DialogueData deste NPC está vazio! Coloque o ScriptableObject no Inspector do NPC.");
                return;
            }

            Debug.Log("Tudo certo! Chamando StartDialogue...");
            dialogueSystem.StartDialogue(dialogueData);
        }
    }
    // Assumindo que seu jogo é 2D. Se for 3D, mude para OnTriggerEnter(Collider other)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Pressione 'E' para falar com o NPC.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
