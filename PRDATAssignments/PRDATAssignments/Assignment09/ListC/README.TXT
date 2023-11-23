Compiling and loading the list-C evaluator and parser (file ListC/README)
-------------------------------------------------------------------------


/Fart City

exercise 10.1 (i)

- ADD: takes the first two elements on the stack and adds them together, and pushes the result back on top of the stack
- CSTI pushes the constant integer i to the stack
- NIL pushes a reference to nil aka. nothing, whereas CSTI 0 pushes the integer 0 to the stack
- IFZERO takes the first element of the stack and checks if it is zero or a nil-reference. If this is the case it returns a TRUE flag.
- CONS allocates two-word-block on the heap, and pushes a reference to them to the stack.
- CAR accesses the first word in the CONS block
- SETCAR sets the first word in the CONS block.

Exercise 10.1 (ii)

ttttttttnnnnnnnnnnnnnnnnnnnnnngg

Paint:
- Aplying the paint operator with Paint(hdr, color), colors the garbage collection bits (gg) of the header to the color

Color:
- returns the color of the garbage collection bits (gg) of the header

Length:
- Returns the number (N) of words in the free block + 1 (the header word)

Exercise 10.1 (iii)
- the allocate (...) function is also called during cons instruction

Exercise 10.1 (iv)
- the allocate function calls tthe collect function if there is no free space to allocate



Archive listc.zip contains the files used below.


Building the list-C command line compiler listcc:

   ./fsharp/fslex --unicode ./ListC/CLex.fsl
   ./fsharp/fsyacc --module ./listC/CPar ./listC/CPar.fsy

   fsharpc --standalone -r ./fsharp/FsLexYacc.Runtime.dll ./ListC/Absyn.fs ./ListC/CPar.fs ./ListC/CLex.fs ./ListC/Parse.fs ./ListC/Machine.fs ./ListC/Comp.fs ./ListC/ListCC.fs -o listcc.exe
   mono listcc.exe ./ListC/ex30.lc

Using the list-C command line compiler to compile program ex30.lc
to listmachine code (in file ex30.out):

   ListCC ex30.lc
     or
   mono listcc.exe ./ListC/ex30.lc

./ListC/ListVM/ListVM/listmachine ./ListC/ex30.out 

The list-C virtual machine, as provided, should compile and run on
both Windows and Unix systems in both 32 and 64 bit.

A Visual Studio 19 project is provided for Windows. On Unix you simply
use gcc in the directory containing the C source files:
  listmachine.c
  utils_unix.c
  utils_win.c

Compiling on Windows open the Visual Studio project and use Build. You
can configure build for 32 or 64 bit. You locate the listmachine
executable and use that to run the compiled list-C programs.

On Unix you compile with gcc -o listmachine listmachine.c and use the
executable. You are of course not using the Visual Studio project
files on Unix and may delete them.

For instance on MacOS (Unix):

$ pwd
.../ListC/ListVM/ListVM
$ ll
total 40
...    256 Oct 27 10:54 .
...    192 Oct 27 10:44 ..
...   5708 Oct 27 10:44 ListVM.vcxproj
...    961 Oct 27 10:44 ListVM.vcxproj.filters
...    165 Oct 27 10:44 ListVM.vcxproj.user
...  15879 Oct 27 10:44 listmachine.c
...    816 Oct 27 10:44 utils_unix.c
...    965 Oct 27 10:44 utils_win.c
$ gcc -o listmachine ListVM/ListVM/listmachine.c 
$ ll
...
...  17840 Oct 27 10:54 listmachine
...  15879 Oct 27 10:44 listmachine.c
...    816 Oct 27 10:44 utils_unix.c
...    965 Oct 27 10:44 utils_win.c
$ ./listmachine 
listmachine for 64 bit architecture compiled with GCC
Usage: listmachine [-trace] <programfile> <arg1> ...
$ 

Run examples (assuming listmachine is a symbolic link to
ListVM/ListVM/listmachine.exe

How to run:

   ./fsharp/fslex --unicode CLex.fsl
   ./fsharp/fsyacc --module CPar CPar.fsy

   fsharpc --standalone -r ./fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ListCC.fs -o listcc.exe
   mono listcc.exe ex30.lc
   
   ./listmachine ex30.out 1000

 
