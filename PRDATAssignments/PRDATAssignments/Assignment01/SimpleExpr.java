
// File Intro/SimpleExpr.java
// Java representation of expressions as in lecture 1
// sestoft@itu.dk * 2010-08-29

import java.util.HashMap;
import java.util.Map;

abstract class Expr { 
  abstract public int eval(Map<String,Integer> env);
  abstract public String toString();
  abstract public Expr simplify();
}

class CstI extends Expr { 
  protected final int i;

  public CstI(int i) { 
    this.i = i; 
  }

  public int eval(Map<String,Integer> env) {
    return i;
  } 

  public String fmt() {
    return ""+i;
  }

  public String fmt2(Map<String,Integer> env) {
    return ""+i;
  }

  public String toString(){
    return ""+i;
  }

  public Expr simplify(){
    return new CstI (i);
  }
  
}

class Var extends Expr { 
  protected final String name;

  public Var(String name) { 
    this.name = name; 
  }

  public int eval(Map<String,Integer> env) {
    return env.get(name);
  } 

  public String fmt() {
    return name;
  } 

  public String fmt2(Map<String,Integer> env) {
    return ""+env.get(name);
  } 

  public String toString() {
    return name;
  } 

  public Expr simplify(){
    return new Var (name);
  }

}

abstract class Binop extends Expr{ 
  protected final String oper;
  protected final Expr e1, e2;

  public Binop (Expr e1, Expr e2, String oper){
    this.e1 = e1;
    this.e2 = e2;
    this.oper = oper;
  }

  public String toString(){
    return (e1.toString() + oper + e2.toString()); 
  }

  public abstract int eval(Map<String,Integer> env);
  

}

class Add extends Binop {

  public Add(Expr e1, Expr e2){
    super(e1,e2,"+");
  }

  public int eval(Map<String,Integer> env){
    return (e1.eval(env)) + (e2.eval(env));
  }

  public Expr simplify(){
    if (e1.toString().equals("0")){
      return e2;
    } else if (e2.toString().equals("0")){
      return e1;
    } else {
      return new Add(e1,e2);
    }
  }
  
}

class Sub extends Binop {

  public Sub(Expr e1, Expr e2){
    super(e1,e2,"-");
  }
  public int eval(Map<String,Integer> env){
    return (e1.eval(env)) - (e2.eval(env));
  }

  public Expr simplify(){
    if (e1.toString().equals("0")){
      return e2;
    } else if (e2.toString().equals("0")){
      return e1;
    } else if (e1.toString().equals(e2.toString())){
      return new CstI (0);
    }  else {
      return new Sub(e1,e2);
    }
  }

}

class Mul extends Binop {

  public Mul(Expr e1, Expr e2){
    super(e1,e2,"*");
  }
  public int eval(Map<String,Integer> env){
    return (e1.eval(env)) * (e2.eval(env));
  }

  public Expr simplify(){
    if (e1.toString().equals("0") || e2.toString().equals("0")){
      return new CstI(0);
    } else if (e2.toString().equals("1")){
      return e1;
    } else if (e1.toString().equals("1")){
      return e2;
    }else {
      return new Mul(e1,e2);
    }
  }

}



// class Prim extends Expr { 
//   protected final String oper;
//   protected final Expr e1, e2;

//   public Prim(String oper, Expr e1, Expr e2) { 
//     this.oper = oper; this.e1 = e1; this.e2 = e2;
//   }

//   public int eval(Map<String,Integer> env) {
//     if (oper.equals("+"))
//       return e1.eval(env) + e2.eval(env);
//     else if (oper.equals("*"))
//       return e1.eval(env) * e2.eval(env);
//     else if (oper.equals("-"))
//       return e1.eval(env) - e2.eval(env);
//     else
//       throw new RuntimeException("unknown primitive");
//   } 

//   public String fmt() {
//     return "(" + e1.fmt() + oper + e2.fmt() + ")";
//   } 

//   public String fmt2(Map<String,Integer> env) {
//     return "(" + e1.fmt2(env) + oper + e2.fmt2(env) + ")";
//   } 
//   public String toString() {
//     return "";
//   } 

// }

// public class SimpleExpr {
//   public static void main(String[] args) {
//     Expr e1 = new CstI(17);
//     Expr e2 = new Prim("+", new CstI(3), new Var("a"));
//     Expr e3 = new Prim("+", new Prim("*", new Var("b"), new CstI(9)), 
// 		            new Var("a"));
//     Map<String,Integer> env0 = new HashMap<String,Integer>();
//     env0.put("a", 3);
//     env0.put("c", 78);
//     env0.put("baf", 666);
//     env0.put("b", 111);

//     System.out.println("Env: " + env0.toString());

//     System.out.println(e1.fmt() + " = " + e1.fmt2(env0) + " = " + e1.eval(env0));
//     System.out.println(e2.fmt() + " = " + e2.fmt2(env0) + " = " + e2.eval(env0));
//     System.out.println(e3.fmt() + " = " + e3.fmt2(env0) + " = " + e3.eval(env0));
//   }
// }
