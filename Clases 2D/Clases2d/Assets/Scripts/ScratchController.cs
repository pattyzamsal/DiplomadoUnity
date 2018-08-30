using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchController : MonoBehaviour
{
    public Animator scrachAnimator;
    bool isMoving = false;
    private Camera sceneCamera;
    float life = 1.0f;

    void Awake()
    {
        //Referenciamos a la camara de la scene
        sceneCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Use this for initialization
    void Start()
    {
        GameObject.Find("Canvas/ScreenGame/SliderLife").GetComponent<Slider>().value = life;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento Lateral

        // Detectamos que el usuario esta presionando la flecha derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            this.transform.Translate(Vector3.right * Time.deltaTime * 5f);
        }

        // Detectamos que el usuario esta presionando la flecha izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            this.transform.Translate(Vector3.left * Time.deltaTime * 5f);
        }
        //Salto del personaje
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ejecutarSalto();
        }

        //Deteccion de movimiento
        isMoving = false;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
        }
        scrachAnimator.SetBool("ScrachIsMoving", isMoving);

    }

    void LateUpdate()
    {
        //Movimiento de la camara
        sceneCamera.transform.position = new Vector3(this.transform.position.x, sceneCamera.transform.position.y, sceneCamera.transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        // Si el personaje toca el piso
        if (col.collider.gameObject.tag == "Terrain")
        {
            Debug.Log("El personaje toco el piso");

            // Le avisa al animator controller que el personaje toco el piso
            scrachAnimator.SetBool("ScrachIsOnFloor", true);
        }

        // Si el personaje toca un enemigo
        if (col.collider.gameObject.tag == "Enemies")
        {

            // Esto evita que el personaje no cambie la animacion de saltar cuando cae encima del enemigo
            scrachAnimator.SetBool("ScrachIsOnFloor", true);

            // Disminuimos la cantidad de vida en 0.2f
            life = life - 0.2f;

            // Le avisamos al Slider que actualice la barra
            GameObject.Find("Canvas/ScreenGame/SliderLife").GetComponent<Slider>().value = life;

            // Preguntamos si la sangre del personaje es menor o igual a 0
            if (life <= 0)
            {
                // Buscar y eliminar la pantalla ScreenGame
                GameObject.Destroy(GameObject.Find("Canvas/ScreenGame"));

                // Cargamos la pantalla ScreenLose
                GameObject newScreen = GameObject.Instantiate(Resources.Load("UI/Prefabs/ScreenLose") as GameObject, GameObject.Find("Canvas").transform);

                // Le asignamos el nombre apropiado a la pantalla
                newScreen.name = "ScreenLose";

                // Movemos esa pantalla dentro del gameObject canvas
                newScreen.transform.parent = GameObject.Find("Canvas").transform;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // Si el personaje toca una moneda...
        if (col.gameObject.tag == "Coins")
        {
            // Busco el gameobject que tiene el sonido de la moneda
            GameObject.Find("Sounds/Coins").GetComponent<AudioSource>().Play();

            // Reconstruimos el gameobject de una explosion partir de un prefab
            GameObject newExplosion = GameObject.Instantiate(Resources.Load("Prefabs/Explosion") as GameObject);

            // Posicionamos la explosion en el lugar donde estaba la moneda
            newExplosion.transform.position = col.gameObject.transform.position;

            // Le asignamos un nombre al game object de la explosion
            newExplosion.name = "NuevaExplosion";

            // Asociamos la explosion al game object contenedor de explosiones
            newExplosion.transform.parent = GameObject.Find("ExplosionContainer").transform;

            // Destruye la moneda
            GameObject.Destroy(col.gameObject);

            // Destruyo la explosion 5 segundos despues de la colision
            GameObject.Destroy(newExplosion, 5);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("OnCollisionExit2D"); //Imprime en la consola lo que este en paréntesis
        scrachAnimator.SetBool("ScrachIsOnFloor", false);
    }

    public void ejecutarSalto()
    {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}
