using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Steuerung der Spielfigur. 
/// </summary>
public class Player : Saveable
{
    /// <summary>
    /// Laufgeschwindigkeit der Figur. 
    /// </summary>
    public float speed = 0.05f;

    /// <summary>
    /// Die Kraft, mit der nach oben gesprungen wird. 
    /// </summary>
    public float jumpPush = 1f;

    /// <summary>
    /// Verstärkung der Gravitation, damit die Figur schneller fällt. 
    /// </summary>
    public float extraGravity = 20f;

    /// <summary>
    /// Das grafische Modell, u.a. für die Drehung in Laufrichtung. 
    /// </summary>
    public GameObject model;

    public AudioSource soundJump;

    public AudioSource soundDeath;

    /// <summary>
    /// Der Winkel zu dem sich die Figur um die eigene Achse (=Y)
    /// drehen soll. 
    /// </summary>
    private float towardsY = 0f;

    /// <summary>
    /// Zeiger auf die Physik-Komponente. 
    /// </summary>
    private Rigidbody rigid;

    /// <summary>
    /// Zeiger auf die Animations-Komponente der Spielfigur. 
    /// </summary>
    private Animator anim;

    /// <summary>
    /// Ist die Figur gerade auf dem Boden?
    /// Wenn false, fällt oder springt sie. 
    /// </summary>
    private bool onGround = false;

    override protected void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        base.Start();
        setRagdollMode(false); 
    }

    /// <summary>
    /// Aktiviert oder deaktiviert die Gliederpuppen-Simulation.
    /// </summary>
    /// <param name="isDead">Wenn true, dann ist die Ragdoll akiv, sonst der interaktive Spielmodus.</param> 
    private void setRagdollMode(bool isDead)
    {
        //1st!
        foreach(Collider c in GetComponentsInChildren<Collider>())
        {
            if(c.gameObject.name.StartsWith("mixamorig:")) // nur wenn dies ein Ragdoll-Bone.
            {
                c.enabled = isDead;
            }
        }
        foreach(Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            if (r.gameObject.name.StartsWith("mixamorig:"))
            {
                r.isKinematic = !isDead;
            }
        }

        //2nd!
        GetComponent<Rigidbody>().isKinematic = isDead;
        GetComponent<Collider>().enabled = !isDead;
        GetComponentInChildren<Animator>().enabled = !isDead;

        if(isDead)
        {
            ScreenFader sf = FindObjectOfType<ScreenFader>();
            sf.fadeOut(true, 1f);

            CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
            if (cvc != null)
            {
                cvc.Follow = null;
                cvc.LookAt = null;
            }

            soundDeath.Play();

            enabled = false;
            return;
        }
    }
    /// <summary>
    /// Aktueller Gesundheitszustand in Leben von 0 bis 3.
    /// </summary>
    public float health = 0.6f;

    //Lässt die Spielfigur sterben.
    public void looseHealth()
    {
        health -= 0.2f;

        if (health <= 0f)
        {
            setRagdollMode(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y < -2.34f) //wenn Spieler runtergefallen -> sterben
        {
            looseHealth();
            return;
        }

        if (Time.timeScale == 0f)
            return; //wenn pausiert, dann Update abbrechen.
        float h = Input.GetAxis("Horizontal"); //Eingabesignal fürs Laufen
        anim.SetFloat("forward", Mathf.Abs(h));

        // Vorwärts bewegen:
        transform.position += h * speed * transform.forward;

        // Drehen: 
        if (h > 0f) //nach rechts gehen
            towardsY = 0f;
        else if (h < 0f) //nach links gehen
            towardsY = -180f; 

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0f, towardsY, 0f), Time.deltaTime * 10f);

        // Springen: 
        RaycastHit hitInfo;
        onGround = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.6f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);

        if(onGround && Vector3.Angle(Vector3.up, hitInfo.normal)> 10) //rutscht
        {
            rigid.AddForce(hitInfo.normal);
        }

        anim.SetBool("grounded", onGround);
        if (Input.GetAxis("Jump") > 0f && onGround)
        {
            if (!soundJump.isPlaying)
            {
                soundJump.Play();
            }

            Vector3 power = rigid.velocity;
            power.y = jumpPush;
            rigid.velocity = power;
        }
        rigid.AddForce(new Vector3(0f, extraGravity, 0f));

        //Schießen:
        if (Input.GetAxisRaw ("Fire2") > 0f)
        {
            GetComponentInChildren<Gun>().shoot();
        }

    }

    override protected void Awake()
    {
        base.Awake();

        CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
        if (cvc !=null )
        {
            cvc.Follow = transform;
            cvc.LookAt = transform;
        }
    }

    override protected void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);
        savegame.playerPosition = transform.position;
        savegame.recentScene = gameObject.scene.name;
        savegame.playerHealth = health;
    }

    override protected void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);
        if(savegame.recentScene==gameObject.scene.name) //nur wenn die geladene Szene die ist, in der zuletzt die Position gespeichert wurde, ... 
            transform.position = savegame.playerPosition; //... dann stelle die gespeicherte Spielerposition wieder her.
        health = (savegame.playerHealth);
    }
} 