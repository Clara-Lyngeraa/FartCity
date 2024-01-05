Compiling and loading the micro-C evaluator and parser (MicroC/README.TXT)
--------------------------------------------------------------------------

Archive microc.zip contains the files used in points A, B and C below.


EXAM JAN 2019

   ./fsharp/fslex --unicode CLex.fsl
   ./fsharp/fsyacc --module CPar CPar.fsy
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs Machine.fs ParseAndRun.fs Comp.fs ParseAndComp.fs Contcomp.fs ParseAndContcomp.fs
            ||
      fsharpc --standalone -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Contcomp.fs MicroCC.fs -o microcc.exe

   gcc machine.c
   ./a.out ex11.out 8
   
   mono microcc.exe ex1.c
   ./machine ex1.out 3

EXAM DEC 2019

   ./fsharp/fslex --unicode CLex.fsl
   ./fsharp/fsyacc --module CPar CPar.fsy
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs Machine.fs ParseAndRun.fs Comp.fs ParseAndComp.fs   


Examples exe 2:

   ppTyp (TypP(TypA(TypI, None)));;
   ppTyp (TypP(TypA(TypP TypI, Some 3)));;
   ppTyp (TypP (TypP(TypA(TypP TypI, None))));;
   ppTyp (TypP (TypA( (TypP(TypA(TypP TypI, None))),Some 3)));;


Examples exe 3:

   ppVar (Glovar 47);;
   ppVar (Glovar 3);;
   ppVar (Locvar 13);;


Examples exe 4:

      open Absyn;;
      open Comp;;

      ppVarTyp ("a",(Locvar 11, TypA (TypI, Some 3)));;
      ppVarTyp ("g",(Glovar 0, TypI));;

      ppVarTyp ("z",(Locvar 11, TypA (TypI, None)));;
      ppVarTyp ("q",(Glovar 47, TypP (TypI)));;

Examples exe 5:
      open Absyn;;
      open Comp;;
      let exVarEnv : varEnv =
         ([("a", (Locvar 6, TypA (TypI,Some 2)));
            ("pn", (Locvar 3, TypA (TypP TypI,Some 1)));
            ("p", (Locvar 1, TypP TypI));
            ("n", (Locvar 0, TypI));
            ("g", (Glovar 0, TypI))], 7);;


      ppVarEnv exVarEnv;;

A. Generating and compiling the lexer and parser for micro-C, and
   loading the interpreter for micro-C.

   ./fsharp/fslex --unicode CLex.fsl
   ./fsharp/fsyacc --module CPar CPar.fsy
   fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs

   open ParseAndRun;;
   fromFile "ex1.c";;
   run (fromFile "ex1.c") [17];;
   run (fromFile "ex5.c");;
   run (fromFile "ex11.c") [8];;


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
