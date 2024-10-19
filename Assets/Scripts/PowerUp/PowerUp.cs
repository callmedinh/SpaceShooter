using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int powerupID;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private BulletData bulletData;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                player.ActivateBulletPowerup(bulletData);
            }
            //if (player != null)
            //{
            //    AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            //   switch (powerupID)
            //    {
            //        case 0:
            //            player.TripleShotActive(); break;
            //        case 1:
            //            player.SpeedBoostActive(); break;
            //        case 2:
            //            player.ShieldActive(); break;
            //        default:
            //            Debug.Log("Default"); break;
            //    }
            //}
            Destroy(this.gameObject);
        }
    }
}
