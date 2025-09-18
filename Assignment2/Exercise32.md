# Exercise 3.2

## Regex
`(b*(ab|b)*a?|a)`

## NFA
This is our NFA

<img width="618" height="466" alt="image" src="https://github.com/user-attachments/assets/07a04eea-829b-4dc3-85a4-8d43c049c0ca" />


## DFA
This is our DFA

<img width="625" height="421" alt="image" src="https://github.com/user-attachments/assets/90b0e5e0-e4ab-4971-848a-47f5182439bd" />


# Hello Lex

## Q1

Regular expression: [0-9] = 0,1,2,3,4,5,6,7,8,9

## Q2

Files generated: hello.fs and hello.fsi is made after generating the lexer.

How many states: 3

## Q3

<img width="278" height="141" alt="image" src="https://github.com/user-attachments/assets/1a065209-2015-49d2-bb53-340a8c94081f" />



## Q4

<img width="262" height="109" alt="image" src="https://github.com/user-attachments/assets/c03d3bad-f262-4165-a46a-534a1ee99f1b" />


## Q5

<img width="350" height="111" alt="image" src="https://github.com/user-attachments/assets/f69b3529-8378-4622-8e4b-0860f5d4f9bd" />


## Q6

It recognizes 34, because the "." is optional.

It recognizes 34.24, because it can understand '.'.

It does not recognize 34,24, because it can't understand ','. It recognizes 34 only.

