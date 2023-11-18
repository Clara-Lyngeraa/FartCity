Compiling and loading continuation-based evaluators (cont/README.TXT)
---------------------------------------------------------------------

Archive cont.zip contains the files used below.

A. Loading two continuation-based interpreters for a functional
   language with exceptions:

   fsharpi Contfun.fs

   open Contfun;;
   eval1 ex1 [];; 
   val it : answer = Result 13

   eval1 ex2 [("n", Int 10)];;
   val it : answer = Result 3628800
   #q;;


B. Loading two continuation-based interpreters for an imperative
   language with exceptions:

   fsharpi Contimp.fs

   open Contimp;;
   > run1 ex1;;
   0
   1
   2
   3
   4
   5
   6
   val it : answer = Abort "Uncaught exception: seven"


   > run1 ex2;;
   1
   2
   3
   4
   5
   6
   7
   val it : answer = Abort "Uncaught exception: seven"

   > run2 ex3;;
   1
   2
   3
   4
   5
   6
   7
   222222
   333333
   val it : answer = Terminate


C. Loading a continuation-based interpreter for micro-Icon, a language
   in which an expression can have multiple results:

   fsharpi Icon.fs

   open Icon;;
   > run ex1;;
   1 Failed
   val it : value = Int 0

   > run ex2;;
   1 2 3 Failed
   val it : value = Int 0

   > run ex3and;;
   4 5 6 4 5 6 4 5 6 Failed
   val it : value = Int 0

   > run ex3or;;
   1 2 3 4 5 6 Failed
   val it : value = Int 0


D. Compile and run a Java implementation of factorial in
   continuation-passing style:

   javac Factorial.java
   > java Factorial 10
   3628800


E. Compile and run example illustrating longjmp and setjmp in C (under Unix):

   gcc testlongjmp.c -o testlongjmp
   > ./testlongjmp 10
   reached 0
   returning from f, i = 2
   returning from f, i = 4
   returning from f, i = 6
   returning from f, i = 8
   returning from f, i = 10

   > ./testlongjmp 11
   reached 1
   longjmp was called
