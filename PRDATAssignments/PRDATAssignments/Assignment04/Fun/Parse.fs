(* Lexing and parsing of micro-ML programs using fslex and fsyacc *)

module Parse

open System
open System.IO
open System.Text
open (*Microsoft.*)FSharp.Text.Lexing
open Absyn

(* Plain parsing from a string, with poor error reporting *)

let fromString (str : string) : expr =
    let lexbuf = (*Lexing. insert if using old PowerPack *)LexBuffer<char>.FromString(str)
    try 
      FunPar.Main FunLex.Token lexbuf
    with 
      | exn -> let pos = lexbuf.EndPos 
               failwithf "%s near line %d, column %d\n" 
                  (exn.Message) (pos.Line+1) pos.Column
             
(* Parsing from a file *)

let fromFile (filename : string) =
    use reader = new StreamReader(filename)
    let lexbuf = (* Lexing. insert if using old PowerPack *) LexBuffer<char>.FromTextReader reader
    try 
      FunPar.Main FunLex.Token lexbuf
    with 
      | exn -> let pos = lexbuf.EndPos 
               failwithf "%s in file %s near line %d, column %d\n" 
                  (exn.Message) filename (pos.Line+1) pos.Column

(* Exercise it *)

let e1 = fromString "5+7";;
let e2 = fromString "let f x = x + 7 in f 2 end";;

(* Examples in concrete syntax *)

let ex1 = fromString 
            @"let f1 x = x + 1 in f1 12 end";;

(* Example: factorial *)

let ex2 = fromString 
            @"let fac x = if x=0 then 1 else x * fac(x - 1)
              in fac n end";;

(* Example: deep recursion to check for constant-space tail recursion *)

let ex3 = fromString 
            @"let deep x = if x=0 then 1 else deep(x-1) 
              in deep count end";;
    
(* Example: static scope (result 14) or dynamic scope (result 25) *)

let ex4 = fromString 
            @"let y = 11
              in let f x = x + y
                 in let y = 22 in f 3 end 
                 end
              end";;

(* Example: two function definitions: a comparison and Fibonacci *)

let ex5 = fromString
            @"let ge2 x = 1 < x
              in let fib n = if ge2(n) then fib(n-1) + fib(n-2) else 1
                 in fib 25 
                 end
              end";;

(*Fart City: Exercise 4.2*)
let rec sum n = 
    match n with
    | 1 -> 1
    | _ -> n + sum(n-1);;

let rec computePW =
    let b = 8
    let rec aux b acc =
      match b with
      | 1 -> acc
      | _ -> aux (b-1) acc*3
    aux b 3;;

let rec three num = 
      match num with
      | 0 -> 1
      | _ -> 3 * three (num-1);;

let computeSome =
    let rec aux num acc =
      match num with
      | 11 -> acc
      | _ -> aux (num+1) (acc+(three num))
    aux 0 0;;


let toThePowerOfEight num = 
    let rec aux eight acc = 
      match eight with
      | 8 -> acc
      | _ -> aux (eight + 1) (acc * num)
    aux 0 1

let copmuteOther = 
    let rec aux num acc =
      match num with
      | 10 -> acc
      | _ -> aux (num+1) (acc+(toThePowerOfEight num))
    aux 0 0