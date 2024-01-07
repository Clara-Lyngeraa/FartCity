# Exam 2019

## Opgave 1
To run

Goto Desktop/ITU/5.Semester/ProgDat/FartCity/PRDATAssignments/PRDATAssignments/Exam2019_jan/Cont

fsharpi Icon.fs

open Icon;;

run iconEx1;;

### 1
let iconMySolution1 = Every(Write(Prim("<",CstI 7,FromTo(1,10))));
I used the Every expr, which prints every number in the interval (WIP)

### 2
let iconEx2 = Every(Write(And(FromTo(1,4),
                            And(Write (CstS "\n"),FromTo(1,4)))));

The first And statement wrties a new line and 1 to 4
The second And writes the first line writes 1 to 4 1 to four times
Its an Every

### 3
Find (pat, str) ->
      let rec aux (i: int) =
        match str.IndexOf(pat, i) with
        | -1 -> econt()
        | index ->
            cont (Int index) (fun () -> aux (i+1))
      aux 0


### 4

let str1 = "Hi there - there is somthing there and i need to find their";
let iconMySolution31 =  (Write(Find("there",str1)));
let str2 = ""
let iconMySolution32 =  (Write(Find("there",str2)));
let str3 = "I need to find i and i want to know i can find it";
let iconMySolution33 =  (Write(Find("i",str3)));

### 5
let iconMySolution4 =  Every(Write(Prim("<", CstI 10,Find("e",str4))));

## Opgave 2

To run:
./fsharp/fsyacc --module FunPar FunPar.fsy
./fsharp/fslex --unicode FunLex.fsl
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs Parse.fs HigherFun.fs ParseAndRunHigher.fs

run (fromString @"let x = { } in x end");;


### 1

let x = { } in x end (* ex1 *)

let x = {field1 = 32} in x.field1 end (* ex2 *)

Let ("x",Record [("field1", CstI 32)],Field (Var "x","field1"))

let x = {field1 = 32; field2 = 33} in x end (* ex3 *)

Let ("x", Record [("field1", CstI 32); ("field2", CstI 33)], Var "x" )

let x = {field1 = 32; field2 = 33} in x.field1 end (* ex4 *)

Let ("x", Record [("field1", CstI 32); ("field2", CstI 33)], Field (Var "x", "field1"))

let x = {field1 = 32; field2 = 33} in x.field1+x.field2 end (* ex5 *)

Let ("x", Record [("field1", CstI 32); ("field2", CstI 33)], Prim("x",(Field (Var "x", "field1")), (Field(Var "x", "field2"))))


### 2 

FunLex.fsl:

  | '.'             { DOT }
  | '{'             { LBRACE }
  | '}'             { LBRACE }
  | ';'             { SEMICOLON}


FunPar.fsy

  | NAME DOT NAME                       { Field($1,$3)           }
  | LBRACE Record RBRACE                { Record $2              }


## Opgave 4
To run:
./fsharp/fslex --unicode CLex.fsl     
./fsharp/fsyacc --module CPar CPar.fsy    
fsharpi -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Interp.fs ParseAndRun.fs