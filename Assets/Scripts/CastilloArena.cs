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
	public int VidaActual = 1;
	[Tooltip("Velocidad a la que se van añadiendo vidas en segundos")]
	public float VelocidadMontaje = 10;
	[Tooltip("Numero de vidas por ticks.")]
	public int numVidasPorTicks = 1;
	public bool CastilloTerminado = false;



	private float _mTimeToAddVida = 0;

	// Use this for initialization
	void Start () {
		
		VidaActual = 1;
	}
	
	// Update is called once per frame
	void Update () {



		if (VidaActual == 0)
		{
			Debug.Log("Castillo destruido");
			//TODO revisar esto
			Destroy(gameObject);
		}
		else if (VidaActual == Estados[0])
		{

			Debug.Log("Cambiamos al sprite 1");
		}
		else if (VidaActual == Estados[1])
		{
			Debug.Log("Cambiamos al sprite 2");
		}
		else if (VidaActual == Estados[2])
		{
			Debug.Log("Cambiamos al sprite 3");
		}
		else if (VidaActual == Estados[3])
		{
			Debug.Log("Cambiamos al sprite 4");
		}
		else if (VidaActual == Estados[4])//VidaMaxima
		{
			Debug.Log("Castillo terminado, sprite 5");
			CastilloTerminado = true;
			//Meter sprite terminado
		}

		//Cada x 
		_mTimeToAddVida += Time.deltaTime;

		if (_mTimeToAddVida > VelocidadMontaje && !CastilloTerminado)
		{
			Debug.Log("Añadiendo "+ numVidasPorTicks+ " vidas al castillo");
			_mTimeToAddVida = 0;
			VidaActual += numVidasPorTicks;
		}
	}

	/// <summary>
	/// Metodo que se llama cada vez que pulsas en un castillo
	/// </summary>
	public void OnMouseDown()
	{

		//Restamos una vida por click
		RestaVida(1);
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




}
