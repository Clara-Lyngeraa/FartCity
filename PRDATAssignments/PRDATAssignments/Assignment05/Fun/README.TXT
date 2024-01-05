Compiling and loading the micro-ML evaluator and parser (Fun/README.TXT)
------------------------------------------------------------------------

The items A to C are similar to the code found under folder Fun handed
out last week. In this week we extend with higher order functions,
items D-E and type interence, item F.


EXAM JAN 2019:



exercise 2:

run (fromString @"let x = { } in x end");;


EXAM DEC 2019:

   ./fsharp/fsyacc --module FunPar FunPar.fsy
   ./fsharp/fslex --unicode FunLex.fsl
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs HigherFun.fs ParseAndRunHigher.fs


Examples:

   open Absyn;;
   InCheck(Var "x", Prim("+",CstI 2, CstI 3), CstI 40);;

   open ParseAndRunHigher;;
   fromString @"let x = 23 in x within [2+3,40] end";;

   run (fromString @"let x = 23 in x within [2+3,40] end");;



A. Loading the micro-ML evaluator, with abstract syntax only

   fsharpi Absyn.fs Fun.fs 

   open Absyn;;
   open Fun;;
   let res = run (Prim("+", CstI 5, CstI 7));;
   #q;;


B. Generating and compiling the lexer and parser, and loading them:

   .fsharp/fsyacc --module FunPar FunPar.fsy
   .fsharp/fslex --unicode FunLex.fsl
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

   ./fsharp/fsyacc --module FunPar FunPar.fsy
   ./fsharp/fslex --unicode FunLex.fsl
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs TypeInference.fs ParseAndType.fs

  
   open ParseAndType;;
   inferType (fromString "let f x = 1 in f 7 + f false end");;

FartCity exercise 6.5

  > inferType (fromString "let f x = 1 in f f end");;
   val it : string = "int"

   1)
   inferType (fromString "let f g = g g in f end");;
   System.Exception: type error: circularity...

   2)
   This error was produced since there are recursive type dependencies 
   that cannot be resolved: g calls itself with g

   3) 
   > inferType (fromString "let f x = let g y = y in g false end in f 42 end");;
   val it : string = "bool"

   4)
   > inferType (fromString "let f x = let g y = if true then y else x in g false end in f 42 end" );;- 
   System.Exception: type error: bool and int...
   
   This error was produced because we are trying to call the f function with an int (42), 
   but the g functions tries to return both a boolean (then y) and an integer (else x);

   5)
   > inferType (fromString "let f x = let g y = if true then y else x in g false end in f true end" )- ;;
   val it : string = "bool"

   (2)
   > inferType (fromString "let f x = if x then true else false in f end");;
   val it : string = "(bool -> bool)"

   fra forelæsning: 
   "fun x -> x = true"
   
   > inferType (fromString "let f x = if x = 1 then 1 else 0 in f end");;
   val it : string = "(int -> int)"
   fra forelæsning: 
   "fun x -> x+1"
   
   > inferType (fromString "let f x = let g y = if y + x = 1 then 1 else 0 in g end in f end");;
   val it : string = "(int -> (int -> int))"
   fra forelæsning: 
   "fun x -> fun y -> x + y"

   > inferType (fromString "let f x = let g y = x in g end in f end");;
   val it : string = "('h -> ('g -> 'h))"
   fra forelæsning: 
   ""


   > inferType (fromString "let f x = let g y = y in g end in f end");;
   val it : string = "('g -> ('h -> 'h))"


   inferType (fromString "let f x = let g y = let k z = y (x z) in k end in g end in f end");;

   > inferType (fromString "let f x = let g = f x in g end in f end");;
   val it : string = "('e -> 'f)"
   
   > inferType (fromString "let f x = f 1 in f 1 end");;
   val it : string = "'e"
