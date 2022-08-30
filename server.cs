using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCP_Aplication
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("192.168.138.177");
                
                TcpListener myList = new TcpListener(ipAd, 8001);

               
                myList.Start();

                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is  :" +
                                  myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                byte[] b = new byte[100];
                
                int k = s.Receive(b);


                char z = Convert.ToChar(b[0]);
                
                int uname = (int)char.GetNumericValue(z);
                char x = Convert.ToChar(b[uname+1]);
                int passw = (int)char.GetNumericValue(x);

                Console.WriteLine("Recieved...");
                Console.Write("Username :   ");
                for (int i = 1; i < uname+1; i++)
                {

                    Console.Write(Convert.ToChar(b[i]));
                   
                }
                Console.WriteLine();
                //validasi
                int validation;
                if (k-uname <= 9)
                {
                    validation = uname + 2;
                }
                else
                {
                    validation = uname + 3;
                }
                

                //
                Console.Write("Password :   ");
                for (int j = validation; j <k; j++)
                {
                    Console.Write(Convert.ToChar(b[j]));
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Keseluruhan data yang di Passing");
                for(int o = 0; o < k; o++)
                {
                    Console.Write(Convert.ToChar(b[o]));
                }


                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                
                
                s.Close();
                myList.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            Console.ReadKey();
        }
    }
}
