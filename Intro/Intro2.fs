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

let rec fmt e : string =
    match e with
    | CstI i -> string i 
    | Var x -> x
    | Add(e1, e2) -> "(" + fmt e1 + " + " + fmt e2 + ")"
    | Sub(e1, e2) -> "(" + fmt e1 + " - " + fmt e2 + ")"
    | Mul(e1, e2) -> "(" + fmt e1 + " * " + fmt e2 + ")"
    
let e12 =  fmt e4

// added "let aex1 = simplify e1" to cover cases with nested Add(Add...) 
let rec simplify ae : aexpr =
    match ae with
    | Add (e1,e2) ->
        let aex1 = simplify e1
        let aex2 = simplify e2
        //  if both const, then add. if one is 0, then return the other var.
        match aex1, aex2 with
        | CstI i1, CstI i2 -> CstI (i1 + i2)
        | CstI 0, i2 -> i2
        | i1, CstI 0 -> i1
        | _ -> Add (aex1,aex2)
    | Sub (e1, e2) ->
        let aex1 = simplify e1
        let aex2 = simplify e2
        // if both const, then minus. if right is 0 then return first var. 
        match aex1, aex2 with
        | CstI i1, CstI i2 -> CstI (i1 - i2)
        | i1, CstI 0 -> i1
        | i1, i2 -> if i1 = i2 then CstI 0 else Sub (aex1,aex2)
    | Mul (e1, e2) ->
        let aex1 = simplify e1
        let aex2 = simplify e2
        // if both const then mul. if one side is 1 then return other var. if one side is 0 then 0.
        match aex1, aex2 with
        | CstI i1, CstI i2 -> CstI (i1 * i2)
        | CstI 1, i2 -> i2
        | i1, CstI 1 -> i1
        | CstI 0, _ -> CstI 0
        | _, CstI 0 -> CstI 0
        | _ -> Mul (aex1,aex2)
    | _ -> ae

// tests for simplify
let e13 = fmt (simplify (Sub(Var "v", Var "v")))
let e14 = fmt (simplify (Add(Var "x", CstI 0)))
let e15 = fmt (simplify (Add(CstI 0, Var "y")));
let e16 = fmt (simplify (Mul(CstI 1, Var "z")));
let e17 = fmt (simplify (Mul(Var "z", CstI 0)));
let e18 = fmt (simplify (Sub(CstI 7, CstI 3)));
let e19 = fmt (simplify (Sub(Var "w", Var "w")));

// symbolic differentiation.
// If the var is the same as the variable, it is 1
// else it is a constant and is 0.
let rec diff (variable:string) (e:aexpr) : aexpr =
    match e with
    | CstI _        -> CstI 0
    | Var x         -> if x = variable then CstI 1 else CstI 0
    | Add(e1,e2)    -> Add(diff variable e1, diff variable e2)
    | Sub(e1,e2)    -> Sub(diff variable e1, diff variable e2)
    | Mul(e1,e2)    -> Add(Mul(diff variable e1, e2), Mul(e1, diff variable e2))
    
let e20 = Add(Mul(Var "x", Var "y"), Var "y")   // x*y + y

let e21 = diff "x" e20 |> simplify |> fmt
let e22 = diff "y" e20 |> simplify |> fmt

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
let e7 = eval (Prim("max", CstI 3, CstI 8)) env;; // Test max

let e8 = eval (Prim("min", CstI 3, CstI 8)) env;; // Test min

let e9 = eval (Prim("==", CstI 2, Var "a")) env;; // Test equals

let e10 = eval (If(Var "a", CstI 11, CstI 22)) env;; // Test If

let e11 = eval (If(CstI 0, CstI 11, CstI 22)) env;; // Test If
