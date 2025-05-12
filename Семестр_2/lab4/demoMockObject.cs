using System;

namespace DeliveryServiceExample
{
    // Интерфейс службы доставки
    public interface IDeliveryService
    {
        DeliveryResult Calculate(DeliveryParams parameters);
    }

    // Параметры доставки
    public class DeliveryParams
    {
        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Distance { get; set; }
    }

    // Результат доставки
    public class DeliveryResult
    {
        public double Price { get; set; }
        public int EstimatedDays { get; set; }
    }

    // Реальная служба доставки (конкретная реализация)
    public class RealDeliveryService : IDeliveryService
    {
        public DeliveryResult Calculate(DeliveryParams parameters)
        {
            double volume = parameters.Length * parameters.Width * parameters.Height;
            double price = parameters.Weight * 10 + volume * 0.05 + parameters.Distance * 0.1;
            int days = (int)Math.Ceiling(parameters.Distance / 500.0);

            return new DeliveryResult
            {
                Price = Math.Round(price, 2),
                EstimatedDays = days
            };
        }
    }

    // Фиктивная служба доставки (для тестов)
    public class MockDeliveryService : IDeliveryService
    {
        public DeliveryResult Calculate(DeliveryParams parameters)
        {
            return new DeliveryResult
            {
                Price = 999,
                EstimatedDays = 3
            };
        }
    }

    // Клиентский код
    class Program
    {
        static void Main(string[] args)
        {
            // Переключение между реалной и фиктивной службой
            Console.WriteLine("Использовать Mock-сервис? (yes/no): ");
            string input = Console.ReadLine();
            IDeliveryService service = input == "yes"
                ? new MockDeliveryService()
                : new RealDeliveryService();

            // Ввод данных
            var deliveryParams = new DeliveryParams
            {
                Weight = 5,
                Length = 40,
                Width = 30,
                Height = 20,
                Distance = 1000
            };

            // Расчет и вывод результата
            var result = service.Calculate(deliveryParams);
            Console.WriteLine($"Стоимость доставки: {result.Price} руб.");
            Console.WriteLine($"Примерный срок: {result.EstimatedDays} дней.");
        }
    }
}
