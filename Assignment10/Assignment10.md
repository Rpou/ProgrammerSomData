## 11.1
let rec lenc (a: 'a list) (c: int -> 'a) =
  match a with
  | [] -> c 0
  | x::xs -> lenc xs (fun x -> c (x+1))

let rec leni (a: 'a list) acc =
  match a with
  | [] -> acc
  | x::xs -> leni xs (acc+1)

## 11.8
run (Every(Write(Prim("+", CstI 1, Prim("*", CstI 2, FromTo(1, 4))))));
run (Every(Write(Prim("+",Prim("*", CstI 10, FromTo(2,4)),FromTo(1,2)))));;

