using System;
using System.Collections.Generic;

// Abstract expression class
abstract class Expr
{
    public abstract int Eval(Dictionary<string, int> env);
    public abstract string Fmt();
    public abstract string Fmt2(Dictionary<string, int> env);
}

// Constant integer
class CstI : Expr
{
    protected readonly int i;

    public CstI(int i)
    {
        this.i = i;
    }

    public override int Eval(Dictionary<string, int> env)
    {
        return i;
    }

    public override string Fmt()
    {
        return i.ToString();
    }

    public override string Fmt2(Dictionary<string, int> env)
    {
        return i.ToString();
    }
}

// Variable
class Var : Expr
{
    protected readonly string name;

    public Var(string name)
    {
        this.name = name;
    }

    public override int Eval(Dictionary<string, int> env)
    {
        return env[name];
    }

    public override string Fmt()
    {
        return name;
    }

    public override string Fmt2(Dictionary<string, int> env)
    {
        return env[name].ToString();
    }
}

// Primitive operation
class Prim : Expr
{
    protected readonly string oper;
    protected readonly Expr e1, e2;

    public Prim(string oper, Expr e1, Expr e2)
    {
        this.oper = oper;
        this.e1 = e1;
        this.e2 = e2;
    }

    public override int Eval(Dictionary<string, int> env)
    {
        return oper switch
        {
            "+" => e1.Eval(env) + e2.Eval(env),
            "*" => e1.Eval(env) * e2.Eval(env),
            "-" => e1.Eval(env) - e2.Eval(env),
            _ => throw new Exception("Unknown primitive")
        };
    }

    public override string Fmt()
    {
        return $"({e1.Fmt()}{oper}{e2.Fmt()})";
    }

    public override string Fmt2(Dictionary<string, int> env)
    {
        return $"({e1.Fmt2(env)}{oper}{e2.Fmt2(env)})";
    }
}

// Main program
class SimpleExpr
{
    static void Main()
    {
        Expr e1 = new CstI(17);
        Expr e2 = new Prim("+", new CstI(3), new Var("a"));
        Expr e3 = new Prim("+", new Prim("*", new Var("b"), new CstI(9)), new Var("a"));

        var env0 = new Dictionary<string, int>
        {
            { "a", 3 },
            { "c", 78 },
            { "baf", 666 },
            { "b", 111 }
        };

        Console.WriteLine("Env: " + string.Join(", ", env0));

        Console.WriteLine($"{e1.Fmt()} = {e1.Fmt2(env0)} = {e1.Eval(env0)}");
        Console.WriteLine($"{e2.Fmt()} = {e2.Fmt2(env0)} = {e2.Eval(env0)}");
        Console.WriteLine($"{e3.Fmt()} = {e3.Fmt2(env0)} = {e3.Eval(env0)}");
    }
}