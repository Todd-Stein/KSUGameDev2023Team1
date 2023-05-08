using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player_health : MonoBehaviour
{
    #region Overlays
    [SerializeField]
    public GameObject tonyVision;
    [SerializeField]
    public GameObject bloodOverlay;
    private Material tonyMaterialRef;

    private MeshRenderer bloodMaterialRef;
    #endregion

    public int totalHealth = 3;
    public int currentHealth;
    public bool isDead;

    public delegate void OnDeath();
    public static OnDeath onDeath;

    public AudioClip playerHurt;
    public float hurtAudioVolume = 1.0f;

    public bool bloodact = false;

    private Color tmpcolor;
    private float bloodtimer = 2f;
    private float btime = 100000000;

    private void Awake()
    {
        currentHealth = totalHealth;
        if (bloodOverlay == null)
        {
            bloodOverlay = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        }
        //bloodMaterialRef = bloodOverlay.GetComponent<MeshRenderer>();
        bloodOverlay.SetActive(false);

        tmpcolor = bloodOverlay.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            RecieveHit();
        }*/

        if (bloodact)
        {
            if(tmpcolor.a <= .7 - currentHealth * .1)
            {
                tmpcolor.a += 2 * Time.deltaTime;
            }
            else
            {
                bloodact = false;
            }
            bloodOverlay.GetComponent<Image>().color = tmpcolor;
        }

        if(Time.time >= btime + bloodtimer)
        {
            if (tmpcolor.a >= 0)
            {
                tmpcolor.a -= 2 * Time.deltaTime;
            }
            bloodOverlay.GetComponent<Image>().color = tmpcolor;
        }
    }
    public void RecieveHit()
    {
        currentHealth--;
        isDead = currentHealth <= 0;

        if(playerHurt != null)
            AudioSource.PlayClipAtPoint(playerHurt, transform.position, hurtAudioVolume);

        bloodOverlay.SetActive(true);
        bloodact = true;
        btime = Time.time;
        /*if (bloodMaterialRef.material != null)
        {
            bloodMaterialRef.material.SetFloat("_randomizeBlood1", Random.Range(200, 600));
            bloodMaterialRef.material.SetFloat("_randomizeBlood2", Random.Range(4000, 6000));
        }*/
        if (isDead)
        {
            // Invoke onDeath delegate
            onDeath?.Invoke();
            bloodOverlay.SetActive(false);
            isDead = false;

            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
