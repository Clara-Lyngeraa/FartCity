Compiling and loading the micro-C evaluator and parser (MicroC/README.TXT)
--------------------------------------------------------------------------

Archive microc.zip contains the files used in points A, B and C below.


A. Generating and compiling the lexer and parser for micro-C, and
   loading the interpreter for micro-C.

   fslex --unicode CLex.fsl
   fsyacc --module CPar CPar.fsy
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs

   open ParseAndRun;;

   run (fromFile "7_2_i.c") [];;
   run (fromFile "7_2_ii.c") [10];;
   run (fromFile "7_2_iii.c") [];;
   
   fromFile "ex1.c";;

   Exercise 7.1:

   > fromFile "ex1.c";;
   val it : Absyn.program =
   Prog
      [Fundec
         (None, "main", [(TypI, "n")],
         Block
            [Stmt
               (While
                  (Prim2 (">", Access (AccVar "n"), CstI 0),
                  Block
                     [Stmt (Expr (Prim1 ("printi", Access (AccVar "n"))));
                     Stmt
                        (Expr
                           (Assign
                              (AccVar "n",
                              Prim2 ("-", Access (AccVar "n"), CstI 1))))]));
            Stmt (Expr (Prim1 ("printc", CstI 10)))])]

   Declarations: Prog, Fundec, Stmt
   Satements: While, Block, Expr
   Types: TypI
   Expressions: CstI, AccVar, Assign, Access, Prim1, Prim2

   run (fromFile "ex1.c") [17];;
   > 17 16 15 14 13 12 11 10 9 8 7 6 5 4 3 2 1 
   val it : Interp.store = map [(0, 0)]

   run (fromFile "ex5.c");;
   > val it : (int list -> Interp.store) = <fun:Invoke@3236>

   run (fromFile "ex11.c") [8];;
   > val it : Interp.store =
   map
    [(0, 8); (1, 0); (2, 9); (3, -999); (4, 0); (5, 0); (6, 0); (7, 0); (8, 0);
     ...]


Exercise 7.2:

our way of running:
./fsharp/fslex --unicode CLex.fsl     
./fsharp/fsyacc --module CPar CPar.fsy    
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs

open ParseAndRun;;
(i)

> run (fromFile "7_2_i.c") [];;
29 val it : Interp.store =
  map
    [(-1, 29); (0, -1); (1, 7); (2, 13); (3, 9); (4, 8); (5, 1); (6, 3);
     (7, 1); ...]

(ii)
> run (fromFile "7_2_ii.c") [10];;
285 val it : Interp.store =
  map
    [(-1, 285); (0, -1); (1, 10); (2, 0); (3, 1); (4, 4); (5, 9); (6, 16);
     (7, 25); ...]

(iii)
>run (fromFile "7_2_iii.c") [];;
1 4 2 0 val it : Interp.store =
  map
    [(0, 1); (1, 2); (2, 1); (3, 1); (4, 1); (5, 2); (6, 0); (7, 0); (8, 1);
     ...]


Exercise 7.3 
> run (fromFile "7_3_i.c") [];;
29 val it : Interp.store =
  map
    [(-1, 29); (0, -1); (1, 7); (2, 13); (3, 9); (4, 8); (5, 1); (6, 3);
     (7, 1); ...]

> run (fromFile "7_3_ii.c") [10];;   
285 val it : Interp.store =
  map
    [(-1, 285); (0, -1); (1, 10); (2, 0); (3, 1); (4, 4); (5, 9); (6, 16);
     (7, 25); ...]

> run (fromFile "7_2_iii.c") [];;  
1 4 2 0 val it : Interp.store =
  map
    [(0, 1); (1, 2); (2, 1); (3, 1); (4, 1); (5, 2); (6, 0); (7, 0); (8, 1);
     ...] 



B. To compile and use the micro-C compiler

   fslex --unicode CLex.fsl
   fsyacc --module CPar CPar.fsy
   fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs   

   open ParseAndComp;;
   compileToFile (fromFile "ex11.c") "ex11.out";;
   compile "ex11";;
   #q;;

The Java stack machine:

   javac Machine.java
   java Machine ex11.out 8

The C stack machine
   gcc machine.c
   ./a.out ex11.out 8

C. To compile and use the backwards (continuation-based) micro-C compiler:

   fslex --unicode CLex.fsl
   fsyacc --module CPar CPar.fsy
   fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Contcomp.fs ParseAndContcomp.fs   

   open ParseAndContcomp;;
   contCompileToFile (fromFile "ex11.c") "ex11.out";;
   compile "ex11";;
   #q;;

   javac Machine.java
   java Machine ex11.out 8


D. Build the backwards micro-C compiler as a command-line program microcc

   fslex --unicode CLex.fsl
   fsyacc --module CPar CPar.fsy
   fsharpc --standalone -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Contcomp.fs MicroCC.fs -o microcc.exe

   mono microcc.exe ex11.c

   javac Machine.java
   java Machine ex11.out 8
