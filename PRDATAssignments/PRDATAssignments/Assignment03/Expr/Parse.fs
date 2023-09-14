(* File Expr/Parse.fs *)
(* Lexing and parsing of simple expressions using fslex and fsyacc *)

module Parse

open System
open System.IO
open System.Text
open FSharp.Text
open Absyn

(* Plain parsing from a string, with poor error reporting *)

let fromString (str : string) : expr =
    let lexbuf = Lexing.LexBuffer<char>.FromString(str)
    try 
      ExprPar.Main ExprLex.Token lexbuf
    with 
      | exn -> let pos = lexbuf.EndPos 
               failwithf "%s near line %d, column %d\n" 
                  (exn.Message) (pos.Line+1) pos.Column
             
(* Parsing from a text file *)

let fromFile (filename : string) =
    use reader = new StreamReader(filename)
    let lexbuf = Lexing.LexBuffer<char>.FromTextReader reader
    try 
      ExprPar.Main ExprLex.Token lexbuf
    with 
      | exn -> let pos = lexbuf.EndPos 
               failwithf "%s in file %s near line %d, column %d\n" 
                  (exn.Message) filename (pos.Line+1) pos.Column

//Copied from Intcomp1.fs

(* Storing intermediate results and variable bindings in the same stack *)

let rec getindex vs x = 
  match vs with 
  | []    -> failwith "Variable not found"
  | y::yr -> if x=y then 0 else 1 + getindex yr x;;

type sinstr =
  | SCstI of int                        (* push integer           *)
  | SVar of int                         (* push variable from env *)
  | SAdd                                (* pop args, push sum     *)
  | SSub                                (* pop args, push diff.   *)
  | SMul                                (* pop args, push product *)
  | SPop                                (* pop value/unbind var   *)
  | SSwap;;                             (* exchange top and next  *)
 
let rec seval (inss : sinstr list) (stack : int list) =
    match (inss, stack) with
    | ([], v :: _) -> v
    | ([], [])     -> failwith "seval: no result on stack"
    | (SCstI i :: insr,          stk) -> seval insr (i :: stk) 
    | (SVar i  :: insr,          stk) -> seval insr (List.item i stk :: stk) 
    | (SAdd    :: insr, i2::i1::stkr) -> seval insr (i1+i2 :: stkr)
    | (SSub    :: insr, i2::i1::stkr) -> seval insr (i1-i2 :: stkr)
    | (SMul    :: insr, i2::i1::stkr) -> seval insr (i1*i2 :: stkr)
    | (SPop    :: insr,    _ :: stkr) -> seval insr stkr
    | (SSwap   :: insr, i2::i1::stkr) -> seval insr (i1::i2::stkr)
    | _ -> failwith "seval: too few operands on stack";;


(* A compile-time variable environment representing the state of
   the run-time stack. *)

type stackvalue =
  | Value                               (* A computed value *)
  | Bound of string;;                   (* A bound variable *)

(* Compilation to a list of instructions for a unified-stack machine *)

let rec scomp (e : expr) (cenv : stackvalue list) : sinstr list =
    match e with
    | CstI i -> [SCstI i]
    | Var x  -> [SVar (getindex cenv (Bound x))]
    | Let(x, erhs, ebody) -> 
          scomp erhs cenv @ scomp ebody (Bound x :: cenv) @ [SSwap; SPop]
    | If(e1,e2,e3) -> failwith ("not implemented")
    | Prim("+", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SAdd] 
    | Prim("-", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SSub] 
    | Prim("*", e1, e2) -> 
          scomp e1 cenv @ scomp e2 (Value :: cenv) @ [SMul] 
    | Prim _ -> failwith "scomp: unknown operator";;

//Exercise 3.6 FartCity
let compString (s: string) : sinstr list = scomp (fromString s) []




