
using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{
    public float lerpFactor = 4f;
    public float backingFactor = 4f;
    public float loweringFactor = 0.3f;
    public float rotationSpeed = 2f;

    public float distanceBehindPlayer = 3.5f;
    public float distanceAbovePlayer = 1.8f;
    public float postRiseAngle = -15f;

    //public bool playerDead = false;
    public GameObject player = null;       //Public variable to store a reference to the player game object
    public GameManager gameloop;
    public Material translucentMaterial;

    private Vector3 offset;       //Private variable to store the offset distance between the player and camera
    private float originalXRotation;
    private float originalDistance;

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
            float hor = Input.GetAxis("Mouse X");
            float ver = Input.GetAxis("Mouse Y");



            transform.LookAt(player.transform.position);
            transform.RotateAround(player.transform.position, Vector3.up, hor * rotationSpeed);
//            transform.RotateAround(player.transform.position, transform.right, ver * rotationSpeed * -1);

            float distDiff = distanceBehindPlayer - Vector3.Distance(transform.position, player.transform.position);

            Vector3 cameraPos = transform.position;
            cameraPos += transform.forward.normalized * -1 * distDiff;
            cameraPos.y = player.transform.position.y + distanceAbovePlayer;

            //            cameraPos.y = player.transform.position.y + offset.y;
            //            cameraPos.x = player.transform.position.x + offset.x/2;

            CharacterController playerCharacterController = player.GetComponent<CharacterController>();
            if (playerCharacterController.velocity.magnitude > 0 || playerCharacterController.velocity.magnitude < 0)
            {
                Vector3 playerFwd = player.transform.forward;
                playerFwd.y = 0;

                Vector3 cameraFwd = transform.forward;
                cameraFwd.y = 0;

                float angle = Vector3.Angle(playerFwd, cameraFwd);

                if (Mathf.Abs(angle) > 90)
                {
                    Vector3 additionalVector = (cameraPos - player.transform.position).normalized*backingFactor;
                    additionalVector.x = 0;

                    cameraPos += additionalVector;
                }
                else
                {
                    cameraPos.y -= loweringFactor;
                }
            }

            /*if (player.GetComponent<TeleportMove>().currentState != TeleportMove.State.Idle)
            {
                Vector3 additionalVector = (cameraPos - player.transform.position).normalized * backingFactor;
                additionalVector.x = 0;

                cameraPos += additionalVector;
            }*/

//            transform.position = cameraPos;
            transform.position = Vector3.Lerp(transform.position, cameraPos, Time.fixedDeltaTime * lerpFactor);
            transform.Rotate(Vector3.right, postRiseAngle);
            
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
        originalDistance = Vector3.Distance(transform.position, player.transform.position);
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