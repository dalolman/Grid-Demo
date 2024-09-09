using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit : MonoBehaviour
{
    public Transform tpTarget;
    public GameObject playerObj;
    private Square_Controller player;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Square_Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other.gameObject));
            Debug.Log("Teleporting player.");
            //player.disabled = true;
            //playerObject.transform.position = tpTarget.transform.position;
            //player.disabled = false;
        }
    }

    private IEnumerator Teleport(GameObject other)
    {
        player = playerObj.GetComponent<Square_Controller>();

        if (player)
        {
            player.disabled = true;
            yield return new WaitForSeconds(1f);
            playerObj.transform.position = tpTarget.transform.position;
            yield return new WaitForSeconds(1f);
            player.disabled = false;
        } else
        {
            Debug.Log("Player is null = " + other.name);
        }
    }
}
