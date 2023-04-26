using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        currentHealth = totalHealth;
        if (bloodOverlay == null)
        {
            bloodOverlay = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        }
        bloodMaterialRef = bloodOverlay.GetComponent<MeshRenderer>();
        bloodOverlay.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            RecieveHit();
        }*/
    }
    public void RecieveHit()
    {
        currentHealth--;
        isDead = currentHealth <= 0;


        bloodOverlay.SetActive(true);
        if (bloodMaterialRef.material != null)
        {
            bloodMaterialRef.material.SetFloat("_randomizeBlood1", Random.Range(200, 600));
            bloodMaterialRef.material.SetFloat("_randomizeBlood2", Random.Range(4000, 6000));
        }
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
