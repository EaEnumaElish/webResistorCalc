using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace CalculatorLibrary
{
    public class Resistor
    {
        int RingCount, TempK;
        double Resistance, Tolerance;
        string[] Rings;

        private readonly Dictionary<string, double> multiptiplicatorsMap = new Dictionary<string, double>() {
                    { "black", 1},
                    { "brown", 10},
                    { "red", 100},
                    { "orange", 1000},
                    { "yellow", 10000},
                    { "green", 100000},
                    { "blue", 1000000},
                    { "purple", 10000000},
                    { "gold", 0.1},
                    { "silver", 0.01},
        };
        private readonly Dictionary<string, int> colorsToNumberMap = new Dictionary<string, int>() {
                    { "black", 0},
                    { "brown", 1},
                    { "red", 2},
                    { "orange", 3},
                    { "yellow", 4},
                    { "green", 5},
                    { "blue", 6},
                    { "purple", 7},
                    { "grey", 8},
                    { "white", 9},
        };
        private readonly Dictionary<string, double> colorsToTolaranceMap = new Dictionary<string, double>() {
                    { "brown", 1},
                    { "red", 2},
                    { "green", 0.5},
                    { "blue", 0.25},
                    { "purple", 0.1},
                    { "gold", 5},
                    { "silver", 10},
        };
        private readonly Dictionary<string, int> colorsToTempKMap = new Dictionary<string, int>() {
                    { "black", 0},
                    { "brown", 100},
                    { "red", 50},
                    { "orange", 15},
                    { "yellow", 25},
                    { "blue", 10},
                    { "purple", 5},
                    { "white", 1},
        };

        public Resistor(int RignCount, string[] Rings)
        {
            RingCount = RignCount;
            this.Rings = Rings;
        }

        public void CalculateResistance()
        {
            switch (RingCount)
            {
                case 3:
                    {
                        double multiplicator = GetMultiplierByColor(RingColor(3));
                        double ring1 = Value(RingColor(1));
                        double ring2 = Value(RingColor(2));

                        string ringsValue = Convert.ToString(ring1) + Convert.ToString(ring2);

                        Resistance = Convert.ToInt32(ringsValue) * multiplicator;
                        break;
                    }
                case 4:
                    {
                        double multiplicator = GetMultiplierByColor(RingColor(3));
                        double ring1 = Value(RingColor(1));
                        double ring2 = Value(RingColor(2));

                        string ringsValue = Convert.ToString(ring1) + Convert.ToString(ring2);

                        Resistance = Convert.ToInt32(ringsValue) * multiplicator;
                        Tolerance = GetToleranceByColor(RingColor(4));
                        break;
                    }
                case 5:
                    {
                        double multiplicator = GetMultiplierByColor(RingColor(4));
                        double ring1 = Value(RingColor(1));
                        double ring2 = Value(RingColor(2));
                        double ring3 = Value(RingColor(3));

                        string ringsValue = Convert.ToString(ring1) + Convert.ToString(ring2) + Convert.ToString(ring3);

                        Resistance = Convert.ToInt32(ringsValue) * multiplicator;
                        Tolerance = GetToleranceByColor(RingColor(5));
                        break;
                    }
                case 6:
                    {
                        double multiplicator = GetMultiplierByColor(RingColor(4));
                        double ring1 = Value(RingColor(1));
                        double ring2 = Value(RingColor(2));
                        double ring3 = Value(RingColor(3));
                        string ringsValue = Convert.ToString(ring1) + Convert.ToString(ring2) + Convert.ToString(ring3);

                        Resistance = Convert.ToInt32(ringsValue) * multiplicator;
                        Tolerance = GetToleranceByColor(RingColor(5));
                        TempK = GetTempKByColor(RingColor(6));
                        break;
                    }
            }
        }

        public double GetMultiplierByColor(string color)
        {
            return multiptiplicatorsMap[color];
        }

        public double GetToleranceByColor(string color)
        {
            return colorsToTolaranceMap[color];
        }
        public int GetTempKByColor(string color)
        {
            return colorsToTempKMap[color];
        }
        public string RingColor(int position)
        {
            return Rings[position - 1];
        }
        public int Value(string color)
        {
            return colorsToNumberMap[color];
        }

        public double GetResistance()
        {
            return Resistance;
        }
        public double GetTolerance()
        {
            return Tolerance;
        }
        public int GetTempK()
        {
            return TempK;
        }
    }




    public partial class ApplicationDBContext : DbContext
    {

        public virtual DbSet<Resistor> Resistors { get; set; }

        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CalculatorDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }

        }

    }
}