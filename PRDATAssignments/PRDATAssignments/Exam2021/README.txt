Opgave 1
1. Forklar betydningen af følgende konstruktioner Block, Dec, Stmt, Expr, Assign, Access og AccVar i ovenstående abstrakte syntaks
Block: Block er et stykke kode der skal køres f.eks. i en while loop / if statement
Dec: Declaration er erklæring af lokal variabel. F.eks. int foo;
Stmt: Statement kan være if-, while-, return-, expression- eller blockstatement
Expr: Expression kan være alt fra 
- acces `i`
- assign `i=x`
- en int konstant `0`
- operation `2+2`
- Call `foo(x)`

Assign: Assign er en expression, når man f.eks. sætter en variabel til en ny værdi
AccVar: AccVar er kun access variabel. Så f.eks. x = y, y++, osv

2. Angiv hvilke dele af ovenstående abstrakte syntaks der svarer til henholdsvis int x; x=1; og (x<y?x:y)=4; i programmet exam01.c.
- int x; [Dec (TypI,"x");
- x=1; Stmt (Expr (Assign (AccVar "x",CstI 1)));
- Stmt(Expr(Assign(CondExpAccess(Prim2 ("<",Access (AccVar "x"),Access (AccVar "y")),AccVar "x",AccVar "y"),CstI 4)));

3. 
To run
./fsharp/fslex --unicode CLex.fsl
./fsharp/fsyacc --module CPar CPar.fsy
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs

open ParseAndRun;;
open Absyn;;
fromFile "ex1.c";;

5. 
To run
./fsharp/fslex --unicode CLex.fsl
./fsharp/fsyacc --module CPar CPar.fsy
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs   

open ParseAndComp;;
open Absyn;;
compileToFile (fromFile "exam02.c") "exam02.out";;
compile "exam01";;

compile "exam01";;
val it : Machine.instr list =
  [LDARGS; CALL (0, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 0; ADD;
   CSTI 1; STI; INCSP -1; INCSP 1; GETBP; CSTI 1; ADD; CSTI 2; STI; INCSP -1;
   GETBP; CSTI 0; ADD; LDI; GETBP; CSTI 1; ADD; LDI; LT; IFZERO "L2"; GETBP;
   CSTI 0; ADD; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; Label "L3"; CSTI 3;
   STI; INCSP -1; GETBP; CSTI 0; ADD; LDI; GETBP; CSTI 1; ADD; LDI; LT;
   IFZERO "L4"; GETBP; CSTI 0; ADD; GOTO "L5"; Label "L4"; GETBP; CSTI 1; ADD;
   Label "L5"; CSTI 4; STI; INCSP -1; GETBP; CSTI 0; ADD; LDI; PRINTI;
   INCSP -1; CSTI 10; PRINTC; INCSP -1; GETBP; CSTI 1; ADD; LDI; PRINTI;
   INCSP -1; CSTI 10; PRINTC; INCSP -1; INCSP -2; RET -1]

javac Machine.java
java Machine exam01.out 
3 
4 

Ran 0.008 seconds

6. 
void main() {
    int x[1]; x[0] = 1;
    int y[1]; y[0] = 2;
    (x[0] < y[0] ? x[0] : y[0]) = 3;    
    (x[0] < y[0] ? x[0] : y[0]) = 4;
    print x[0]; println; // Expected 3
    print y[0]; println; // Expected 4
}
To run
./fsharp/fslex --unicode CLex.fsl
./fsharp/fsyacc --module CPar CPar.fsy
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs   

open ParseAndComp;;
open Absyn;;
compileToFile (fromFile "exam02.c") "exam02.out";;
compile "exam02";;

javac Machine.java
java Machine exam02.out 

Opgave 2
To run
./fsharp/fslex --unicode CLex.fsl
./fsharp/fsyacc --module CPar CPar.fsy
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs
#fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs

open ParseAndRun;;
open Absyn;;
fromFile "exam04.c";;

3. 
./fsharp/fslex --unicode CLex.fsl
./fsharp/fsyacc --module CPar CPar.fsy
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs   

open ParseAndComp;;
open Absyn;;
compileToFile (fromFile "exam04.c") "exam04.out";;
compileToFile (fromFile "exam04.c") "exam04.out";;
val it : Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 0; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; LDI;
   PRINTI; INCSP -1; GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD; LDI; CSTI 1; ADD;
   STI; INCSP -1; INCSP 0; Label "L3"; GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0;
   ADD; LDI; LT; IFNZRO "L2"; INCSP -1; RET 0]

javac Machine.java
java Machine exam04.out 10
0 1 2 3 4 5 6 7 8 9 
Ran 0.009 seconds

4.
./fsharp/fslex --unicode CLex.fsl
./fsharp/fsyacc --module CPar CPar.fsy
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs   

open ParseAndComp;;
open Absyn;;
compileToFile (fromFile "exam05.c") "exam05.out";;
> compileToFile (fromFile "exam05.c") "exam05.out";;
val it : Machine.instr list =
  [INCSP 1; LDARGS; CALL (1, "L1"); STOP; Label "L1"; CSTI 0; CSTI 0; STI;
   INCSP -1; GOTO "L3"; Label "L2"; CSTI 0; LDI; PRINTI; INCSP -1; CSTI 0;
   CSTI 0; LDI; CSTI 1; ADD; STI; INCSP -1; INCSP 0; Label "L3"; CSTI 0; LDI;
   GETBP; CSTI 0; ADD; LDI; LT; IFNZRO "L2"; INCSP 0; RET 0]

javac Machine.java
java Machine exam05.out 10
0 1 2 3 4 5 6 7 8 9 
Ran 0.009 seconds

Opgave 3
./fsharp/fslex --unicode CLex.fsl
./fsharp/fsyacc --module CPar CPar.fsy

fsharpc --standalone -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ListCC.fs -o listcc.exe
mono listcc.exe ex30.lc

Opgave 4
fsharpi Absyn.fs Fun.fs

open Absyn;;
open Fun;;
let exam01 = Letfun("f1", "x", Prim("+", Var "x", CstD 1.0),Call(Var "f1", CstD 12.0));;
let exam01 = Letfun("f1", "x", Prim("+", Var "x", CstD 1.0),Call(Var "f1", CstD 12.0));;
val exam01 : expr =
  Letfun
    ("f1", "x", Prim ("+", Var "x", CstD 1.0), Call (Var "f1", CstD 12.0))



b.
./fsharp/fsyacc --module FunPar FunPar.fsy
./fsharp/fslex --unicode FunLex.fsl
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs HigherFun.fs ParseAndRunHigher.fs
open Absyn;;
open HigherFun;;
run (Prim("+", CstD 23.0, CstI 1));;  

3.
./fsharp/fsyacc --module FunPar FunPar.fsy
./fsharp/fslex --unicode FunLex.fsl
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs HigherFun.fs ParseAndRunHigher.fs
open Absyn;;
open HigherFun;;
run (Let("x",CstD 23.0, Prim("*",CstI 2,Prim1("toInt", Var "x"))));; 