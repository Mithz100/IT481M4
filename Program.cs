// Bubble Sort

using System;
using System.Diagnostics;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        // create lists
        int[] smallListBubble = GenerateRandomList(10);
        int[] mediumListBubble = GenerateRandomList(100);
        int[] largeListBubble = GenerateRandomList(1000);

        int[] smallListMerge = GenerateRandomList(10);
        int[] mediumListMerge = GenerateRandomList(100);
        int[] largeListMerge = GenerateRandomList(1000);

        // sort and measure time for each list using bubble sort
        SortAndMeasureTime(() => BubbleSort(smallListBubble), "Small List (Bubble)");
        SortAndMeasureTime(() => BubbleSort(mediumListBubble), "Medium List (Bubble)");
        SortAndMeasureTime(() => BubbleSort(largeListBubble), "Large List (Bubble)");

        // sort and measuer time for each list using merge sort
        SortAndMeasureTime(() => MergeSort(smallListMerge), "Small List (Merge)");
        SortAndMeasureTime(() => MergeSort(mediumListMerge), "Medium List (Merge)");
        SortAndMeasureTime(() => MergeSort(largeListMerge), "Large List (Merge)");
    }

    static int[] GenerateRandomList(int size)
    {
        using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            int[] list = new int[size];
            byte[] buffer = new byte[sizeof(int)];
            
            for(int i = 0; i < size; i++)
            {
                rng.GetBytes(buffer);
                int randomNumber = BitConverter.ToInt32(buffer, 0);
                list[i] = Math.Abs(randomNumber % 2000);
            }
            return list;
        }
    }

    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for(int i = 0; i < n - 1; i++)
        {
            for(int j = 0; j < n - 1; j++)
            {
                if(arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    static void MergeSort(int[] arr)
    {
        int[] temp = new int[arr.Length];
        MergeSort(arr, temp, 0, arr.Length - 1);
    }

    static void MergeSort(int[] arr, int[] temp, int left, int right)
    {
        if(left < right)
        {
            int mid = left + (right - left) / 2;
            MergeSort(arr, temp, left, mid);
            MergeSort(arr, temp, mid + 1, right);
            Merge(arr, temp, left, mid, right);
        }
    }

    static void Merge(int[] arr, int[] temp, int left, int mid, int right)
    {
        int i = left;
        int j = mid + 1;
        int k = left;

        while(i <= mid && j <= right)
        {
            if(arr[i] <= arr[j])
            {
                temp[k] = arr[i];
                i++;
            }
            else
            {
                temp[k] = arr[j];
                j++;
            }
            k++;
        }

        while(i <= mid)
        {
            temp[k] = arr[i];
            i++;
            k++;
        }

        while(j <= right)
        {
            temp[k] = arr[j];
            j++;
            k++;
        }

        for(k = left; k <= right; k++)
        {
            arr[k] = temp[k];
        }
    }

    static void SortAndMeasureTime(Action action, string listType)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        Console.WriteLine($"Time taken to sort {listType}: {stopwatch.ElapsedMilliseconds} milliseconds");
    }
}
