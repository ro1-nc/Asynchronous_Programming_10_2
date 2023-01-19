using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Operation_Termination_10_2
{

    class Program
    {
        public static CancellationTokenSource _tokensource = null;




        static void Main(string[] args)
        {
            int x = 5;

            _tokensource = new CancellationTokenSource();
            var token = _tokensource.Token;

            bool dir = Infinite(x, token);
           



            Console.ReadKey();
        }



        public static bool Infinite(int counter, CancellationToken token)
        {
            bool dir = false;

            Cancel(token, dir);
            for (int i = 0; i <= counter; i++)
            {

                
                if (token.IsCancellationRequested)
                {
                    _tokensource.Dispose();
                    break;
                }
                else
                {                 
                    Console.WriteLine(i);
                }
                if (i == counter)
                {
                    dir = true;
                    Console.WriteLine("Process completed");
                    _tokensource.Dispose();
                }

                Thread.Sleep(2000);
                //Task.Delay(20000);

                //Cancel(token);

            }
            return dir;
        }

        public static async void Cancel(CancellationToken token, bool dir)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Please press Enter to cancel the process");

                Console.ReadKey();

                if (!dir)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        _tokensource.Cancel();
                        try
                        {
                            token.ThrowIfCancellationRequested();


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Process has been cancelled");
                            Console.WriteLine(e.Message);

                        }
                    }
                }



                //string x = Console.ReadLine();
                //if (x == "x")
                //{
                //    _tokensource.Cancel();
                //    try
                //    {
                //        token.ThrowIfCancellationRequested();

                //        Console.WriteLine("Process completed");
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine("Process has been cancelled");
                //        Console.WriteLine(e.Message);

                //    }

                //}
                //else //if(x=="")
                //{
                //    Console.WriteLine("Process completed");
                //}
            });





        }



    }


}
