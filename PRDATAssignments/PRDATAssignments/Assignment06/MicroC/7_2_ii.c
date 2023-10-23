// FartCity exercise 7.2 (i) and (ii)
int *sump;

void arrsum(int n, int arr[], int *sump) {
  int sum; sum=0;
  int i; i=0;
  while (i<n) {
    sum=sum+arr[i]; 
    i=i+1;
  }
  *sump=sum; 
}

void squares(int n, int arr[]) {
  int sum; sum=0;
  int i; i=0;
  while (i<n) {
    arr[i]=i*i;
    i=i+1;
  }
}

void main(int n) { 
  int arr[20];
  squares(n, arr);
  arrsum(10, arr, sump); 
  print *sump; 
}
