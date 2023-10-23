// Exercise 7.3 redo from 7_2 ii with for loops
int *sump;

void squares(int n, int arr[])
{
    int sum;
    sum = 0;
    int i;
    for (i = 0; i < n; i = i + 1)
        arr[i] = i * i;
}

void main(int n)
{
    int arr[20];
    squares(n, arr);
    arrsum(10, arr, sump);
    print *sump;
}