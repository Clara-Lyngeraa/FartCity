    ./fsharp/fslex --unicode FunLex.fsl
    ./fsharp/fsyacc --module FunPar FunPar.fsy    

   fsharpc --standalone -r:./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs TypeInference.fs HigherFun.fs Machine.fs Contcomp.fs ParseTypeAndRun.fs MicroSMLC.fs -o microsmlc.exe

    - Compiling the test program test.sml:
    $mono microsmlc.exe ex09.sml

   Micro-SML compiler v 1.1 of 2018-11-18
   Compiling ex09.sml to ex09.out
   
   Compiled to file ex09.out

    Compiling the test program with tail call and other optimizations:
   $mono microsmlc.exe -opt ex09.sml
 
    Micro-SML compiler v 1.1 of 2018-11-18
    Compiling ex09.sml to ex09.out

    Compiled to file ex09.out

    - Compiling AND evaluating the program with the evaluator in HigherFun.fs:
    $mono microsmlc.exe -eval ex09.sml

    Micro-SML compiler v 1.1 of 2018-11-18
    Compiling ex09.sml to ex09.out

    Evaluating Program
    4 
    Result value: Result (Int 4)
    Used: Elapsed 40ms, CPU 40ms
    Compiled to file ex09.out

    - Micro-SML compiler v 1.1 of 2018-11-18
    Compiling ex09.sml to ex09.out

    Evaluating Program
    4 
    Result value: Result (Int 4)
    Used: Elapsed 40ms, CPU 41ms
    Compiled to file ex09.out
 
    - Compiling AND output intermediate AST's
    $mono microsmlc.exe -verbose ex09.sml
    Micro-SML compiler v 1.1 of 2018-11-18
    Compiling ex09.sml to ex09.out

    Program after alpha conversion (exercise):
    fun f x = if (x < 0) then g 4 else f (x - 1)
    and g x = x
    begin
    print(f 2)
    end
    Program with types:
    fun f x = if (x:int < 0:int):bool then g:(int -> int)_tail 4:int:int else f:(int -> int)_tail (x:int - 1:int):int:int
    and g x = x:int
    begin
    print(f:(int -> int) 2:int:int):int
    end
    Result type: int

    Compiled to ex09.out
    LABEL G_ExnVar_L2
        0: CSTI 0
        2: CSTI 0
        4: STI
    LABEL G_Valdecs_L3
        5: ACLOS 1
        7: ACLOS 1
        9: PUSHLAB LabFunc_f_L4
        11: CSTI 1
        13: LDI
        14: HEAPSTI 1
        16: INCSP -1
        18: PUSHLAB LabFunc_g_L5
        20: CSTI 2
        22: LDI
        23: HEAPSTI 1
        25: INCSP -1
        27: GETSP
        28: CSTI 2
        30: SUB
        31: CALL 0 L1
        34: STI
        35: INCSP -3
        37: STOP
    LABEL LabFunc_f_L4
        38: GETBP
        39: CSTI 1
        41: ADD
        42: LDI
        43: CSTI 0
        45: LT
        46: IFZERO L7
        48: CSTI 2
        50: LDI
        51: CSTI 4
        53: CLOSCALL 1
        55: GOTO L6
    LABEL L7
        57: GETBP
        58: CSTI 0
        60: ADD
        61: LDI
        62: GETBP
        63: CSTI 1
        65: ADD
        66: LDI
        67: CSTI 1
        69: SUB
        70: CLOSCALL 1
    LABEL L6
        72: RET 2
    LABEL LabFunc_g_L5
        74: GETBP
        75: CSTI 1
        77: ADD
        78: LDI
        79: RET 2
    LABEL L1
        81: CSTI 1
        83: LDI
        84: CSTI 2
        86: CLOSCALL 1
        88: PRINTI
        89: RET 0


    Compiled to file ex09.out
 The command line parameters can be combined.


## ex 13.1
1.
ex09.out
Inde i MsmlVM folderen
./src/msmlmachine ../ex09.out 
4 
Result value: 4
Used 0 cpu milli-seconds

2.
Int

3.
fun f x = if (x:int < 0:int):bool then g:(int -> int)_tail 4:int:int else f:(int -> int)_tail (x:int - 1:int):int:int
and g x = x:int

The calls to g and f

4.
(f:(int -> int) 2:int:int):int
f is int->int
g is int->int

5.
Running with -eval
40ms

Running $./src/msmlmachine ../ex09.out 
4 
Result value: 4
Used 0 cpu milli-seconds

6.
No optimization
0 0 0 0 12 34 1 34 1 32 38 0 1 11 33 1 15 -1 32 74 0 2 11 33 1 15 -1 14 0 2 2 19 0 81 12 15 -3 25 13 0 1 1 11 0 0 7 17 57 0 2 11 0 4 35 1 16 72 13 0 0 1 11 13 0 1 1 11 0 1 2 35 1 21 2 13 0 1 1 11 21 2 0 1 11 0 2 35 1 22 21 0

With optimization
0 0 0 0 12 34 1 34 1 32 38 0 1 11 33 1 15 -1 32 67 0 2 11 33 1 15 -1 14 0 2 2 19 0 74 12 15 -3 25 13 0 1 1 11 0 0 7 17 55 0 2 11 0 4 38 1 13 11 13 0 1 1 11 0 1 2 38 1 13 0 1 1 11 21 2 0 1 11 0 2 35 1 22 21 0
Around 7

## 13.2

    
-------------------------------
rho |- fst (e1,e2) : (int,int)