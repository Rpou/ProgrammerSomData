(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

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
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr;;
    
let e4 = Sub(Var "v", Add(Var "W", Var "Z"));;

let e5 = Mul(Var "2", Sub(Var "v", Add(Var "W", Var "Z")));;

let e6 = Add(Add(Var "x", Var "y"), Add(Var "z", Var "v"));;

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr;;
  

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim(ope , e1, e2) ->
        let i1 = eval e1 env
        let i2 = eval e2 env
        match ope with
        | "+" -> i1 + i2
        | "-" -> i1 - i2
        | "*" -> i1 * i2
        | "max" -> if i1 > i2 then i1 else i2
        | "min" -> if i1 < i2 then i1 else i2
        | "==" -> if i1 = i2 then 1 else 0
        | _ -> failwith "unknown primitive"
    | If(e1, e2, e3) -> if eval e1 env <> 0 then eval e2 env else eval e3 env;; 

let e1v  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;

// Tests
let e4 = eval (Prim("max", CstI 3, CstI 8)) env;; // Test max

let e5 = eval (Prim("min", CstI 3, CstI 8)) env;; // Test min

let e6 = eval (Prim("==", CstI 2, Var "a")) env;; // Test equals

let e7 = eval (If(Var "a", CstI 11, CstI 22)) env;; // Test If

let e8 = eval (If(CstI 0, CstI 11, CstI 22)) env;; // Test If
