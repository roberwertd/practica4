
using System;
using System.IO;
using System.Collections;
namespace Practica4
{
	public class LectorDeArchivos
	{
		private String path;
		private Hashtable tabla;
		private ArrayList listaDeLLaves;
		
		public LectorDeArchivos(String rutaArchivo)
		{
			this.path = rutaArchivo;
			this.tabla = new Hashtable();
			this.listaDeLLaves = new ArrayList();
		}
		
		public void leer ()
		{
			String linea;
			StreamReader lector;
			if(File.Exists(this.path))
			{
				try
				{
					lector = new StreamReader(this.path);
					linea = lector.ReadLine();
					convertirLineaEnCampos( linea );
					while(lector.Peek() > -1)
					{
						linea = lector.ReadLine();
						convertirLineaEnCampos( linea );
					}
					lector.Close();
					
				}
				catch ( Exception e )
				{
					Console.WriteLine("Exception: " + e.Message);
				}
				finally
				{
					Console.WriteLine("Cerrando la lectura...");
					Console.WriteLine();
				}
				imprimirArchivoCSV();
			}
			else
				Console.WriteLine(" Archivos Por Favor ");
		}
		
		
		private void convertirLineaEnCampos(String lineaAconvertir )
		{
			ArrayList arraylistDeCampos = new ArrayList();
			String nuevaPalabra ="";
			int contadorComas = 0;
			String key ="";
			String[] linea = lineaAconvertir.Split(new char[] {','});
			foreach(String palabra in linea)
			{
				nuevaPalabra += palabra;
				if( contadorComas == 0){
					key = nuevaPalabra;
					this.listaDeLLaves.Add(key);
					nuevaPalabra = "";
					contadorComas++;
				}else{
					arraylistDeCampos.Add(nuevaPalabra);
					nuevaPalabra = "";
					contadorComas++;
				}
			}
			this.tabla.Add(key,arraylistDeCampos);
		}
		
		private void imprimirBordesdeTabla()
		{
			int numeroDeColumnas = obtenerNumeroDeColumnas();
			Console.Write("+");
			for(int columna = 0; columna < numeroDeColumnas; columna++){
				int anchoColumna = obtenerAnchoDeColumna(columna)+2;
				for(int cont = 0; cont < anchoColumna; cont++)
					Console.Write("-");
				Console.Write("+");
			}
			Console.WriteLine();
		}
		
		private int obtenerAnchoDeColumna(int columnaAcalcular)
		{
			int ancho = 0;
			ICollection keyColl = this.tabla.Keys;
			ICollection valColl = this.tabla.Values;
			String cadenaMayor = "";
			bool esTablaPrimaria;
			if(columnaAcalcular > 0)
				esTablaPrimaria = false;
			else
				esTablaPrimaria = true;
			
			if( esTablaPrimaria){
				foreach(Object valorLLaveTabla in keyColl)
					if( valorLLaveTabla.ToString().Length > cadenaMayor.Length )
						cadenaMayor = valorLLaveTabla.ToString();
				
				ancho= cadenaMayor.Length;
				
			}else{
				cadenaMayor = "";
				foreach(ArrayList valorEnTabla in valColl)
					if( valorEnTabla[columnaAcalcular-1].ToString().Length > cadenaMayor.Length )
						cadenaMayor = valorEnTabla[columnaAcalcular-1].ToString();
				
				ancho= cadenaMayor.Length;
			}
			return ancho;
		}
		
		private void imprimirArchivoCSV()
		{
			Console.WriteLine("Imprimiendo...");
			Console.WriteLine();
			ICollection valuesCol = this.tabla.Values;
			imprimirBordesdeTabla();
			for(int indiceListaDeLLaves = 0; indiceListaDeLLaves< this.listaDeLLaves.Count; indiceListaDeLLaves++){
				imprimirCampoDeColumnaPrimaria(indiceListaDeLLaves);
				imprimirRestoDeTupla( indiceListaDeLLaves);
				if(indiceListaDeLLaves == 0)
					imprimirBordesdeTabla();
			}
			imprimirBordesdeTabla();
		}
		
		private void imprimirCampoDeColumnaPrimaria(int renglon)
		{
			String nuevaLinea = "| ";
			ICollection keyColl = this.tabla.Keys;
			String espaciadoMaximo = "";
			int numeroDeColumnas = obtenerNumeroDeColumnas();
			int espaciosDeLinea = obtenerAnchoDeColumna(0);
		
			foreach(Object valorLLaveTabla in keyColl){
				for(int i = valorLLaveTabla.ToString().Length; i < espaciosDeLinea; i++)
					espaciadoMaximo += " ";
				
				if( this.listaDeLLaves[renglon].Equals(valorLLaveTabla) )
					nuevaLinea +=  valorLLaveTabla + espaciadoMaximo + " |";
				
				espaciadoMaximo ="";
			}			
			Console.Write(nuevaLinea);
		}
		
		
		private void imprimirRestoDeTupla(int tupla)
		{
			String restoTupla = " ";
			int numeroDeColumnas = obtenerNumeroDeColumnas();
			String espaciosEnBlanco = "";
			String nombreLLave = this.listaDeLLaves[tupla].ToString();
			ArrayList lista = (ArrayList)this.tabla[nombreLLave];
			
			foreach(String palabra in lista)
			{
				int anchoColumna = obtenerAnchoDeColumna( (lista.IndexOf(palabra)+1) );
				if(palabra.Length < anchoColumna){
					for(int espacio = palabra.Length; espacio < anchoColumna;espacio++)
						espaciosEnBlanco += " ";
					}
				restoTupla += palabra + espaciosEnBlanco +" | ";
				espaciosEnBlanco ="";
			}
			Console.Write(restoTupla);
			Console.WriteLine();
		}
		
		private int obtenerNumeroDeColumnas()
		{
			int numeroColumnas = 0;
			ICollection valoresTabla = this.tabla.Values;
			foreach(ArrayList lista in valoresTabla)
				for(int contadorDeColumnas=0;contadorDeColumnas <= lista.Count;contadorDeColumnas++)
					numeroColumnas = contadorDeColumnas;
			return numeroColumnas+1;
		}
		
		
	}
}