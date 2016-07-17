using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PrimeHunter
{
    class Program
    {
        //public Thread workerThread;
        public bool workerThreadIsActive = false;
        static long maxPrimeFound = 1;
        static int secondsAllowed = 5;

        static void Main(string[] args)
        {
            // create separate thread for finding primes
            //workerThread = new Thread(FindPrimeNumbers);
            //workerThread.Start();

            try
            {
                // start timing
                for (int iCounter = 0; iCounter < secondsAllowed; iCounter++)
                {
                    Thread.Sleep(1000); // sleep for 1 sec				
                                        // display progress so far
                    Console.WriteLine("Seconds elapsed: {0}\tMax Prime Found: {1}", iCounter, maxPrimeFound);
                }

                // stop thread for finding primes
                //workerThread.abort();
            }
            catch (Exception ex)
            {
                //if  workerThreadIsActive

                //{
                //    workerThread.abort();
                //}
            }

            // display largest prime found
            Console.WriteLine("\nLargest prime number found in {0} is {1}", secondsAllowed, maxPrimeFound);

            Console.Write("\nPress any key to end.");

            Console.ReadKey();
            // end			

        }

        private void FindPrimeNumbers()
        {
            long iCounter = 0;
            long localMaxPrime = maxPrimeFound; // starting with maxPrimeFound

            workerThreadIsActive = true;

            // use brute force to find each succeeding prime.  (We can optimize later.))		
            try
            {
                while (true)
                {
                    // as each prime is identified, save it to global maxPrimeFound
                    if (IsPrime(iCounter))
                    {
                        maxPrimeFound = localMaxPrime;
                    }
                    iCounter++;
                }
            }
            catch (Exception ex)
            {
                workerThreadIsActive = false;
            }
        }

        private bool IsPrime(long primeCandidate)
        {
            bool isPrime = true; // assume prime until we prove otherwise
            //long iDivisor = 2;

            //while (iDivisor < primeCandidate && isPrime)
            //{
            //    isPrime = (primeCandidate % iDivisor == 0);
            //    iDivisor++;
            //}

            return isPrime;
        }
    }
}
