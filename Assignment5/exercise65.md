(1)
let f x = 1
in f f end

Has type "int". Well typed.


let f g = g g
in f end

Is not typable. It is circular. Not typeable.


let f x =
  let g y = y
  in g false end
in f 42 end

Has type "bool". Well typed. 


let f x =
  let g y = if true then y else x
  in g false end
in f 42 end

Gets int and bool error. 
It mixes the int "42" and the bool "false" in an "if then else".
Not well typed.


let f x =
  let g y = if true then y else x
  in g false end
in f true end

Has type "bool". Well typed
