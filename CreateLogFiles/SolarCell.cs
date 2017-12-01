using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateLogFiles
{
    class SolarCell
    {
        public double ProductionTime { get; set; }  // production time of the solar cell in seconds
        public double Pmpp { get; set; }    // pmpp of the cell
        public string Quality { get; set; } // quality of the solar cell

        // creates and returns a new solar cell object with random properties
        public void GetRandomSolarCellProperties(SolarCell randomSolarCell, Random randomNumber)
        {
            randomSolarCell.ProductionTime = randomNumber.Next(5, 121);  // assign random time between 5 and 120 seconds as production time
            randomSolarCell.Pmpp = 5.5 * Math.Pow(Math.Abs(Math.Sqrt(-2.0 * Math.Log((1 - randomNumber.NextDouble()))) * Math.Sin(2.0 * Math.PI * (1.0 - randomNumber.NextDouble()))), 0.03); // box müller standard deviated random number
            if (randomSolarCell.Pmpp > 5.1)  // assign quality value depending on pmpp of cell
            {
                randomSolarCell.Quality = "A";
            }
            else
            {
                if (randomSolarCell.Pmpp > 4.8)
                {
                    randomSolarCell.Quality = "B";
                }
                else { randomSolarCell.Quality = "C"; }
            }
        }
    }
}
