// FartCity exercise 7.2 (ii)
int *countp;
int *number[];

void getNElements(int n, int arr[], int *number[]) {
  int tempArr[7];
  int i; i=0;
  while (i<n) {
    tempArr[i]=arr[i]; 
    i=i+1;
  }
  *number=tempArr; 
}

void histogram(int n, int ns[], int max, int freq[]){
    int i; i=0;
    while (i<max) {
        countp=0;
        int j; j=0;
        while (j<n)
        {  
            if(ns[j]=i){
                countp = countp + 1;
            }
            j=j+1;
        }
        freq[i]=countp;
        i=i+1;
    }

}

void main() { 
    int arr[7];
    arr[0]=1;
    arr[1]=2;
    arr[2]=1;
    arr[3]=1;
    arr[4]=1;
    arr[5]=2;
    arr[6]=0;
    
    int freq[4];
    int max; max=3;
    int i; i = 0;

    while (i <= max) {
        freq[i] = 0;
        i = i + 1;
    }

    histogram(7, arr, 3, freq);
    while (i <= max) {
        print freq[i];
        print max;
        i = i + 1;
    }


}
