
using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{
    public float lerpFactor = 4f;

    //public bool playerDead = false;
    public GameObject player = null;       //Public variable to store a reference to the player game object
    public GameManager gameloop;

    private Vector3 offset;       //Private variable to store the offset distance between the player and camera

    private ArrayList affectedGameObjects;

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
        if (player != null)
        {
            float distFromPlayer = Vector3.Distance(transform.position, player.transform.position);

            RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, (player.transform.position - transform.position).normalized, distFromPlayer);
            foreach (RaycastHit raycastHit in raycastHits)
            {
                if (!raycastHit.collider.gameObject.CompareTag(Tags.Player))
                {
                    
                }
            }
        }
    }

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 cameraPos = transform.localPosition;
            cameraPos = player.transform.position + offset;
            cameraPos.y = player.transform.position.y + offset.y;
            cameraPos.x = player.transform.position.x + offset.x/2;
			transform.localPosition = Vector3.Lerp (transform.localPosition, cameraPos, Time.deltaTime * lerpFactor);
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
}