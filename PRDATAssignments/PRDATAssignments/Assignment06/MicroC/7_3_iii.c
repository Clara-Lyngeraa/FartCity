// Exercise 7.3 redo from 7_2 iii with for loops

void histogram(int n, int ns[], int max, int freq[])
{
    int i;
    i = 0;

    // runs from 0 to 3
    // go through the numbers from 0 to max looking for these in the array
    for (i = 0; i <= max; i = i + 1)
    {
        int countp;
        countp = 0;
        int j;
        j = 0;
        for (j = 0; j < n; j++) // go trough the array ns from 0 to n
        {
            if (ns[j] == i)
            {
                countp = countp + 1; // increment the counter if we found the number we were looking for
            }
            j = j + 1;
        }
        freq[i] = countp;
        i = i + 1;
    }
}

// main creates array [1,2,1,1,1,2,0]
void main()
{
    int arr[7];
    arr[0] = 1;
    arr[1] = 2;
    arr[2] = 1;
    arr[3] = 1;
    arr[4] = 1;
    arr[5] = 2;
    arr[6] = 0;

    int freq[4];
    int max;
    max = 3;
    int i;
    i = 0;

    histogram(7, arr, 3, freq);

    int t;
    t = 0;
    while (t <= max)
    {
        print freq[t];
        t = t + 1;
    }
}