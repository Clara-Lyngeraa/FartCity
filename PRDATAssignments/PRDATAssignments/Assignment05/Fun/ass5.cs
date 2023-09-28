class Test {

static void Main (string [] args){
    Hej(3);
    int[] lst1 = {3,5,12};
    int[] lst2 = {2,3,4,7};
    var res = Merge(lst1,lst2);
    foreach (int i in res){
        Console.WriteLine(i);
    }
}

    static int Hej (int i ){
        return i;
    }
    static int[] Merge(int[] lst1, int[] lst2){
        int [] finalList = new int[lst1.Length+lst2.Length];
        var res = lst1.Concat(lst2).ToArray();

        int i = 0;
        int j = 0;
        int current = 0;
        while (i < lst1.Length && j < lst2.Length)
        {
            var x = lst1[i];
            var y = lst2[j];
            if (x <= y) {
                finalList[current] = x;
                current++;
                i++;
            } else if (x > y) {
                finalList[current] = y;
                current++;
                j++;
            } 
        }
        if (i != lst1.Length)
        {
            for (int k = i; k < lst1.Length; k++)
            {
                finalList[current] = lst1[k];
                current++;
            }
        } else if (j != lst2.Length)
        {            
            for (int k = j; j<lst2.Length; k++)
            {
                finalList[current] = lst2[k];
                current++;
            }}
        return finalList;
    }
}