using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : Interactable
{
	[SerializeField]
	private GameObject collectibleToSpawn;

	[SerializeField]
    private AudioClip collectAudio;

	public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        SpawnCollectible();
		AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
		Destroy(gameObject);
    }

	void SpawnCollectible()
	{
		Instantiate(collectibleToSpawn, transform.position, collectibleToSpawn.transform.rotation);
	}
}
