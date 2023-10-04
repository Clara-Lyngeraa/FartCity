Compiling and loading the micro-ML evaluator and parser (Fun/README.TXT)
------------------------------------------------------------------------

The items A to C are similar to the code found under folder Fun handed
out last week. In this week we extend with higher order functions,
items D-E and type interence, item F.

A. Loading the micro-ML evaluator, with abstract syntax only

   fsharpi Absyn.fs Fun.fs

   open Absyn;;
   open Fun;;
   let res = run (Prim("+", CstI 5, CstI 7));;
   #q;;


B. Generating and compiling the lexer and parser, and loading them:

   fsyacc --module FunPar FunPar.fsy
   fslex --unicode FunLex.fsl
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs   

   open Parse;;
   let e1 = fromString "5+7";;
   let e2 = fromString "let y = 7 in y + 2 end";;
   let e3 = fromString "let f x = x + 7 in f 2 end";;


C. Using the lexer, parser and first-order evaluator together:

   fsyacc --module FunPar FunPar.fsy
   fslex --unicode FunLex.fsl
   fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs Fun.fs ParseAndRun.fs

   open ParseAndRun;;
   run (fromString "5+7");;
   run (fromString "let y = 7 in y + 2 end");;
   run (fromString "let f x = x + 7 in f 2 end");;


D. Loading the evaluator for a higher-order functional language (same
   abstract syntax as the first-order language):

   fsharpi Absyn.fs HigherFun.fs

   open HigherFun;;
   eval ex1 [];;
   open Absyn;;
   run (Letfun ("twice", "f",
                Letfun ("g", "x", Call (Var "f", Call (Var "f", Var "x")), Var "g"),
                        Letfun ("mul3", "z", Prim ("*", Var "z", CstI 3),
                                Call (Call (Var "twice",Var "mul3"),CstI 2))));;

   (The above abstract syntax term corresponds to the concrete syntax
   term shown in point E below).


E. Using the lexer, parser and higher-order evaluator together:

   fsyacc --module FunPar FunPar.fsy
   fslex --unicode FunLex.fsl
   fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs HigherFun.fs ParseAndRunHigher.fs

   Our way of running
   ./fsharp/fsyacc --module FunPar FunPar.fsy
   ./fsharp/fslex --unicode FunLex.fsl
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs HigherFun.fs ParseAndRunHigher.fs

   

   open ParseAndRunHigher;;
   
   FartCity Exercise 6.1

   run (fromString @"let add x = let f y = x+y in f end
                     in add 2 5 end");;
   val it : HigherFun.value = Int 7

   
   run (fromString @"let add x = let f y = x+y in f end
                     in let addtwo = add 2
                     in addtwo 5 end
                     end");;
   val it : HigherFun.value = Int 7

   run (fromString @"let add x = let f y = x+y in f end
                     in let addtwo = add 2
                        in let x = 77 in addtwo 5 end
                        end
                     end");;
   val it : HigherFun.value = Int 7

   run (fromString @"let add x = let f y = x+y in f end
                     in add 2 end");;
   
   val it : HigherFun.value =
   Closure
      ("f", "y", Prim ("+", Var "x", Var "y"),
      [("x", Int 2);
         ("add",
         Closure
            ("add", "x", Letfun ("f", "y", Prim ("+", Var "x", Var "y"), Var "f"),
            []))])
   
   Theh result of the third expression is as expcted, since we call addTwo with 5.

   We have function add and function f which both is declared polymorphic, but in the add 2 we say that add takes an int. 
   Since we give no argument to the function f we create a closure for the function f, that in its 
   enviroment has the closure for the function with the name "add". This means to evaluate f in this environment


   Exercise 6.2
   run (fromString @"fun x -> x * 2");;
   val it : HigherFun.value = Clos ("x", Prim ("*", Var "x", CstI 2), [])


   Exercise 6.3

   open ParseAndRunHigher;;
   run (fromString @"let add x = fun y -> x+y
                     in add 2 5 end");;

   run (fromString @"let add = fun x -> fun y -> x+y
                     in add 2 5 end");;



   

F. Using the lexer, parser and polymorphic type inference together:

   fsyacc --module FunPar FunPar.fsy
   fslex --unicode FunLex.fsl
   fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs TypeInference.fs ParseAndType.fs

   open ParseAndType;;
   inferType (fromString "let f x = 1 in f 7 + f false end");;
