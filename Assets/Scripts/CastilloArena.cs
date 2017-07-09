using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastilloArena : MonoBehaviour
{

    public int TipoCastillo;
    [Tooltip("Cada valor de la lista representa los estados del castillo, es decir, vida inicial, estados intermedios y el ultimo representa la vida maxima")]
    public List<int> Estados;
    [Tooltip("Vida maxima del castillo")]
    public float VidaActual = 1;
    [Tooltip("Velocidad a la que se van añadiendo vidas en segundos")]
    public float VelocidadMontaje = 10;
    [Tooltip("Numero de vidas por ticks.")]
    public int numVidasPorTicks = 1;
    public bool EnConstruccion = false;
    public bool CastilloTerminado = false;
    public bool volviendo = false;
    public Vector2 posInGrid;
    private float _mTimeToAddVida = 0;

    private bool firstIni = false;

    public List<Sprite> Sprites;
    public int clicks = 0;
    public int clicksWave = 0;
    public CastleManager CM;
    void Start()
    {

        VidaActual = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponentInChildren<Kids>().GetComponent<SpriteRenderer>().enabled == false)
        {
            volviendo = false;
            GetComponentInChildren<Kids>().GetComponent<Animator>().Rebind();

        }

        if (EnConstruccion)
        {

            GetComponentInChildren<Kids>().GetComponent<SpriteRenderer>().enabled = true;

            //Lanzar Anim niño
            if (VidaActual == Estados[0] && firstIni)
            {
                //Debug.Log("Cambiamos al sprite 0");
                GetComponentInChildren<Kids>().volver = true;
                GetComponentInChildren<Kids>().ReiniciaTiempo();

                spawnShell();

                GetComponent<SpriteRenderer>().sprite = Sprites[0];
                firstIni = false;
                //lanzar animacion de llorar
                EnConstruccion = false;
                volviendo = true;
                firstIni = false;
                CM.numCastlesDestroyed++;


            }
            else if (VidaActual > Estados[1] && VidaActual <= Estados[2])
            {
                //Debug.Log("Cambiamos al sprite 1");
                GetComponentInChildren<Kids>().GetComponent<Animator>().SetTrigger("Castillo");
                GetComponent<SpriteRenderer>().sprite = Sprites[1];

            }
            else if (VidaActual > Estados[2] && VidaActual <= Estados[3])
            {
                //Debug.Log("Cambiamos al sprite 2");

                GetComponent<SpriteRenderer>().sprite = Sprites[2];

            }
            else if (VidaActual > Estados[3] && VidaActual <= Estados[4])
            {
                //Debug.Log("Cambiamos al sprite 3");
                GetComponent<SpriteRenderer>().sprite = Sprites[3];
            }
            else if (VidaActual >= Estados[4])//VidaMaxima
            {
                //Debug.Log("Castillo terminado, sprite 4");
                GetComponent<SpriteRenderer>().sprite = Sprites[4];
                GetComponentInChildren<Kids>().GetComponent<Animator>().SetTrigger("Terminado");
                CastilloTerminado = true;
                //Meter sprite terminado
            }




            //Método de dudosa funcionalidad. 
            //Cada medio segundo, todos los castillos pierden un punto de vida
            if (PlayerPrefs.GetString("activeSkill") == "Storm")
            {
                if (_mTimeToAddVida > VelocidadMontaje / 2 && VidaActual > 0)
                {
                    VidaActual--;
                }
            }


            //Cada x 
            _mTimeToAddVida += Time.deltaTime;
            //Debug.Log("_mTimeToAddVida: " + _mTimeToAddVida);
            if (_mTimeToAddVida > VelocidadMontaje && !CastilloTerminado)
            {
                //Debug.Log("Añadiendo " + numVidasPorTicks + " vidas al castillo");
                _mTimeToAddVida = 0;
                VidaActual += numVidasPorTicks;
                firstIni = true;
            }

            //La habilidad Storm hace que cada ronda se reste un punto de vida, o el que sea

        }
    }

    /// <summary>
    /// Metodo que se llama cada vez que pulsas en un castillo
    /// </summary>
    public void OnMouseDown()
    {
        if (EnConstruccion)
        {
            clicks++;
            clicksWave++;
        }
        //Restamos una vida por click
        RestaVida(1);
    }

    /// <summary>
    /// Metodos que se llama cada vez que queremos restar vida al castillo
    /// </summary>
    /// <param name="vidas">Numero de vidas a restar</param>
    public void RestaVida(int vidas)
    {
        if (VidaActual - vidas >= 0)
        {
            VidaActual -= vidas;
        }
        else
        {
            VidaActual = 0;
        }
        if (CastilloTerminado)
        {
            CastilloTerminado = false;
        }
    }

    public void SetPositionInGrid(int x, int y)
    {
        posInGrid = new Vector2(x, y);

    }

    void spawnShell()
    {
        if (Random.value >= 0.8) //%20 percent chance (1 - 0.8 is 0.2)
        { //code here

            //ATENCIÓN: HAY VECES QUE NO SPAWNEAN LAS CONCHAS; MOTIVO DESCONOCIDO. PARA HACER PRUEBAS, Random value>=0 (100% de casos salen conchas)
            this.transform.FindChild("Shell").GetComponent<SpriteRenderer>().enabled = true;
            this.transform.FindChild("Shell").GetComponent<Animator>().enabled = true;
            this.transform.FindChild("Shell").GetComponent<Animator>().Play("shellSpawn");

            CM.totalShells++;
            StartCoroutine(waitForAnimation(this.transform.FindChild("Shell").GetComponent<Animator>()));
            //this.transform.FindChild("Shell").GetComponent<SpriteRenderer>().enabled = false;

        }

    }

    IEnumerator waitForAnimation(Animator anim)
    {

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("shellSpawn"))
        {
            yield return null;// Avoid any reload.
        }

        this.transform.FindChild("Shell").GetComponent<SpriteRenderer>().enabled = false;
        this.transform.FindChild("Shell").GetComponent<Animator>().enabled = false;
    }


}
