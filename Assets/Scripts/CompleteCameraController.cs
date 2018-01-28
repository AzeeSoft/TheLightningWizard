
using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{
    public float lerpFactor = 4f;
    public float backingFactor = 4f;
    public float loweringFactor = 4f;

    //public bool playerDead = false;
    public GameObject player = null;       //Public variable to store a reference to the player game object
    public GameManager gameloop;
    public Material translucentMaterial;

    private Vector3 offset;       //Private variable to store the offset distance between the player and camera
    private float originalXRotation;

    private ArrayList affectedRenderers = new ArrayList();
    private ArrayList originalMaterials = new ArrayList();

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MyCoroutine());
		gameloop = GameObject.Find("GM").GetComponent <GameManager > ();

    }

    IEnumerator MyCoroutine()
    {
            yield return new WaitForSeconds(.5f);
            findPlayer();
    }

    void Update()
    {

    }

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {
        smartView();

        if (player != null)
        {
            Vector3 cameraPos = transform.localPosition;
            cameraPos = player.transform.position + offset;
            cameraPos.y = player.transform.position.y + offset.y;
            cameraPos.x = player.transform.position.x + offset.x/2;

            CharacterController playerCharacterController = player.GetComponent<CharacterController>();
            if (playerCharacterController.velocity.z < 0)
            {
                Vector3 additionalVector = (cameraPos - player.transform.position).normalized * backingFactor;
                additionalVector.x = 0;

                cameraPos += additionalVector;
            } else if (playerCharacterController.velocity.z > 0)
            {
                cameraPos.y -= loweringFactor;
            }

			transform.localPosition = Vector3.Lerp (transform.localPosition, cameraPos, Time.deltaTime * lerpFactor);
            transform.LookAt(player.transform);
        }

        if (gameloop.playerIsDead==true)
        {
            findPlayer();
            gameloop.playerIsDead = false;
        }
    }

    private void findPlayer(){
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    private void smartView()
    {
        undoSeeThroughRenderers();

        if (player != null)
        {
            float distFromPlayer = Vector3.Distance(transform.position, player.transform.position);

            RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, (player.transform.position - transform.position).normalized, distFromPlayer);
            foreach (RaycastHit raycastHit in raycastHits)
            {
                if (!raycastHit.collider.gameObject.CompareTag(Tags.Player) && !raycastHit.collider.gameObject.CompareTag(Tags.Enemy))
                {
                    seeThroughGameObject(raycastHit.collider.gameObject);
                }
            }
        }
    }

    private void seeThroughGameObject(GameObject targetGameObject)
    {
        Renderer[] renderers = targetGameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer curRenderer in renderers)
        {
            affectedRenderers.Add(curRenderer);
            originalMaterials.Add(curRenderer.material);

            curRenderer.material = translucentMaterial;
        }
    }

    private void undoSeeThroughRenderers()
    {
        for (int i = 0; i < affectedRenderers.Count; i++)
        {
            Renderer affectedRenderer = (Renderer)affectedRenderers[i];
            if (affectedRenderer != null)
            {
                affectedRenderer.material = (Material) originalMaterials[i];
            }
        }

        affectedRenderers.Clear();
        originalMaterials.Clear();
    }
}