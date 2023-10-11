Compiling and loading the micro-ML evaluator and parser (Fun/README.TXT)
------------------------------------------------------------------------

A. Loading the micro-ML evaluator, with abstract syntax only

   fsharpi Absyn.fs Fun.fs

   open Absyn;;
   open Fun;;
   let res = run (Prim("+", CstI 5, CstI 7));;
   val res : int = 12

B. Generating and compiling the lexer and parser, and loading them:

   ./fsharp/fsyacc --module FunPar FunPar.fsy
   ./fsharp/fslex --unicode FunLex.fsl
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs


   open Parse;;
   let e1 = fromString "5+7";;
   let e2 = fromString "let y = 7 in y + 2 end";;
   let e3 = fromString "let f x = x + 7 in f 2 end";;


C. Using the lexer, parser and first-order evaluator together:

   fsyacc --module FunPar FunPar.fsy
   fslex --unicode FunLex.fsl
   fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs Fun.fs ParseAndRun.fs

   Our way of running
   ./fsharp/fsyacc --module FunPar FunPar.fsy
   ./fsharp/fslex --unicode FunLex.fsl
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs Fun.fs ParseAndRun.fs

   open ParseAndRun;;
  
   Exercise 4.1 Fartcity: 
   Running the examples: 

   > run (fromString "5+7");;
   val it : int = 12

   > run (fromString "let y = 7 in y + 2 end");;
   val it : int = 9
   
   > run (fromString "let f x = x + 7 in f 2 end");;
   val it : int = 9

D. Fart City: Exercise 4.2
   run (fromString "let sum n = if n = 0 then 0 else n + sum(n-1) in sum 1000 end");;
   run (fromString "let f x =  if x = 0 then 1 else 3 * (f(x-1)) in f 8 end");;
   run (fromString "let f x = if x = 0 then 1 else 3 * (f(x-1)) 
                           in let g x = if x = 0 then 1 else (f(x)) + (g(x-1))
                           in g 11 end
                           end");;
   run (fromString "let f x = x*x*x*x*x*x*x*x
                           in let g x = if x = 1 then x else (f(x-1))
                           in g 10 end
                           end");;

   
   


