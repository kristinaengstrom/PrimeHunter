using System;
using System.Threading;

namespace PrimeHunter
{
    class Program
    {
        static bool workerThreadIsActive = false;
        static long maxPrimeFound = 3;
        static int secondsAllowed = 5;

        static void Main(string[] args)
        {
            Console.WriteLine("Calculate the largest prime number found in {0} seconds.\n", secondsAllowed);

            // create separate thread for finding primes
            Thread workerThread = new Thread(FindPrimeNumbers);
            workerThread.Start();

            try
            {
                // start timing
                for (int iCounter = 1; iCounter <= secondsAllowed; iCounter++)
                {
                    Thread.Sleep(1000); // sleep for 1 sec				
                    // display progress so far
                    Console.WriteLine("Seconds elapsed: {0}\tMax Prime Found: {1}", iCounter, maxPrimeFound);
                }

                // stop thread for finding primes
                workerThread.Abort();
            }
            catch (Exception ex)
            {
                if (workerThreadIsActive)

                {
                    workerThread.Abort();
                }
            }

            // display largest prime found
            Console.WriteLine("\nLargest prime number found in {0} seconds is {1}.", secondsAllowed, maxPrimeFound);

            // end			
            Console.Write("\nPress any key to end.");
            Console.ReadKey();

        }

        static void FindPrimeNumbers()
        {
            long iCounter = maxPrimeFound; // starting with maxPrimeFound

            workerThreadIsActive = true;

            // use brute force to find each succeeding prime.  (We can optimize later.))		
            try
            {
                while (true)
                {
                    // as each prime is identified, save it to global maxPrimeFound
                    if (IsPrime(iCounter))
                    {
                        maxPrimeFound = iCounter;
                    }
                    iCounter++;
                }
            }
            catch (Exception ex)
            {
                workerThreadIsActive = false;
            }
        }
        /// <summary>
        /// Determine if the given value is prime
        /// </summary>
        /// <param name="primeCandidate"></param>
        /// <returns></returns>
        static bool IsPrime(long primeCandidate)
        {
            bool isPrime = true; // assume prime until we prove otherwise
            long iDivisor = 1;
            // optimization: no factor can be greater than target's square root (or truncated integer value of sq rt)
            long iMaxDivisor = (long)Math.Round(Math.Sqrt(Convert.ToDouble(primeCandidate)), 0);
            long iRemainder;

            while ((iDivisor < iMaxDivisor) && isPrime)
            {
                iDivisor++;
                iRemainder = (primeCandidate % iDivisor);
                isPrime = (iRemainder != 0);
            }

            return isPrime;
        }
    }
}
