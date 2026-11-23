using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using InputKey = UnityEngine.InputSystem.Key;

namespace Tito
{
    public class Chest : MonoBehaviour
    {
        public enum State { Locked, Unlocked, Open }

        [Header("Estado")]
        [SerializeField] private State currentState = State.Locked;

        [Header("Interaccion")]
        [SerializeField] private InputKey interactKey = InputKey.E;
        [SerializeField] private float interactDistance = 3f;

        [Header("Animacion")]
        [SerializeField] private Animator animator;
        [SerializeField] private string openAnimation = "Open";

        [Header("Objetos que desaparecen")]
        [Tooltip("Desaparece con la 1ra E (al desbloquear)")]
        [SerializeField] private GameObject firstObject;
        [Tooltip("Desaparece con la 2da E (al abrir)")]
        [SerializeField] private GameObject secondObject;
        [SerializeField] private float hideDuration = 0.6f;
        [SerializeField] private float spinSpeed = 360f;

        [Header("Objeto que aparece (dentro del Spawn)")]
        [SerializeField] private Transform spawnPoint;
        [Tooltip("Si lo dejas vacio, usa el primer hijo del Spawn Point")]
        [SerializeField] private GameObject objectInsideSpawn;
        [SerializeField] private float appearDuration = 0.5f;

        private PlayerInventory player;

        private void Awake()
        {
            if (animator == null)
                animator = GetComponentInChildren<Animator>();

            if (objectInsideSpawn == null && spawnPoint != null && spawnPoint.childCount > 0)
                objectInsideSpawn = spawnPoint.GetChild(0).gameObject;

            // El objeto del spawn empieza oculto
            if (objectInsideSpawn != null)
                objectInsideSpawn.SetActive(false);
        }

        private void Update()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null || !keyboard[interactKey].wasPressedThisFrame)
                return;

            Debug.Log($"[Cofre] Pulsaste E | Estado: {currentState}");

            if (currentState == State.Open)
                return;

            if (player == null)
                player = FindFirstObjectByType<PlayerInventory>();

            if (player == null)
            {
                Debug.LogWarning("[Cofre] No existe ningun PlayerInventory en la escena");
                return;
            }

            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > interactDistance)
            {
                Debug.Log($"[Cofre] Estas lejos ({distance:F1} m). Maximo: {interactDistance} m");
                return;
            }

            Interact();
        }

        private void Interact()
        {
            // 1ra E: Locked -> Unlocked, desaparece el objeto 1
            if (currentState == State.Locked)
            {
                if (player.UseKey())
                {
                    currentState = State.Unlocked;
                    Debug.Log("[Cofre] DESBLOQUEADO. Pulsa E otra vez para abrir.");

                    if (firstObject != null)
                        StartCoroutine(ShrinkAndHide(firstObject, null));
                }
                else
                {
                    Debug.Log("[Cofre] Necesitas una llave");
                }
                return;
            }

            // 2da E: Unlocked -> Open, desaparece el objeto 2, aparece el del spawn
            if (currentState == State.Unlocked)
                OpenChest();
        }

        private void OpenChest()
        {
            currentState = State.Open;
            Debug.Log("[Cofre] ABIERTO");

            if (animator == null)
                Debug.LogWarning("[Cofre] No hay Animator");
            else if (!animator.HasState(0, Animator.StringToHash(openAnimation)))
                Debug.LogWarning($"[Cofre] El Animator no tiene el estado '{openAnimation}'");
            else
                animator.Play(openAnimation, 0, 0f);

            if (secondObject != null)
                StartCoroutine(ShrinkAndHide(secondObject, RevealSpawnObject));
            else
                RevealSpawnObject();
        }

        private IEnumerator ShrinkAndHide(GameObject obj, System.Action onFinished)
        {
            Transform t = obj.transform;
            Vector3 startScale = t.localScale;
            Quaternion startRotation = t.localRotation;
            float time = 0f;

            while (time < hideDuration)
            {
                time += Time.deltaTime;
                float progress = Mathf.Clamp01(time / hideDuration);

                t.localScale = Vector3.Lerp(startScale, Vector3.zero, progress * progress);
                t.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.Self);

                yield return null;
            }

            // Se desactiva (no se destruye) y recupera su escala
            obj.SetActive(false);
            t.localScale = startScale;
            t.localRotation = startRotation;

            onFinished?.Invoke();
        }

        private void RevealSpawnObject()
        {
            if (objectInsideSpawn == null)
            {
                Debug.LogWarning("[Cofre] No hay objeto dentro del Spawn Point");
                return;
            }

            StartCoroutine(GrowIn(objectInsideSpawn));
        }

        private IEnumerator GrowIn(GameObject obj)
        {
            Transform t = obj.transform;
            Vector3 targetScale = t.localScale;

            t.localScale = Vector3.zero;
            obj.SetActive(true);

            float time = 0f;
            while (time < appearDuration)
            {
                time += Time.deltaTime;
                float progress = Mathf.Clamp01(time / appearDuration);

                float pop = 1f + 0.2f * Mathf.Sin(progress * Mathf.PI);
                t.localScale = targetScale * (progress * pop);

                yield return null;
            }

            t.localScale = targetScale;
        }
    }
}