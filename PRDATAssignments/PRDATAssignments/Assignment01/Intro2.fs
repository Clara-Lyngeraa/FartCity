(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

//module Intro2

(* Association lists map object language variables to their values *)
module Intro2

open System.Formats.Asn1
let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;
let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;


(* Object language expressions with variables *)
type aexpr = 
  | CstI of int
  | Var of string
  | Add of  aexpr * aexpr
  | Mul of  aexpr * aexpr
  | Sub of  aexpr * aexpr

// type expr = 
//   | CstI of int
//   | Var of string
//   | Prim of string * expr * expr
//   | If of expr * expr * expr;;
  
let rec fmt (a: aexpr) =
    match a with
    | CstI (i) -> string i
    | Var s -> s
    | Add (a1, a2) ->
        let newA1 = fmt a1
        let newA2 = fmt a2
        "(" + newA1 + " + " + newA2 + ")"
    | Mul (a1, a2) ->
        let newA1 = fmt a1
        let newA2 = fmt a2
        "(" + newA1 + " * " + newA2 + ")"
    | Sub (a1, a2) ->
        let newA1 = fmt a1
        let newA2 = fmt a2
        "(" + newA1 + " - " + newA2 + ")"

let rec simplify a : aexpr =
    match a with
    | CstI i -> CstI i
    | Var s -> Var s
    | Add (a1, a2) ->
        match a1 with
        | CstI 0 -> simplify a2
        | _ ->
            match a2 with
            | CstI 0 -> simplify a1
            | _ -> Add(simplify a1, simplify a2)
        
    | Mul (a1, a2) ->
        match a1 with
        | CstI 0 -> CstI 0
        | CstI 1 -> simplify a2
        | _ ->
            match a2 with
            | CstI 0 -> CstI 0
            | CstI 1 -> simplify a1
            | _ -> Mul(simplify a1, simplify a2)
            
    | Sub (a1, a2) ->
        match a1 = a2 with
        | true -> CstI 0
        | false ->
            match a1 with
            | CstI 0 -> simplify a2
            | _ ->
                match a2 with
                | CstI 0 -> simplify a1
                | _ -> Sub(simplify a1, simplify a2)


let rec symDif a (x : string ): aexpr =
    match a with
    | CstI i -> CstI 0
    | Var s ->
        if s = x
        then CstI 1
        else CstI 0
    | Add (a1, a2) ->
        Add(symDif a1 x, symDif a2 x)
    | Sub (a1, a2) ->
        Sub(symDif a1 x, symDif a2 x)
    | Mul (a1, a2) ->
        Add(Mul(symDif a1 x, a2), Mul(a1, symDif a2 x))

// let e1 = CstI 17;;
//
// let e2 = Prim("+", CstI 3, Var "a");;
//
// let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;
//
// let e4 = Prim("max", CstI 3, CstI 9);;
// let e5 = Prim("min", CstI 3, CstI 9);;
// let e6 = Prim("==", CstI 3, CstI 3);;
// let e7 = If(Var "a", CstI 11, CstI 22)
//
//
// (* Evaluation within an environment *)
//
// let rec eval e (env : (string * int) list) : int =
//     match e with
//     | CstI i            -> i
//     | Var x             -> lookup env x
//     | Prim(ope, e1, e2) ->
//         let i1 = eval e1 env
//         let i2 = eval e2 env
//         match ope with
//         | "+" -> i1 + i2
//         | "*" -> i1 * i2
//         | "-" -> i1 - i2
//         | "max" -> if i1 > i2 then i1 else i2
//         | "min" -> if i1 > i2 then i2 else i1
//         | "==" -> if i1 = i2 then 1 else 0
//         | _ -> failwith "unkown primitive"
//     | If(e1, e2, e3) ->
//         let i1 = eval e1 env
//         if i1 = 0
//         then eval e3 env
//         else eval e2 env
//         
// let e1v  = eval e1 env;;
// let e2v1 = eval e2 env;;
// let e2v2 = eval e2 [("a", 314)];;
// let e3v  = eval e3 env;;
// let e4v  = eval e4 env;;
// let e5v  = eval e5 env;;
// let e6v  = eval e6 env;;
// let e7v  = eval e7 env;;
//
// printfn ("%A") e4v
// printfn ("%A") e5v
// printfn ("%A") e6v
// printfn ("%A") e7v

// let e8 = Sub(Var "v", Add(Var "w", Var "z"))
// let e9 = Mul(CstI 2 , Sub(Var "v", Add(Var "w", Var "z")))
// let e10 = Add(Add(Var "x", Var "y"), Add(Var "z", Var "v"))

let a1 = Add(CstI 0, CstI 34)
let a2 = Add(Var "y", CstI 0)
let a3 = Sub(CstI 34, CstI 0)
let a4 = Mul(CstI 1, Var "y")
let a5 = Mul(CstI 34, CstI 1)
let a6 = Mul(CstI 0, CstI 1)
let a7 = Mul(Var "1", CstI 0) 
let a8 = Sub(CstI 5, CstI 5)

let a9 = Sub(CstI 5, Var "x") 
let a10 = Add(CstI 5, CstI 5) 
let a11 = Mul(Var "x", CstI 5) 

let a1v = simplify a1
let a2v = simplify a2
let a3v = simplify a3
let a4v = simplify a4
let a5v = simplify a5
let a6v = simplify a6
let a7v = simplify a7
let a8v = simplify a8
let a9v = symDif a9 "x"
let a10v = symDif a10 "y"
let a11v = symDif a11 "x"

printf "%A" (simplify a1)
printf "%A" (simplify a2)


