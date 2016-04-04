/// <summary>
///     Author:     Alex Seceanschi & John Friesen
///     Student#:   0690827 & 0666315
///     Date:       Created: April 6, 2016
///     Purpose:    A Visual Studio Console App project called PictionaryService. 
///                     The main purpose of this project is to offer the meta data 
///                     of our library to our clients. By doing this we are 
///                     about to play this game with multiple client and 
///                     over a network. NOTE: all firewall setting must be
///                     turned off to be able to play over local network.
/// </summary>

using PictionaryLibrary;
using System;
using System.ServiceModel;

namespace PictionaryService
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost servHost = null;
            try
            {
                // Create the service host 
                servHost = new ServiceHost(typeof(DrawCanvasBoard));

                // Start the service
                servHost.Open();
                Console.WriteLine("Service started. Press a key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
                if (servHost != null)
                    servHost.Close();
            }
        }
    }
}
