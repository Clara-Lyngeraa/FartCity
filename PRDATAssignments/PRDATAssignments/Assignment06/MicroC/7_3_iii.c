// Exercise 7.3 redo from 7_2 iii with for loops

// FartCity exercise 7.2 (ii)
int *countp;
int *number[];

void getNElements(int n, int arr[], int *number[])
{
    int tempArr[7];
    int i;
    for (i = 0; i < n; i = i + 1)
        tempArr[i] = arr[i];
    *number = tempArr;
}

void histogram(int n, int ns[], int max, int freq[])
{
    int i;
    for (i = 0; i < max; i = i + 1)
        countp = 0;
    int j;
    for (j = 0; j < n; j++)
        if (ns[j] = i)
        {
            countp = countp + 1;
        }
    freq[i] = countp;
    i = i + 1;
}

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

    for (i = 0; i <= max; i = i + 1)
        freq[i] = 0;

    histogram(7, arr, 3, freq);

    for (i = 0; i <= max; i = i + 1)
        print freq[i];
    print max;
}
