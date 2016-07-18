using System;
using System.Threading;
using System.Diagnostics;

namespace PrimeHunter
{
    class Program
    {
        static bool workerThreadIsActive = false; // we'll need to keep tabs on the extra thread
        static long maxPrimeFound = 1; // initial value is trivial case
        static int secondsAllowed = 60; 

        /// <summary>
        /// Simple application to identify and write out as many prime numbers
        /// as possible in a given time period.
        /// </summary>
        /// <param name="args"></param>
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

            // end program	
            Console.Write("\nPress any key to end.");
            Console.ReadKey();
        }

        /// <summary>
        /// Find all prime numbers greater than some given value.
        /// As each prime is found, store it in a global variable.
        /// </summary>
        static void FindPrimeNumbers()
        {
            long iCounter = maxPrimeFound + 1; // always pick up just after last found value, i.e., maxPrimeFound

            workerThreadIsActive = true;

            // use brute force to find each succeeding prime.  		
            try
            {
                while (true)
                {
                    // as each prime is identified, save it to global maxPrimeFound
                    if (IsPrime(iCounter))
                    {
                        maxPrimeFound = iCounter;
                        DisplayFirstFewValues(maxPrimeFound, 30);
                    }
                    iCounter++;
                }
            }
            catch (Exception ex)
            {
                workerThreadIsActive = false;
            }
        }

        [Conditional("DEBUG")]
        static void DisplayFirstFewValues(long displayValue, long maxDisplayValue)
        { 
            if (displayValue < maxDisplayValue)
            {
                Console.WriteLine("Prime: {0}", maxPrimeFound);
            }
        }


        /// <summary>
        /// Determine if the given value is prime
        /// </summary>
        /// <param name="primeCandidate"></param>
        /// <returns>True if input is prime; otherwise, returns false.</returns>
        static bool IsPrime(long primeCandidate)
        {
            bool isPrime = true; // assume prime until we prove otherwise
            long iDivisor = 1;
            // optimization: no factor can be greater than target's square root (or truncated integer value of sq rt)
            long iMaxDivisor = (long)Math.Round(Math.Sqrt(Convert.ToDouble(primeCandidate)), 0);

            while ((iDivisor < iMaxDivisor) && isPrime)
            {
                iDivisor++;
                isPrime = ((primeCandidate % iDivisor) != 0);
            }

            return isPrime;
        }
    }
}
