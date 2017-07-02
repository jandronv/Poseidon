using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CastilloArena : MonoBehaviour {

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
	public Vector2 posInGrid;
	private float _mTimeToAddVida = 0;

	private bool firstIni = false;

	public List<Sprite> Sprites;
    public int clicks=0;
    public CastleManager CM;
	void Start () {
		
		VidaActual = 0;
	}
	
	// Update is called once per frame
	void Update () {



		if (EnConstruccion) {

			GetComponentInChildren<Kids>().GetComponent<SpriteRenderer>().enabled = true;
			
			//Lanzar Anim niño
			if (VidaActual == Estados[0] && firstIni)
			{
				//Debug.Log("Cambiamos al sprite 0");
				GetComponentInChildren<Kids>().volver = true;
				GetComponentInChildren<Kids>().ReiniciaTiempo();

				GetComponent<SpriteRenderer>().sprite = Sprites[0];
				//lanzar animacion de llorar
				EnConstruccion = false;
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
		}
	}

	/// <summary>
	/// Metodo que se llama cada vez que pulsas en un castillo
	/// </summary>
	public void OnMouseDown()
	{
        if (EnConstruccion) { 
            clicks++;
        }
        //Restamos una vida por click
        RestaVida(5);
	}

	/// <summary>
	/// Metodos que se llama cada vez que queremos restar vida al castillo
	/// </summary>
	/// <param name="vidas">Numero de vidas a restar</param>
	public void RestaVida(int vidas)
	{
		if (VidaActual >= 1) {
			VidaActual--;
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



}
