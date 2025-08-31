using System;
using System.Collections.Generic;
using Exercises;
using Microsoft.VisualBasic.CompilerServices;

// Abstract expression class
/*abstract class Expr
{
    public abstract int Eval(Dictionary<string, int> env);
    public abstract string Fmt();
    public abstract string Fmt2(Dictionary<string, int> env);
}*/

abstract class AExpr
{
    public abstract string Fmt();
    public abstract string Fmt2(Dictionary<string, int> env);

    public abstract AExpr simplify();
}

// Constant integer
class CstI : AExpr
{
    protected readonly int i;

    public CstI(int i)
    {
        this.i = i;
    }

    public override string Fmt()
    {
        return i.ToString();
    }

    public override string Fmt2(Dictionary<string, int> env)
    {
        return i.ToString();
    }

    public override AExpr simplify()
    {
        return this;
    }
}

// Variable
class Var : AExpr
{
    protected readonly string name;

    public Var(string name)
    {
        this.name = name;
    }

    public override string Fmt()
    {
        return name;
    }

    public override string Fmt2(Dictionary<string, int> env)
    {
        return env[name].ToString();
    }
    
    public override AExpr simplify()
    {
        return this;
    }
}

class Add : AExpr
{
    protected readonly string oper;
    protected readonly AExpr e1, e2;

    public Add(AExpr e1, AExpr e2)
    {
        this.oper = "+";
        this.e1 = e1;
        this.e2 = e2;
    }

    public override string Fmt()
    {
        return $"({e1.Fmt()}{oper}{e2.Fmt()})";
    }

    public override string Fmt2(Dictionary<string, int> env)
    {
        return $"({e1.Fmt2(env)}{oper}{e2.Fmt2(env)})";
    }
    
    public override AExpr simplify()
    {
        var evaluated1 = e1.simplify();
        var evaluated2 = e2.simplify();
        
        if(evaluated1 is CstI && evaluated2 is CstI)
        {
            return new CstI(Int32.Parse(evaluated1.Fmt()) + Int32.Parse(evaluated2.Fmt()));  
        } 
        else if (evaluated1 is CstI && Int32.Parse(evaluated1.Fmt()) == 0)
        {
            return evaluated2;
        }
        else if (evaluated2 is CstI && Int32.Parse(evaluated2.Fmt()) == 0)
        {
            return evaluated1;
        }
        else
        {
            return new Add(evaluated1, evaluated2);
        }
    }
}

class Mul : AExpr
{
    protected readonly string oper;
    protected readonly AExpr e1, e2;

    public Mul(AExpr e1, AExpr e2)
    {
        this.oper = "*";
        this.e1 = e1;
        this.e2 = e2;
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

class Sub : AExpr
{
    protected readonly string oper;
    protected readonly AExpr e1, e2;

    public Sub(AExpr e1, AExpr e2)
    {
        this.oper = "-";
        this.e1 = e1;
        this.e2 = e2;
    }
    
    public override string Fmt()
    {
        return $"({e1.Fmt()}{oper}{e2.Fmt()})";
    }

    public override string Fmt2(Dictionary<string, int> env)
    {
        return $"({e1.Fmt2(env)}{oper}{e2.Fmt2(env)})";
    }
    
    public override AExpr simplify()
    {
        if(e1 is CstI && e2 is CstI)
        {
            return new CstI(Int32.Parse(e1.Fmt()) - Int32.Parse(e2.Fmt()));  
        } 
        else if (e2 is CstI && Int32.Parse(e2.Fmt()) == 0)
        {
            return e1;
        }
        else
        {
            if (e1.Fmt() == e2.Fmt())
            {
                new CstI(0);
            }
            else
            {
                return new Sub(e1, e2);
            }
        }
    }
}

// Main program
class SimpleExpr
{
    static void Main()
    {
        AExpr e1 = new CstI(17);
        AExpr e2 = new Add(new CstI(3), new Var("a"));
        AExpr e3 = new Add(new Mul(new Var("b"), new CstI(9)), new Var("a"));

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

