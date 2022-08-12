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
            Vehicle vehicle1 = new PassengerCar(fuelConsumption:5, fuelCapacity:40, avgSpeed:80, passengerCapacity:4, passenger:0);
            Console.WriteLine(vehicle1);

            Console.WriteLine($"Дальность по топливу({vehicle1.FuelСapacity}) {vehicle1.getEstimateRange(vehicle1.FuelСapacity)} км");

            vehicle1.Fuel = 35;

            Console.WriteLine($"Дальность по топливу({vehicle1.Fuel}) {vehicle1.getEstimateRange(vehicle1.Fuel)} км");

            Console.WriteLine($"Дальность по топливу(с учетом пассажиров:{((PassengerCar)vehicle1).Passenger}) {vehicle1.getActualRange()} км");

            ((PassengerCar)vehicle1).Passenger = 5;
            Console.WriteLine(vehicle1);

            ((PassengerCar)vehicle1).Passenger = 2;
            Console.WriteLine(vehicle1);

            Console.WriteLine($"Дальность по топливу(с учетом пассажиров:{((PassengerCar)vehicle1).Passenger}) {vehicle1.getActualRange()} км");
            Console.WriteLine($"Время в пути на 500км:{vehicle1.getTravelTime(500)}ч");


            Console.WriteLine();

            // -------------------------------------------------------------------------------------
            Vehicle vehicle2 = new TruckCar(fuelConsumption:15, fuelCapacity:60, avgSpeed:60, cargoCapacity:1000, 0);
            Console.WriteLine(vehicle2);

            Console.WriteLine($"Дальность по топливу({vehicle2.FuelСapacity}) {vehicle1.getEstimateRange(vehicle2.FuelСapacity)} км");

            vehicle2.Fuel = 35;

            Console.WriteLine($"Дальность по топливу({vehicle2.Fuel}) {vehicle1.getEstimateRange(vehicle2.Fuel)} км");

            Console.WriteLine($"Дальность по топливу(с учетом груза:{((TruckCar)vehicle2).Cargo}) {vehicle2.getActualRange()} км");

            ((TruckCar)vehicle2).Cargo = 1500;
            Console.WriteLine(vehicle2);

            ((TruckCar)vehicle2).Cargo = 700;
            Console.WriteLine(vehicle2);

            Console.WriteLine($"Дальность по топливу(с учетом груза:{((TruckCar)vehicle2).Cargo}) {vehicle2.getActualRange()} км");
            Console.WriteLine($"Время в пути на 150км:{vehicle2.getTravelTime(150)}ч");

            Console.WriteLine();

            // -------------------------------------------------------------------------------------
            Vehicle vehicle3 = new SportCar(fuelConsumption: 12, fuelCapacity: 50, avgSpeed: 160, speedMax: 300);
            Console.WriteLine(vehicle3);

            Console.WriteLine($"Дальность по топливу({vehicle3.FuelСapacity}) {vehicle3.getEstimateRange(vehicle3.FuelСapacity)} км");

            Console.ReadLine();
        }
    }

    enum VechicleType { Passenger, Sport, Truck }

    // Опишите класс автомобиль у которого есть базовые параметры в виде типа ТС, среднего расхода топлива, объем топливного бака, скорость. 
    abstract class Vehicle
    {
        #region VechicleType Type // тип ТС 
        public VechicleType Type { get => type; set => type = value; }
        private VechicleType type; // тип ТС 
        #endregion

        #region double FuelConsumption // среднего расхода топлива
        public double FuelConsumption { get => fuelСonsumption; set => fuelСonsumption = value; }
        private double fuelСonsumption;   // среднего расхода топлива   ? на 100км, уточнить
        #endregion

        public double FuelСapacity { get => fuelСapacity; set => fuelСapacity = value; }
        private double fuelСapacity;      // объем топливного бака

        public double AvgSpeed { get => avgSpeed; set => avgSpeed = value; }
        double avgSpeed;          // скорость, средняя

        public double Speed { get => speed; set => speed = value; }
        private double speed=0;             // скорость
        
        public double Fuel { get => fuel; set => fuel = value; }
        private double fuel=0;              // объем топлива

        public Vehicle(double fuelConsumption, double fuelCapacity, double avgSpeed)
        {
            this.FuelConsumption = fuelConsumption;
            this.FuelСapacity = fuelCapacity;
            this.AvgSpeed = avgSpeed;
            this.Speed = 0;
            this.Fuel = this.FuelСapacity;
        }

        //Опишите метод, с помощью которого можно вычислить сколько автомобиль может проехать на полном баке топлива или на остаточном количестве топлива в баке на данный момент.
        public double getEstimateRange(double fuel)
        {
            return (fuel/fuelСonsumption)*100;
        }

        //Метод для отображения текущей информации о состоянии запаса хода в зависимости от пассажиров и груза. 
        public virtual double getActualRange()
        {
            return (fuel / fuelСonsumption) * 100;
        }

        //Метод, который на основе параметров количества топлива и заданного расстояния вычисляет за сколько автомобиль его преодолеет.
        public double getTravelTime(double distance)
        {
            if (AvgSpeed > 0 && distance < getActualRange())
            {
                return distance / AvgSpeed;
            } else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return $"Type:{Type}, FuelConsumption:{FuelConsumption}, FuelCapacity:{FuelСapacity}, AvgSpeed:{AvgSpeed}, Fuel:{Fuel}, Speed:{Speed}"; 
        }
    }

    //У легкового автомобиля добавьте параметр количество перевозимых пассажиров. На основе данного параметра может изменяться запас хода. 
    //Предусмотрите проверку на допустимое количество пассажиров.Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%.
    class PassengerCar : Vehicle
    {
        public int PassengerCapacity { get => passengerCapacity; set => passengerCapacity = value; }
        private int passengerCapacity;                // пассажировместимость 
        
        public int Passenger { 
            get => passenger; 
            set { if (value >=0 && value <= PassengerCapacity) 
                { passenger = value; } 
                else { Console.WriteLine($"(Passenger.set)Количество {value}, должно быть в диапазоне от нуля до {PassengerCapacity}"); } 
            } 
        }
        private int passenger;                        // пассажиры

        public PassengerCar(double fuelConsumption, double fuelCapacity, double avgSpeed, int passengerCapacity, int passenger)
            : base(fuelConsumption, fuelCapacity, avgSpeed)
        {
            this.Type = VechicleType.Passenger; 
            this.PassengerCapacity = passengerCapacity;
            this.Passenger = passenger;
        }

        const double passengerRangeReducing = 0.06; // Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%.

        public override double getActualRange()
        {
            if (Passenger > 0)
            {
                return base.getActualRange() - base.getActualRange() * (passengerRangeReducing * passenger);
            } else
            {
                return base.getActualRange();
            }
        }

        public override string ToString()
        {
            return base.ToString() + $", PassengerCapacity:{PassengerCapacity}, Passenger:{Passenger}";
        }
    }

    // Класс грузового автомобиля дополните параметром грузоподъемность.Также, как и у легкового автомобиля, грузоподъемность влияет на запас хода автомобиля. 
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

        public TruckCar(double fuelConsumption, double fuelCapacity, double avgSpeed, double cargoCapacity, double cargo) 
            : base(fuelConsumption, fuelCapacity, avgSpeed)
        {
            this.Type = VechicleType.Truck;
            this.CargoCapacity = cargoCapacity;
            this.Cargo = cargo;
        }

        const double cargoRangeReducing = 0.04; // Каждые дополнительные 200кг веса уменьшают запас хода на 4%.

        public override double getActualRange()
        {
            if (Cargo > 0)
            {
                return base.getActualRange() - base.getActualRange() * (cargoRangeReducing * (Cargo/200));
            }
            else
            {
                return base.getActualRange();
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
        //TODO отсебятина... в принципе задание типа хватит...для него
        public double SpeedMax { get => speedMax; set => speedMax = value; }
        private double speedMax;

        public SportCar(double fuelConsumption, double fuelCapacity, double avgSpeed, double speedMax)
            : base(fuelConsumption, fuelCapacity, avgSpeed)
        {
            this.Type = VechicleType.Sport;
            this.SpeedMax = speedMax;
        }

        /*
        public override double getActualRange()
        {
            return base.getActualRange();
        }
        */

        public override string ToString()
        {
            return base.ToString() + $", SpeedMax:{SpeedMax}";
        }
    }
}
