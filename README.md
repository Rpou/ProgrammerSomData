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
