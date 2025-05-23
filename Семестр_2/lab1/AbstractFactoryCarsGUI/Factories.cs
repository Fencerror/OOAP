namespace AbstractFactoryCarsGUI
{
    // Абстрактная фабрика автомобилей
    interface ICarFactory
    {
        Body CreateBody();
        Engine CreateEngine();
        Wheels CreateWheels();
    }

    // Конкретные фабрики производителей
    class BMWFactory : ICarFactory
    {
        public Body CreateBody() => new BMWBody();
        public Engine CreateEngine() => new BMWEngine();
        public Wheels CreateWheels() => new BMWWheels();
    }

    class TeslaFactory : ICarFactory
    {
        public Body CreateBody() => new TeslaBody();
        public Engine CreateEngine() => new TeslaEngine();
        public Wheels CreateWheels() => new TeslaWheels();
    }
}
