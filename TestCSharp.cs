using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Опишите класс автомобиль у которого есть базовые параметры в виде типа ТС, среднего расхода топлива, объем топливного бака, скорость. 

Опишите метод, с помощью которого можно вычислить сколько автомобиль может проехать на полном баке топлива или на остаточном количестве топлива в баке на данный момент. 
Метод для отображения текущей информации о состоянии запаса хода в зависимости от пассажиров и груза. 
Метод, который на основе параметров количества топлива и заданного расстояния вычисляет за сколько автомобиль его преодолеет. 

Реализуйте на его основе классы легковой автомобиль, грузовой автомобиль, спортивный автомобиль. 

У легкового автомобиля добавьте параметр количество перевозимых пассажиров. На основе данного параметра может изменяться запас хода. 
Предусмотрите проверку на допустимое количество пассажиров. Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%. 

Класс грузового автомобиля дополните параметром грузоподъемность. Также, как и у легкового автомобиля, грузоподъемность влияет на запас хода автомобиля. 
Дополните класс проверкой может ли автомобиль принять полный груз на борт. Каждые дополнительные 200кг веса уменьшают запас хода на 4%.
*/
namespace VehicleTest
{
    internal class TestCSharp
    {
        static void Main(string[] args)
        {
            // -------------------------------------------------------------------------------------
            Vehicle vehicle1 = new PassengerCar(fuelConsumption:5, fuelCapacity:40, speed:80, passengerCapacity:4, passenger:0);
            Console.WriteLine(vehicle1);

            Console.WriteLine($"Дальность по топливу({vehicle1.FuelСapacity}) {vehicle1.GetEstimateRange(vehicle1.FuelСapacity)} км");

            vehicle1.Fuel = 35;

            Console.WriteLine($"Дальность по топливу({vehicle1.Fuel}) {vehicle1.GetEstimateRange(vehicle1.Fuel)} км");

            Console.WriteLine($"Дальность по топливу(с учетом пассажиров:{((PassengerCar)vehicle1).Passenger}) {vehicle1.GetActualRange()} км");

            ((PassengerCar)vehicle1).Passenger = 5;
            Console.WriteLine(vehicle1);

            ((PassengerCar)vehicle1).Passenger = 2;
            Console.WriteLine(vehicle1);

            Console.WriteLine($"Дальность по топливу(с учетом пассажиров:{((PassengerCar)vehicle1).Passenger}) {vehicle1.GetActualRange()} км");
            Console.WriteLine($"Время в пути на 500км:{vehicle1.GetTravelTime(500)}ч");


            Console.WriteLine();

            // -------------------------------------------------------------------------------------
            Vehicle vehicle2 = new TruckCar(fuelConsumption:15, fuelCapacity:60, speed:60, cargoCapacity:1000, 0);
            Console.WriteLine(vehicle2);

            Console.WriteLine($"Дальность по топливу({vehicle2.FuelСapacity}) {vehicle1.GetEstimateRange(vehicle2.FuelСapacity)} км");

            vehicle2.Fuel = 35;

            Console.WriteLine($"Дальность по топливу({vehicle2.Fuel}) {vehicle1.GetEstimateRange(vehicle2.Fuel)} км");

            Console.WriteLine($"Дальность по топливу(с учетом груза:{((TruckCar)vehicle2).Cargo}) {vehicle2.GetActualRange()} км");

            ((TruckCar)vehicle2).Cargo = 1500;
            Console.WriteLine(vehicle2);

            ((TruckCar)vehicle2).Cargo = 700;
            Console.WriteLine(vehicle2);

            Console.WriteLine($"Дальность по топливу(с учетом груза:{((TruckCar)vehicle2).Cargo}) {vehicle2.GetActualRange()} км");
            Console.WriteLine($"Время в пути на 150км:{vehicle2.GetTravelTime(150)}ч");

            Console.WriteLine();

            // -------------------------------------------------------------------------------------
            Vehicle vehicle3 = new SportCar(fuelConsumption: 12, fuelCapacity: 50, speed: 160);
            Console.WriteLine(vehicle3);

            Console.WriteLine($"Дальность по топливу({vehicle3.FuelСapacity}) {vehicle3.GetEstimateRange(vehicle3.FuelСapacity)} км");

            Console.ReadLine();
        }
    }

    enum VechicleType { Passenger, Sport, Truck }

    // Опишите класс автомобиль у которого есть базовые параметры в виде типа ТС, среднего расхода топлива, объем топливного бака, скорость. 
    abstract class Vehicle
    {
        public VechicleType Type { get; set; } // Тип ТС 

        //TODO .. на 100км, уточнить?
        public double FuelConsumption { get; set; } // Средний расхода топлива

        public double FuelСapacity { get; set; } // Объем топливного бака

        // TODO ...средняя подразумевается, возможно нужно отдельное поле для текущей, уточнить?
        public double Speed { get; set; } // Скорость

        public double Fuel { get; set; } // Топливо

