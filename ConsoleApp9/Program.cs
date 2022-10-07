using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp9
{
    class Program
    {
        public static void fillHashTable(ref Hashtable arr)
        {
            Random randNum = new Random();
            for (int i = 0; i < arr.Count; i++)
            {
                arr.Add(i, randNum.Next(0, 100));
            }
        }
        public static void fillArray(ref int[] arr)
        {
            Random randNum = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = randNum.Next(0, 100);
            }
        }
        static void Main(string[] args)
        {
            int[] Arr = new int[10000];
            fillArray(ref Arr);
            Timing t = new Timing();
            Stopwatch sw = new Stopwatch();
            TimeSpan timeTaken;
            Hashtable ArrHt = new Hashtable();
            fillHashTable(ref ArrHt);
            sw.Start();
            ArrHt.Values.OfType<int>().Where(s => s == 100);
            sw.Stop();
            timeTaken = sw.Elapsed;
            Console.WriteLine("Время выполнения простого поиска по хэш таблице(StopWatch): " + timeTaken.ToString(@"m\:ss\.fff"));
            t = new Timing();
            t.StartTime();
            ArrHt.Values.OfType<int>().Where(s => s == 100);
            t.StopTime();
            Console.WriteLine($"Время выполнения простого поиска по хэш таблице (Timing): {t.Result().ToString()}");

            ExchangeSorting exchangeSorting = new ExchangeSorting();

        sw.Start();
        exchangeSorting.BubleSort(Arr); // метод сортировки
        sw.Stop();
        timeTaken = sw.Elapsed;

        Console.WriteLine("Время выполнения сортировки пузырьком (StopWatch): " + timeTaken.ToString(@"m\:ss\.fff"));

        t = new Timing();
        t.StartTime();
        exchangeSorting.BubleSort(Arr); // метод сортировки
        t.StopTime();
        Console.WriteLine($"Время выполнения сортировки пузырьком (Timing): {t.Result().ToString()}");

        SimpleSearch simpleSearch = new SimpleSearch();

        sw.Start();
        simpleSearch.Search(Arr, 100); // метод поиска
        sw.Stop();
        timeTaken = sw.Elapsed;

        Console.WriteLine("Время выполнения простого поиска(StopWatch): " + timeTaken.ToString(@"m\:ss\.fff"));

        t = new Timing();
        t.StartTime();
        simpleSearch.Search(Arr, 100); // метод поиска
        t.StopTime();
        Console.WriteLine($"Время выполнения простого поиска(Timing): {t.Result().ToString()}");


        SearchBinary searchBinary = new SearchBinary();

        sw.Start();
        searchBinary.Search(Arr, 100); // метод поиска
        sw.Stop();
        timeTaken = sw.Elapsed;

        Console.WriteLine("Время выполнения бинарного поиска(StopWatch): " + timeTaken.ToString(@"m\:ss\.fff"));

        t = new Timing();
        t.StartTime();
        searchBinary.Search(Arr, 100); // метод поиска
        t.StopTime();
        Console.WriteLine($"Время выполнения бинарного поиска(Timing): {t.Result().ToString()}");
        }
    }
    internal class ExchangeSorting
    {
        public void BubleSort(int[] a)
        {
            int N = a.Length;
            for (int i = 1; i < N; i++)
            {
                for (int j = N - 1; j >= i; j--)
                {
                    if (a[j - 1] > a[j])
                    {
                        int t = a[j - 1];
                        a[j - 1] = a[j];
                        a[j] = t;
                    }
                }
            }
        }
        //сортировка обменом ak пузыриком
    }
    internal class SearchBinary
    {
        public int Search(int[] a, int x)
        {
            int middle, left = 0, right = a.Length - 1;
            do
            {
                middle = (left + right) / 2;
                if (x > a[middle])
                    left = middle + 1;
                else
                    right = middle - 1;
            }
            while ((a[middle] != x) && (left <= right));
            if (a[middle] == x)
                return middle;
            else return -1;
        }
        //Бинарный поиск
    }
    internal class SimpleSearch
    {
        public int Search(int[] a, int x)
        {
            int i = 0;
            while (i < a.Length && a[i] != x) i++;
            if (i < a.Length)
                return i;
            else return -1;
        }
        //Прямой поиск
    }
    internal class Timing

    {
        TimeSpan duration; 
        TimeSpan[] threads; 
        public Timing()
        {
            duration = new TimeSpan(0);
            threads = new TimeSpan[Process.GetCurrentProcess().
            Threads.Count];
        }
        public void StartTime() 
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            for (int i = 0; i < threads.Length; i++)
                threads[i] = Process.GetCurrentProcess().Threads[i].
                UserProcessorTime;
        }
        public void StopTime() 
        {   
            TimeSpan tmp;
            for (int i = 0; i < threads.Length; i++)
            {
                tmp = Process.GetCurrentProcess().Threads[i].
                UserProcessorTime.Subtract(threads[i]);
                if (tmp > TimeSpan.Zero)
                    duration = tmp;
            }
        }
        public TimeSpan Result()
        {
            return duration;
        }
    }
}
