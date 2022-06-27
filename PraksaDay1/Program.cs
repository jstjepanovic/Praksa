using System;
using System.Linq;
using System.Collections.Generic;

namespace PraksaDay1
{
    
    class Program
    {
        interface IEquatable<T>
        {
            bool Equals(T obj);
        }
        public abstract class Vehicle
        {

            public int ProductionYear { get; set; }
            public double TotalTravelLengthKm { get; set; }
            public abstract void Travel(double timeH, double avgSpeedKmH);

        }

        public class Car : Vehicle, IEquatable<Car>
        {
            public string Color;
            private string Model { get; set; }
            public Car(int year, string model, string color)
            {
                ProductionYear = year;
                TotalTravelLengthKm = 0;
                Color = color;
                Model = model;
            }

            public Car(int year, string model, string color, double totalTravel)
            {
                ProductionYear = year;
                TotalTravelLengthKm = totalTravel;
                Color = color;
                Model = model;
            }

            public bool Equals(Car car)
            {
                return (this.ProductionYear, this.Model, this.Color) == (car.ProductionYear, car.Model, car.Color);
            }

            public override void Travel(double timeH, double avgSpeedKmH)
            {
                double travelLength = timeH * avgSpeedKmH;
                TotalTravelLengthKm += travelLength;
                Console.WriteLine("Car drove {0}km. Total travel length is now {1}km.", travelLength, TotalTravelLengthKm);
            }

            public override string ToString() { return "Model: " + Model + ", color: " + Color; }
        }

        public class Airplane : Vehicle
        {
            public double TotalIncome = 0;
            protected string _model { get; set; }
            public Airplane(int year, string model)
            {
                ProductionYear = year;
                _model = model;
                TotalTravelLengthKm = 0;
            }
            public Airplane(int year, double totalTravel, string model)
            {
                ProductionYear = year;
                _model = model;
                TotalTravelLengthKm = totalTravel;
            }

            public string Model { get => _model; }

            public override void Travel(double timeH, double avgSpeedKmH)
            {
                double travelLength = timeH * avgSpeedKmH;
                TotalTravelLengthKm += travelLength;
                Console.WriteLine("Airplane flew {0}km. Total travel length is now {1}km.", travelLength, TotalTravelLengthKm);
            }

            public virtual void Transport(double timeH, double avgSpeedKmH, double transportPrice, int transportedEntityQuantity)
            {
                Travel(timeH, avgSpeedKmH);
            }

            public override string ToString() { return "Model: " + _model + ", total income: " + TotalIncome + ", total travel length: " + TotalTravelLengthKm; }
        }

        public class PassengerPlane : Airplane
        {
            private int _totalPassengerCount;
            private List<int> _passengerRatings = new List<int> {};

            public PassengerPlane(int year, string model) : base(year, model) { _totalPassengerCount = 0; }
            public PassengerPlane(int year, string model, double incomeToDate, int passengersToDate) : base(year, model)
            {
                TotalIncome = incomeToDate;
                _totalPassengerCount = passengersToDate;
            }

            public int TotalPassengerCount
            {
                get => _totalPassengerCount;
            }

            public double AverageRating
            {
                get { double average = _passengerRatings.Count > 0 ? _passengerRatings.Average() : 0.0; return average; }
            }

            public override void Transport(double timeH, double avgSpeedKmH, double transportPrice, int transportedEntityQuantity)
            {
                base.Transport(timeH, avgSpeedKmH, transportPrice, transportedEntityQuantity);

                _totalPassengerCount += transportedEntityQuantity;
                TotalIncome += transportedEntityQuantity * transportPrice;

                Console.WriteLine("Passenger plane finished the transportation of {0} passengers. Total flight income was {1}.", transportedEntityQuantity, transportedEntityQuantity * transportPrice);

                Random rd = new Random();
                int rand_num = rd.Next(1, 10);
                _passengerRatings.Add(rand_num);
            }
        }

        public class CargoPlane : Airplane
        {
            private int _totalCargoTransported;

            public CargoPlane(int year, string model) : base(year, model){ _totalCargoTransported = 0; }
            public CargoPlane(int year, string model, double incomeToDate, int cargoToDate) : base(year, model)
            {
                TotalIncome = incomeToDate;
                _totalCargoTransported = cargoToDate;
            }

            public int TotalCargoTrasported
            {
                get => _totalCargoTransported;
            }

            public override void Transport(double timeH, double avgSpeedKmH, double transportPrice, int transportedEntityQuantity)
            {
                base.Transport(timeH, avgSpeedKmH, transportPrice, transportedEntityQuantity);

                _totalCargoTransported += transportedEntityQuantity;
                TotalIncome += transportedEntityQuantity * transportPrice;

                Console.WriteLine("Cargo plane finished the transportation of cargo weighted {0}kg. Total flight income was {1}.", transportedEntityQuantity, transportedEntityQuantity * transportPrice);
            }
        }

        public class Company<T>
        {
            private List<T> _companyVehicles = new List<T> { };

            public List<T> CompanyVehicles { get => _companyVehicles; }
            
            public Company(params T[] vehicles)
            {
                foreach (var vehicle in vehicles)
                {
                    _companyVehicles.Add(vehicle);
                }
            }

            public void PrintCompanyVehicles()
            {
                Console.WriteLine("Company vehicles:");
                foreach (var vehicle in _companyVehicles)
                {
                    Console.WriteLine(vehicle.ToString());
                }
            }
        }
        static void Main(string[] args)
        {
            CargoPlane Cplane = new CargoPlane(2002, "883");
            PassengerPlane Pplane1 = new PassengerPlane(2008, "885");
            PassengerPlane Pplane2 = new PassengerPlane(2008, "885", 28000, 350);
            Cplane.Transport(5, 400, 15.2, 2100);
            Cplane.Transport(2.5, 450, 25.5, 750);
            Pplane1.Transport(4.4, 575, 110, 105);
            Pplane2.Transport(4, 600, 80, 45);
            Pplane2.Transport(1.5, 625, 110, 32);
            Console.WriteLine("Cplane total cargo trasported: {0}kg", Cplane.TotalCargoTrasported);
            Console.WriteLine("Pplane total income: {0}, average rating: {1}", Pplane2.TotalIncome, Pplane2.AverageRating);
            Car Car1 = new Car(2015, "TT332", "grey");
            Car Car2 = new Car(2015, "TT332", "grey", 102493);
            Car Car3 = new Car(2001, "450", "blue", 582211);
            if (Car1.Equals(Car2))
            {
                Console.WriteLine("Car C1 is equal to car C2");
            }
            var cars = new List<Car>();
            cars.Add(Car1);
            cars.Add(Car2);
            cars.Add(Car3);


            foreach (var car in cars)
            {
                car.Travel(10.2, 76.82);
            }

            var vehicles = new List<Vehicle>();
            vehicles.Add(Pplane2);
            vehicles.Add(Cplane);
            vehicles.Add(Car1);
            vehicles.Add(Car2);

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine("Production year: {0}, total travel: {1}", vehicle.ProductionYear, vehicle.TotalTravelLengthKm);
            }

            Company<PassengerPlane> Company1 = new Company<PassengerPlane>(Pplane1, Pplane2);
            Company1.PrintCompanyVehicles();

        }
    }
}