        public Vehicle(double fuelConsumption, double fuelCapacity, double speed) : this(fuelConsumption, fuelCapacity, speed, fuelCapacity)
        {
        }
        public Vehicle(double fuelConsumption, double fuelCapacity, double speed, double fuel)
        {
            this.FuelConsumption = fuelConsumption;
            this.FuelСapacity = fuelCapacity;
            this.Speed = speed;
            this.Fuel = fuel;
        }

        //Опишите метод, с помощью которого можно вычислить сколько автомобиль может проехать на полном баке топлива или на остаточном количестве топлива в баке на данный момент.
        public double GetEstimateRange(double fuel)
        {
            return (fuel / FuelConsumption) * 100;
        }

        //Метод для отображения текущей информации о состоянии запаса хода в зависимости от пассажиров и груза. 
        public virtual double GetActualRange()
        {
            return (Fuel / FuelConsumption) * 100;
        }

        //Метод, который на основе параметров количества топлива и заданного расстояния вычисляет за сколько автомобиль его преодолеет.
        public double GetTravelTime(double distance)
        {
            if (Speed > 0 && distance < GetActualRange())
            {
                return distance / Speed;
            } else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return $"Type:{Type}, FuelConsumption:{FuelConsumption}, FuelCapacity:{FuelСapacity}, Speed:{Speed}, Fuel:{Fuel}"; 
        }
    }

    // У легкового автомобиля добавьте параметр количество перевозимых пассажиров. На основе данного параметра может изменяться запас хода. 
    // Предусмотрите проверку на допустимое количество пассажиров.Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%.
    class PassengerCar : Vehicle
    {
        public int PassengerCapacity { get; set; } // Пассажировместимость

        // Предусмотрите проверку на допустимое количество пассажиров
        // Проверка наивная... в реальном проекте их скорее всего будет 2(на уровне интерфейса и модели в методах типа Validate())
        public int Passenger { 
            get => _passenger; 
            set { if (value >=0 && value <= PassengerCapacity) 
                { _passenger = value; } 
                else { Console.WriteLine($"(Passenger.set)Количество {value}, должно быть в диапазоне от нуля до {PassengerCapacity}"); } 
            } 
        }
        private int _passenger; // Количество пассажиров

        public PassengerCar(double fuelConsumption, double fuelCapacity, double speed, int passengerCapacity, int passenger)
            : base(fuelConsumption, fuelCapacity, speed)
        {
            this.Type = VechicleType.Passenger; 
            this.PassengerCapacity = passengerCapacity;
            this.Passenger = passenger;
        }

        const double passengerRangeReducing = 0.06; // Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%.

        public override double GetActualRange()
        {
            if (Passenger > 0)
            {
                return base.GetActualRange() - base.GetActualRange() * (passengerRangeReducing * _passenger);
            } else
            {
                return base.GetActualRange();
            }
        }

        public override string ToString()
        {
            return base.ToString() + $", PassengerCapacity:{PassengerCapacity}, Passenger:{Passenger}";
        }
    }

    // Класс грузового автомобиля дополните параметром грузоподъемность.
    // Также, как и у легкового автомобиля, грузоподъемность влияет на запас хода автомобиля. 
    // Дополните класс проверкой может ли автомобиль принять полный груз на борт.
    class TruckCar : Vehicle
    {
        public double CargoCapacity { get => cargoCapacity; set => cargoCapacity = value; }
        private double cargoCapacity;

        // Дополните класс проверкой может ли автомобиль принять полный груз на борт.
        public double Cargo { 
            get => cargo;
            set { if (value>=0 && value <= CargoCapacity) 
                    { cargo = value; } 
                    else { Console.WriteLine($"(Cargo.set)Количество {value}, должно быть в диапазоне от нуля до {CargoCapacity}"); } 
            }
        }
        private double cargo;

        public TruckCar(double fuelConsumption, double fuelCapacity, double speed, double cargoCapacity, double cargo) 
            : base(fuelConsumption, fuelCapacity, speed)
        {
            this.Type = VechicleType.Truck;
            this.CargoCapacity = cargoCapacity;
            this.Cargo = cargo;
        }

        const double cargoRangeReducing = 0.04; // Каждые дополнительные 200кг веса уменьшают запас хода на 4%.

        public override double GetActualRange()
        {
            if (Cargo > 0)
            {
                return base.GetActualRange() - base.GetActualRange() * (cargoRangeReducing * (Cargo/200));
            }
            else
            {
                return base.GetActualRange();
            }
        }

        public override string ToString()
        {
            return base.ToString() + $", CargoCapacity:{CargoCapacity}, Cargo:{Cargo}";
        }

    }

    // спортивный автомобиль
    class SportCar : Vehicle
    {
        public SportCar(double fuelConsumption, double fuelCapacity, double speed)
            : base(fuelConsumption, fuelCapacity, speed)
        {
            this.Type = VechicleType.Sport;
        }
    }
}
