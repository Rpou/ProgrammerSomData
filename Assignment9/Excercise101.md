## Excercise 10.1

1. It pops two ints on the stack, then it pops add and puts the result of the previous two ints on the stack.
2. It pushes the integer i to the stack.
3. NIL is equal to NULL, 0 is an int. So 0 would be valid in an add statement, where as NIL is a pointer that would crash the program.
4. IFZERO check wether the previous value on the stack is either zero or NULL. If true, it will jump to the adress given to IFZERO and continue excecuting from there.
5. It pops the two previous values on the stack and creates a reference to them together on the heap.
6. CAR uses a pointer as created by CONS. It the returns the first value of the pair created from CONS.
7. SETCAR does the same as CAR, but instead of returning it updates the value. The pointer and new values comes before it on the stack.
