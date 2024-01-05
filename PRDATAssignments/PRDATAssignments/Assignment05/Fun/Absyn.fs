(* Fun/Absyn.fs * Abstract syntax for micro-ML, a functional language *)

module Absyn

type expr = 
  | CstI of int
  | CstB of bool
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr
  | If of expr * expr * expr
  | Fun of string * expr
  | Letfun of string * string * expr * expr    (* (f, x, fBody, letBody) *)
  | Call of expr * expr
  | InCheck of expr * expr * expr   // raoo exam dec-2019
  | Field of expr * string // raoo exam jan-2019
  | Record of (string * expr) list // raoo exam jan-2019
