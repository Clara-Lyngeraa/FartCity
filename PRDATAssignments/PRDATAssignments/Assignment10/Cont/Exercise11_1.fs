//Assignment 10 FartCity

//Exercise 11.1
module Exercise10


//lenc : ’a list ->(int ->’b) ->’b
let rec lenc xs c =
    match xs with
    | [] -> c 0
    | x::xr -> lenc xr (fun r -> c(r + 1));;

(*
    > lenc [2; 5; 7] id;;
    val it : int = 3

    > lenc [2; 5; 7] (printf "The answer is ’% d’ \n");;
    The answer is ’ 3’ 
    val it : unit = ()
*)

//leni : int list -> int -> int
let rec leni xs acc =
    match xs with
    | [] -> acc
    | x::xr -> leni xr (acc + 1);

(*
    > leni [1;2;3;4;5] 0;;
    val it : int = 5

    > leni [] 0;;         
    val it : int = 0

*)

let rec rev xs =
    match xs with
    | [] -> []
    | x::xr -> rev xr @ [x];;

let rec revc xs c =
    match xs with
    | [] -> c []
    | x :: xr -> revc xr (fun r -> c(r @ [x]))


let rec revi xs acc=
    match xs with
    | [] -> acc
    | x :: xr -> revi xr (x :: acc)


//prodc : int list -> (int -> int) -> int
(*
    let rec prod xs =
    match xs with
    | [] -> 1
    | x::xr -> x * prod xr;;
*)

let rec prodc xs c = 
    match xs with 
    |[] -> c 1
    | x :: xr -> prodc xr (fun r -> c(r * x));;

let rec prodcOpt xs c = 
    match xs with 
    |[] -> c 1
    | x :: _ when x = 0 -> c 0
    | x :: xr -> prodc xr (fun r -> c(r * x))

let rec prodi xs acc = 
    match xs with 
    |[] -> acc 
    | x :: _ when x = 0 -> 0
    | x :: xr -> prodi xr (acc * x);;