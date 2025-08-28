# ProgrammerSomData
Programmer som data exercises

## Exercise 1.1

1.  File Intro2.fs contains a definition of the expr expression language and
an evaluation function eval. Extend the eval function to handle three addi-
tional operators: "max", "min", and "==". Like the existing operators, they
take two argument expressions. The equals operator should return 1 when true
and 0 when false.

2.  Write some example expressions in this extended expression language, using
abstract syntax, and evaluate them using your new eval function.

3. Rewrite one of the eval functions to evaluate the arguments of a primitive
before branching out on the operator, in this style:

   <img width="879" height="288" alt="image" src="https://github.com/user-attachments/assets/455da436-ff49-4bb3-a799-0b741daf001b" />

   
4.  Extend the expression language with conditional expressions If(e1, e2,
e3) corresponding to Java’s expression e1 ? e2 : e3 or F#’s conditional
expression if e1 then e2 else e3.
You need to extend the expr datatype with a new constructor If that takes
three expr arguments.

5.  Extend the interpreter function eval correspondingly. It should evaluate e1,
and if e1 is non-zero, then evaluate e2, else evaluate e3. You should be able
to evaluate th expression If(Var "a", CstI 11, CstI 22) in an en-
vironment that binds variable a.

Note that various strange and non-standard interpretations of the conditional ex-
pression are possible. For instance, the interpreter might start by testing whether
expressions e2 and e3 are syntactically identical, in which case there is no need to
evaluate e1, only e2 (or e3). Although possible, this shortcut is rarely useful.

## Ecercise 1.2 

1. Declare an alternative datatype aexpr for a representation of arithmetic ex-
pressions without let-bindings. The datatype should have constructors CstI,
Var, Add, Mul, Sub, for constants, variables, addition, multiplication, and
subtraction.
Then x ∗ (y + 3) is represented as Mul(Var "x", Add(Var "y",
CstI 3)), not as Prim("*", Var "x", Prim("+", Var "y",
CstI 3)).

2. Write the representation of the expressions v − (w + z) and 2 ∗ (v − (w + z))
and x + y + z + v.

3. Write an F# function fmt : aexpr -> string to format expressions
as strings. For instance, it may format Sub(Var "x", CstI 34) as the
string "(x - 34)". It has very much the same structure as an eval func-
tion, but takes no environment argument (because the name of a variable is
independent of its value).

4. Write an F# function simplify : aexpr -> aexpr to perform expres-
sion simplification. For instance, it should simplify (x + 0) to x, and simplify
(1 + 0) to 1. The more ambitious student may want to simplify (1 + 0) ∗ (x + 0)
to x. Hint: Pattern matching is your friend. Hint: Don’t forget the case where
you cannot simplify anything.

You might consider the following simplifications, plus any others you find
useful and correct:

<img width="199" height="301" alt="image" src="https://github.com/user-attachments/assets/9b354c92-960e-4c5e-ab66-ea35ca55ff15" />


5. Write an F# function to perform symbolic differentiation of simple arithmetic
expressions (such as aexpr) with respect to a single variable.

## Exercise 1.4 

This chapter has shown how to represent abstract syntax in functional
languages such as F# (using algebraic datatypes) and in object-oriented languages
such as Java or C# (using a class hierarchy and composites).

1. Use Java or C# classes and methods to do what we have done using the F#
datatype aexpr in the preceding exercises. Design a class hierarchy to represent arithmetic expressions: it could have an abstract class Expr with subclasses CstI, Var, and Binop, where the latter is itself abstract and has
concrete subclasses Add, Mul and Sub. All classes should implement the
toString() method to format an expression as a String.
The classes may be used to build an expression in abstract syntax, and then
print it, as follows:

<img width="546" height="70" alt="image" src="https://github.com/user-attachments/assets/cef507d4-fa12-4b9c-93b0-dfbb44f6ab66" />

2. Create three more expressions in abstract syntax and print them.
3. Extend your classes with facilities to evaluate the arithmetic expressions, that is, add a method int eval(env).
4.  Add a method Expr simplify() that returns a new expression where algebraic simplifications have been performed, as in part (iv) of Exercise 1.2.

