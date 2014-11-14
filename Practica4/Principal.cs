	/*
		 * Created by SharpDevelop.
		 * User: Usuario
		 * Date: 13/11/2014
		 * Time: 03:43 p. m.
		 * 
		 * To change this template use Tools | Options | Coding | Edit Standard Headers.
		 */
			using System;
			namespace Practica4
			{
				public class Principal
				{
					public static void Main (String[] args)
					{
						try{

						LectorDeArchivos lector = new LectorDeArchivos("ejemploCSV.csv");
						lector.leer();	
						Console.WriteLine();
							Console.ReadKey(true);
						   }
				        catch 	(Exception e) {
						Console.WriteLine ("Exception :" + e.Message);
						}

					}
				}
			}