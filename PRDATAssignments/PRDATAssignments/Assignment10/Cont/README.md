# Assignment 10 FartCity

## Exercise 11.1

### (i)

```fsharp
let rec lenc xs c =
    match xs with
    | [] -> c 0
    | x::xr -> lenc xr (fun r -> c(r + 1));;


> lenc [2; 5; 7] id;;
    val it : int = 3

> lenc [2; 5; 7] (printf "The answer is ’% d’ \n");;
    The answer is ’ 3’
    val it : unit = ()
```

### (ii)

Calling the function with the continuation `(fun v -> 2*v)`
would result in getting twice the length.

### (iii)

```fsharp
let rec leni xs acc =
    match xs with
    | [] -> acc
    | x::xr -> leni xr (acc + 1);
```

The relation between `lenc` and `leni` is that they are both tail recursive function using heap space instead of using the stack to add computations left of each recursive call to return to.

## Exercise 11.2

### (i)

```fsharp
let rec revc xs c =
    match xs with
    | [] -> c []
    | x :: xr -> revc xr (fun r -> c(r @ [x]))

> revc [1;2;3] id;;
val it : int list = [3; 2; 1]
```

### (ii)

You would append the result of the function with the result of the function so:

```fsharp
> revc [1;2;3] (fun v -> v @ v);;
val it : int list = [3; 2; 1; 3; 2; 1]
```

### (iii)

```fsharp
let rec revi xs acc=
    match xs with
    | [] -> acc
    | x :: xr -> revi xr (x :: acc)

> revi [1;2;3] [];;
val it : int list = [3; 2; 1]

```

## Exercise 11.3

```fsharp
let rec prodc xs c =
    match xs with
    |[] -> c 1
    | x :: xr -> prodc xr (fun r -> c(r * x))

> prodc [1;2;3] id;;
val it : int = 6

```

## Exercise 11.4

```fsharp
let rec prodcOpt xs c =
    match xs with
    |[] -> c 1
    | x :: _ when x = 0 -> c 0
    | x :: xr -> prodc xr (fun r -> c(r * x))
```

As soon as we encounter a 0 end the computation with 0.

```fsharp

let rec prodi xs acc =
    match xs with
    |[] -> acc
    | x :: _ when x = 0 -> 0
    | x :: xr -> prodi xr (acc * x);;

> prodi [1;0] 1;;
val it : int = 0

> prodi [2;3] 1;;
val it : int = 6

```

## Exerice 11.8

```fsharp
> open Icon
- run (Every(Write(Prim("*", CstI 2, FromTo(1, 4)))));;
2 4 6 8 val it : Icon.value = Int 0

```

### (i)

```fsharp
> run (Every(Write(Prim("+",Prim("*", CstI 2, FromTo(1, 4)), CstI 1))));;
3 5 7 9 val it : value = Int 0

> run (Every(Write(Prim("+", Prim("*", CstI 10, FromTo(2,4)), FromTo(1,2)))));;
21 22 31 32 41 42 val it : value = Int 0
```

### (ii)

let ex8 = Write(Prim("<", CstI 4, FromTo(1, 10)));

```fsharp
> run (Write(Prim("<", CstI 50, Prim("*", CstI 7, FromTo(1,10)))));;
56 val it : value = Int 56

```

### (iii)

In Icon.fs eval

```fsharp
| Prim1(str, ex) -> //fartCity exercise 11.8
      eval ex (fun v1 -> fun econt1 ->
          match (str, v1) with
          | ("square", Int i1) -> cont (Int(i1*i1)) econt1
          | ("even", Int i1) ->
                if i1%2 = 0 then
                  (cont (Int i1) econt1)
                else
                  econt1 ()
          | _ -> Str "unknown prim1"

      ) econt


let ex10 = Every(Write(Prim1("square", FromTo(3,6))));
> run ex10;;
9 16 25 36 val it : Icon.value = Int 0

let ex11: expr = Every(Write(Prim1("even", FromTo(1,7))));
> run ex11;;
2 4 6 val it : Icon.value = Int 0

```

### (iv)

```fsharp
| Prim1(str, ex) -> //fartCity exercise 11.8
      eval ex (fun v1 -> fun econt1 ->
          match (str, v1) with
          | ("square", Int i1) -> cont (Int(i1*i1)) econt1
          | ("even", Int i1) ->
                if i1%2 = 0 then
                  (cont (Int i1) econt1)
                else
                  econt1 ()
          | ("multiples", Int i1) ->
            let rec con i =
                cont (Int(i1*i)) (fun () -> con (i + 1))
            con 1;
          | _ -> Str "unknown prim1"
        ) econt
```
