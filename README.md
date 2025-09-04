# ProgrammerSomData
Programmer som data exercises

# ASSIGNMENT 1

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


## Exercise 2.1 

Extend the expression language expr from Intcomp1.fs with
multiple sequential let-bindings, such as this (in concrete syntax):

<img width="513" height="40" alt="image" src="https://github.com/user-attachments/assets/f92a2e91-639b-452e-93a0-6c729c3c89f3" />

To evaluate this, the right-hand side expression 5+7 must be evaluated and bound
to x1, and then x1*2 must be evaluated and bound to x2, after which the let-body
x1+x2 is evaluated.
The new abstract syntax for expr might be

<img width="754" height="144" alt="image" src="https://github.com/user-attachments/assets/ef9d83a1-d229-4875-b349-03f303e4a043" />

so that the Let constructor takes a list of bindings, where a binding is a pair of a
variable name and an expression. The example above would be represented as:

<img width="835" height="44" alt="image" src="https://github.com/user-attachments/assets/88469aca-8049-46c5-b478-3ab9cdeb72e3" />

Revise the eval interpreter from Intcomp1.fs to work for the expr language
extended with multiple sequential let-bindings.


## Exercise 2.2 

Revise the function freevars : expr -> string list to
work for the language as extended in Exercise 2.1. Note that the example expression
in the beginning of Exercise 2.1 has no free variables, but let x1 = x1+7 in
x1+8 end has the free variable x1, because the variable x1 is bound only in the
body (x1+8), not in the right-hand side (x1+7), of its own binding. There are
programming languages where a variable can be used in the right-hand side of its
own binding, but ours is not such a language.


## Exercise 2.3 

Revise the expr-to-texpr compiler tcomp : expr -> texpr
from Intcomp1.fs to work for the extended expr language. There is no need
to modify the texpr language or the teval interpreter to accommodate multiple
sequential let-bindings.

# Assignment 2

## Exercise 2.4

Write a bytecode assembler (in F#) that translates a list of byte-
code instructions for the simple stack machine in Intcomp1.fs into a list of
integers. The integers should be the corresponding bytecodes for the interpreter
in Machine.java. Thus you should write a function: 

assemble : sinstr list -> int list.

Use this function together with scomp from Intcomp1.fs to make a compiler
from the original expressions language expr to a list of bytecodes int list.
You may test the output of your compiler by typing in the numbers as an int
array in the Machine.java interpreter. (Or you may solve Exercise 2.5 below to
avoid this manual work).

## Exercise 2.5

Modify the compiler from Exercise 2.4 to write the lists of integers to
a file. An F# list inss of integers may be output to the file called fname using this
function (found in Intcomp1.fs):

<img width="691" height="77" alt="image" src="https://github.com/user-attachments/assets/91ac68dc-405c-4562-a063-c7c4939aea1c" />

Then modify the stack machine interpreter in Machine.java to read the sequence
of integers from a text file, and execute it as a stack machine program. The name
of the text file may be given as a command-line parameter to the Java program.
Reading numbers from the text file may be done using the StringTokenizer class or
StreamTokenizer class; see e.g. Java Precisely [4, Example 145].
It is essential that the compiler (in F#) and the interpreter (in Java) agree on the
intermediate language: what integer represents what instruction.

## Exercise 3.2

Write a regular expression that recognizes all sequences consisting of a and b
where two a’s are always separated by at least one b. For instance, these four strings
are legal: b, a, ba, ababbbaba; but these two strings are illegal: aa, babaa.
Construct the corresponding NFA. Try to find a DFA corresponding to the NFA.


