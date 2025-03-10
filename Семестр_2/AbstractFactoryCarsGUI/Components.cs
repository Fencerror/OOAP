namespace AbstractFactoryCarsGUI
{
    // Абстрактные классы для компонентов
    abstract class Body { public abstract string Type { get; } }
    abstract class Engine { public abstract string Type { get; } }
    abstract class Wheels { public abstract string Type { get; } }

    // Конкретные компоненты для BMW
    class BMWBody : Body { public override string Type => "Седан"; }
    class BMWEngine : Engine { public override string Type => "Бензиновый 3.0L"; }
    class BMWWheels : Wheels { public override string Type => "Спортивные"; }

    // Конкретные компоненты для Tesla
    class TeslaBody : Body { public override string Type => "Спорткар"; }
    class TeslaEngine : Engine { public override string Type => "Электрический"; }
    class TeslaWheels : Wheels { public override string Type => "Аэродинамические"; }
}
