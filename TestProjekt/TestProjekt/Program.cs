using System;
using System.Collections.Generic;
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
    
    public abstract int Eval(Dictionary<string, int> env);
    
    public abstract AExpr Simplify();
    
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

    public override int Eval(Dictionary<string, int> env)
    {
        return this.i;
    }

    public override AExpr Simplify()
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
    
    public int Lookup(Dictionary<string, int> env)
    {
        return env[this.name];
    }
    
    public override int Eval(Dictionary<string, int> env)
    {
        return Lookup(env);
    }
    
    public override AExpr Simplify()
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

    public override int Eval(Dictionary<string, int> env)
    {
        var evaluated1 = e1.Eval(env);
        var evaluated2 = e2.Eval(env);
        
        return evaluated1 + evaluated2;
    }

    public override AExpr Simplify()
    {
        var evaluated1 = e1.Simplify();
        var evaluated2 = e2.Simplify();
        
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
    
    public override int Eval(Dictionary<string, int> env)
    {
        var evaluated1 = e1.Eval(env);
        var evaluated2 = e2.Eval(env);
        
        return evaluated1 * evaluated2;
    }
    
    public override AExpr Simplify()
    {
        var evaluated1 = e1.Simplify();
        var evaluated2 = e2.Simplify();
        
        if(evaluated1 is CstI && evaluated2 is CstI)
        {
            return new CstI(Int32.Parse(evaluated1.Fmt()) * Int32.Parse(evaluated2.Fmt()));  
        } 
        else if (evaluated1 is CstI && Int32.Parse(evaluated1.Fmt()) == 0)
        {
            return new CstI(0);
        }
        else if (evaluated2 is CstI && Int32.Parse(evaluated2.Fmt()) == 0)
        {
            return new CstI(0);
        }
        else if (evaluated1 is CstI && Int32.Parse(evaluated1.Fmt()) == 1)
        {
            return evaluated2;
        }
        else if (evaluated2 is CstI && Int32.Parse(evaluated2.Fmt()) == 1)
        {
            return evaluated1;
        }
        else
        {
            return new Mul(evaluated1, evaluated2);
        }
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

    public override int Eval(Dictionary<string, int> env)
    {
        var evaluated1 = e1.Eval(env);
        var evaluated2 = e2.Eval(env);
        
        return evaluated1 - evaluated2;
    }
    
    public override AExpr Simplify()
    {
        var evaluated1 = e1.Simplify();
        var evaluated2 = e2.Simplify();
        
        if(evaluated1 is CstI && evaluated2 is CstI)
        {
            return new CstI(Int32.Parse(evaluated1.Fmt()) - Int32.Parse(evaluated2.Fmt()));  
        } 
        else if (evaluated2 is CstI && Int32.Parse(evaluated2.Fmt()) == 0)
        {
            return evaluated1;
        }
        else
        {
            if (evaluated1.Fmt() == evaluated2.Fmt())
            {
                return new CstI(0);
            }
            else
            {
                return new Sub(evaluated1, evaluated2);
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
        AExpr e4 = new Sub(new Var("a"), new Var("a"));
        AExpr e5 = new Mul(new CstI(1), new Var("z"));
        AExpr e6 = new Sub(e1, new CstI(10));

        var env0 = new Dictionary<string, int>
        {
            { "a", 3 },
            { "c", 78 },
            { "baf", 666 },
            { "b", 111 }
        };
        
        Console.WriteLine(e1.Simplify().Fmt());
        Console.WriteLine(e2.Simplify().Fmt());
        Console.WriteLine(e3.Simplify().Fmt());
        Console.WriteLine(e4.Simplify().Fmt());
        Console.WriteLine(e5.Simplify().Fmt());
        Console.WriteLine(e6.Simplify().Fmt());
        
    }
}

