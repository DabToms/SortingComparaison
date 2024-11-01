namespace SortingComparaison;

/// <summary>
/// Klasa startowa programu.
/// </summary>
internal class Program
{
    /// <summary>
    /// Klasa startowa programu.
    /// </summary>
    /// <param name="args">Anrumenty systemu.</param>
    /// <returns>Reprezentacja operacji asynchronicznej.</returns>
    public static async Task Main(string[] args)
    {
        for (int samplesCount = 10; samplesCount <= 100000; samplesCount *= 10)
        {
            List<Task<SortingResultContext>> sortingTasks = new List<Task<SortingResultContext>>();
            Console.WriteLine($"Sorting for {samplesCount} samples.");

            // stworzenie setu o wielości z losowych liczb(mogą się powtarzać) o długości i
            var randomArray = CreateArrayOfRandom(samplesCount);

            // sortowanie ze zmierzeniem czasu dla każdego z algorytmów i dodanie wyniku do listy wyników
            sortingTasks.Add(SortDataAsync(randomArray, CHashLinqSort, "O(Nlog(N))"));
            sortingTasks.Add(SortDataAsync(randomArray, BubbleSort, "O(n^2)"));
            sortingTasks.Add(SortDataAsync(randomArray, SelectionSort, "O(n^2)"));
            sortingTasks.Add(SortDataAsync(randomArray, InsertionSort, "O(n^2)"));
            sortingTasks.Add(SortDataAsync(randomArray, QuickSort, "O(Nlog(N))"));
            sortingTasks.Add(SortDataAsync(randomArray, MergeSort, "O(Nlog(N))"));

            // Await all sorting tasks
            var sortingResults = await Task.WhenAll(sortingTasks);

            // wypisanie z listy SortingResultContext
            Console.WriteLine($"Name\t\t||\t\tComplexity\t\t||\t\tDuration(in ticks)");
            foreach (var result in sortingResults.OrderBy(x => x.SortingDuration))
            {
                result.PrintResults();
            }
        }
    }

    /// <summary>
    /// Asynchroniczne sortowanie danych.
    /// </summary>
    /// <param name="randomArray">Tablica liczb całkowitych.</param>
    /// <param name="sortingAlgorithm">Metoda sortująca.</param>
    /// <param name="complexity">Przewidywana złożoność.</param>
    /// <returns>Kontekst wyniku sortowania.</returns>
    private static Task<SortingResultContext> SortDataAsync(int[] randomArray, Action<int[]> sortingAlgorithm, string complexity)
    {
        return Task.Run(() =>
        {
            var startingTicks = DateTime.Now.Ticks;
            sortingAlgorithm((int[])randomArray.Clone()); // Clone array to prevent modification
            var endingTicks = DateTime.Now.Ticks;

            return new SortingResultContext
            {
                Name = sortingAlgorithm.Method.Name,
                Complexity = complexity,
                SortingDuration = endingTicks - startingTicks,
            };
        });
    }

    /// <summary>
    /// Sortowanie za pomocą wbudowanego sortowania w c#.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void CHashLinqSort(int[] arr)
    {
        Array.Sort(arr); // Corrected to use Array.Sort for C# Linq sort
    }

    /// <summary>
    /// BubbleSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    /// <summary>
    /// SelectionSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void SelectionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            int temp = arr[i];
            arr[i] = arr[minIndex];
            arr[minIndex] = temp;
        }
    }

    /// <summary>
    /// InsertionSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void InsertionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = key;
        }
    }

    /// <summary>
    /// QuickSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void QuickSort(int[] arr)
    {
        QuickSort(arr, 0, arr.Length - 1);
    }

    /// <summary>
    /// QuickSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void QuickSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(arr, left, right);
            QuickSort(arr, left, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, right);
        }
    }

    /// <summary>
    /// MEtoda do partycjonowania w Quicksort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static int Partition(int[] arr, int left, int right)
    {
        int pivot = arr[right];
        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp2 = arr[i + 1];
        arr[i + 1] = arr[right];
        arr[right] = temp2;
        return i + 1;
    }

    /// <summary>
    /// MergeSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void MergeSort(int[] arr)
    {
        MergeSort(arr, 0, arr.Length - 1);
    }

    /// <summary>
    /// MergeSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            MergeSort(arr, left, middle);
            MergeSort(arr, middle + 1, right);
            Merge(arr, left, middle, right);
        }
    }

    /// <summary>
    /// Metoda merge-ująca w MergeSort.
    /// </summary>
    /// <param name="arr">Tabliza liczb całkowitych.</param>
    private static void Merge(int[] arr, int left, int middle, int right)
    {
        int[] temp = new int[arr.Length];
        for (int i = left; i <= right; i++)
        {
            temp[i] = arr[i];
        }

        int j = left;
        int k = middle + 1;
        int l = left;
        while (j <= middle && k <= right)
        {
            if (temp[j] <= temp[k])
            {
                arr[l] = temp[j];
                j++;
            }
            else
            {
                arr[l] = temp[k];
                k++;
            }

            l++;
        }

        while (j <= middle)
        {
            arr[l] = temp[j];
            l++;
            j++;
        }
    }

    /// <summary>
    /// Stwórz tablicę wypełnioną losowymi liczbami całkowitymi.
    /// </summary>
    /// <param name="length">Ilość liczb w tablicy.</param>
    /// <returns>Tablica z losowymi liczbami.</returns>
    private static int[] CreateArrayOfRandom(int length)
    {
        var rand = new Random();
        int[] array = new int[length];
        foreach (int i in Enumerable.Range(0, length))
        {
            array[i] = rand.Next(0, length);
        }

        return array;
    }
}
