using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VfxSpawner : MonoBehaviour
{
    private enum eSoundState
    {
        Explosion
    }

    public GameObject explosionPrefab;
    public UnityAction<Vector3> onExplosionComplet;
    public UnityAction<Vector3> onExplosionsComplet;
    List<GameObject> explosionGos = new List<GameObject>();

    public AudioClip audioExplosion;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();

    }

    public void SpawnExplosion(Vector3 tpos)
    {
        PlaySound(eSoundState.Explosion);
        GameObject explosionGo = Instantiate<GameObject>(this.explosionPrefab);
        explosionGo.transform.position = tpos;
        //this.OnSpawnvfxExplosionAction(vfxExplosionGo);
        var explosion = explosionGo.GetComponent<Vfx>();
        explosion.onExplosionComplet = () =>
        {
            this.onExplosionComplet(tpos);
        };
    }

    public void SpawnExplosions(Vector3 tpos)
    {
        PlaySound(eSoundState.Explosion);
        StartCoroutine(this.SpawnExplosionsRoutine(tpos));
    }

    private IEnumerator SpawnExplosionsRoutine(Vector3 tpos)
    {
        for (int i = 0; i < 20; i++)
        {
            var randPosX = Random.Range(-1.5f, 1.5f) + tpos.x;
            var randPosY = Random.Range(-1.5f, 1.5f) + tpos.y;
            Vector3 ranPos = new Vector3(randPosX, randPosY, 0);

            GameObject explosionGo = Instantiate<GameObject>(this.explosionPrefab);
            explosionGos.Add(explosionGo);
            explosionGo.transform.position = ranPos;
            var explosion = explosionGo.GetComponent<Vfx>();
            explosion.onExplosionComplet = () =>
            {
                explosionGos.Remove(explosionGo);
            };
            yield return new WaitForSeconds(0.1f);
        }

        while (true)
        {            
            if (explosionGos.Count == 0)
            {
                this.onExplosionsComplet(tpos);
                break;
            }
            yield return null;
        }        
    }

    private void PlaySound(eSoundState state)
    {
        return;
        if (state == eSoundState.Explosion)
        {
            audioSource.clip = audioExplosion;
        }
        audioSource.Play();
    }

}
