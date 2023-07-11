namespace Beyond.NET.Sample.SubclassingTests;

public class MyBaseClass {
    public virtual void Do() {
        Console.WriteLine($"{nameof(Do)} called in {nameof(MyBaseClass)}");
    }
}

public class MySubClass: MyBaseClass {
    public override void Do() {
        Console.WriteLine($"{nameof(Do)} called in {nameof(MySubClass)}");
    }
}