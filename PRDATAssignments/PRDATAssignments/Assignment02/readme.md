## 3.2

regex: a?b?(b+a)\*

### NFA

![Exercise3.2NFA.png](Exercise3.2NFA.png)

### DFA

![Exercise3.2DFA.png](Exercise3.2DFA.png)

## 2.1

a) 0\*42

b) ((?!42)\d)\*

c) (0* 4[3-9])|0*[5-9][0-9]|0\*([1-9]\d{2,})

## 2.2

### NFA

![Exercise2.2NFA.png](Exercise2.2NFA.png)

### DFA

![Exercise2.2DFA.png](Exercise2.2DFA.png)

## HelloLex

### q.1
```['0'-'9']``` - it matches any single digit between 0 and 9.

### q.2 
using ``` mono ~/bin/fsharp/fslex.exe --unicode hello.fsl ```
- The additional file being generated is hello.fs
- 3 states are in the automaton of the lexer

### q.3

