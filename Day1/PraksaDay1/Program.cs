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
        }

        public class Airplane : Vehicle
        {
            public double TotalIncome = 0;
            public Airplane(int year)
            {
                ProductionYear = year;
                TotalTravelLengthKm = 0;
            }
            public Airplane(int year, double totalTravel)
            {
                ProductionYear = year;
                TotalTravelLengthKm = totalTravel;
            }

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
        }

        public class PassengerPlane : Airplane
        {
            private int _totalPassengerCount;
            private List<int> _passengerRatings = new List<int> {};

            public PassengerPlane(int year) : base(year) { _totalPassengerCount = 0; }
            public PassengerPlane(int year, double incomeToDate, int passengersToDate) : base(year)
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

        class CargoPlane : Airplane
        {
            private int _totalCargoTransported;

            public CargoPlane(int year) : base(year){ _totalCargoTransported = 0; }
            public CargoPlane(int year, double incomeToDate, int cargoToDate) : base(year)
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
        static void Main(string[] args)
        {
            CargoPlane Cplane = new CargoPlane(2002);
            PassengerPlane Pplane = new PassengerPlane(2008, 28000, 350);
            Cplane.Transport(5, 400, 15.2, 2100);
            Cplane.Transport(2.5, 450, 25.5, 750);
            Pplane.Transport(4, 600, 80, 45);
            Pplane.Transport(1.5, 625, 110, 32);
            Console.WriteLine("Cplane total cargo trasported: {0}kg", Cplane.TotalCargoTrasported);
            Console.WriteLine("Pplane total income: {0}, average rating: {1}", Pplane.TotalIncome, Pplane.AverageRating);
            Car C1 = new Car(2015, "TT332", "grey");
            Car C2 = new Car(2015, "TT33", "grey");
            if (C1.Equals(C2))
            {
                Console.WriteLine("Car C1 is equal to car C2");
            }

            var vehicles = new List<Vehicle>();
            vehicles.Add(Pplane);
            vehicles.Add(Cplane);
            vehicles.Add(C1);
            vehicles.Add(C2);

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine("Production year: {0}, total travel: {1}", vehicle.ProductionYear, vehicle.TotalTravelLengthKm);
            }
        }
    }
}
