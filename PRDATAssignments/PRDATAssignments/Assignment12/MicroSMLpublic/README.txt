>Compiling and loading the micro-SML compiler (MicroSML/README.TXT)
---------------------------------------------------------

Archive microsml.zip contains the files used below

A. Building the micro-SML command line compiler microsmlc:

    ./fsharp/fslex --unicode FunLex.fsl
    ./fsharp/fsyacc --module FunPar FunPar.fsy    

   fsharpc --standalone -r:./fsharp/FsLexYacc.Runtime.dll Absyn.fs FunPar.fs FunLex.fs TypeInference.fs HigherFun.fs Machine.fs Contcomp.fs ParseTypeAndRun.fs MicroSMLC.fs -o microsmlc.exe

Compiling the test program test.sml:
  mono microsmlc.exe test.sml

Compiling the test program with tail call and other optimizations:
  mono microsmlc.exe -opt test.sml

Compiling AND evaluating the program with the evaluator in
HigherFun.fs:
  mono microsmlc.exe -eval test.sml

Compiling AND output intermediate AST's
  mono microsmlc.exe -verbose test.sml

The command line parameters can be combined.

C. Building the virtual machine

  The virtual machine is in the Visual Studio Project MsmlVM.

  On Unix / Mac:
    Go to directory MsmlVM/src where you find the C file
    msmlmachine.c
    
    gcc -Wall msmlmachine.c -o msmlmachine

  On Windows:
    You can use the Visual Studio 2019 solution MsmlVM.sln and
    compile for 32 or 64 bit.

Running the compiled test program on Mac:

  ./MsmlVM/src/msmlmachine test.out

The command line parameter -trace will write trace information on
stdout. The parameter -silent will supress garbage collection output
etc.

D. To replace the continuation based compiler with the simpler forward
compiler, simply replace Contcomp.fs with Comp.fs above.
