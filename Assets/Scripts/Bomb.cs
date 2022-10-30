using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Bomb : MonoBehaviour
{
    public AudioClip explosionSound;
    public GameObject explosionPrefab;
    public LayerMask levelMask;
    
    private bool exploded = false;

    
    void Start ()
    {
        Invoke ("Explode", 3f); //Вызов взрыва через 3 секунды
    }

    void Explode ()
    {
        //Explosion sound
        AudioSource.PlayClipAtPoint (explosionSound, transform.position);

        //Создание первого взрыва в позиции бомбы
        Instantiate (explosionPrefab, transform.position, Quaternion.identity);

        //Цепочка взрывов
        StartCoroutine (CreateExplosions (Vector3.forward));
        StartCoroutine (CreateExplosions (Vector3.right));
        StartCoroutine (CreateExplosions (Vector3.back));
        StartCoroutine (CreateExplosions (Vector3.left));

        GetComponent<MeshRenderer> ().enabled = false; //Включить меш
        exploded = true; 
        transform.Find ("Collider").gameObject.SetActive (false); //Выключить колайдер
        Destroy (gameObject, .3f); 
    }

    public void OnTriggerEnter (Collider other)
    {
        if (!exploded && other.CompareTag ("Explosion"))
        { 
            CancelInvoke ("Explode"); 
            Explode (); 
        }
    }

    private IEnumerator CreateExplosions (Vector3 direction)
    {
        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit; 

            Physics.Raycast (transform.position + new Vector3 (0, .5f, 0), direction, out hit, i, levelMask);

            if (!hit.collider)
            { 
                Instantiate (explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            } else
            { 
                break;
            }

            yield return new WaitForSeconds (.05f); 
        }

    }
}
