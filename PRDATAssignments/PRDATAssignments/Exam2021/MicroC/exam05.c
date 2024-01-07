int i;

void main(int n) {
    i=0;
    alias j as i; // Make local variable j an alias of local variable i
    while (j < n) {
        print j;
        i=i+1;
    }
}