using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
namespace TerrainProfileFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup the terrain reader, setting the path to where USGS data is found on disk
            TerrainPcs.Terrain terrain = new TerrainPcs.USGS();
            terrain.TerrainDataPath = @"C:\USGS\"; // make sure you download USGS data and set this path accordingly.

            // create geolocations for our transmitter and receiver
            TerrainPcs.Geolocation txLocation = new TerrainPcs.Geolocation(42.99802, -79.03379);
            TerrainPcs.Geolocation rxLocation = new TerrainPcs.Geolocation(42.99532, -79.0095);

            // set the spacing between height values (in meters)
            double terrainSpacingm = 90;

            // call the DLL, passing the start location, end location, how far apart to sample. The false is an
            // internal configuration value.
            double[] terrainProfile = terrain.GetPathElevationITM(txLocation, rxLocation, terrainSpacingm, false);

            // The terrain profile also has some metadata embedded in it. The first element is the number of
            // points /elements. This value will equal terrainProfile.Length - 2. The second element will be the
            // terrain spacing that was passed to generate the profile (terrainSpacingm in this case)
            int numberOfPoints = (int) (terrainProfile[0]);

            for (int i = 2; i < terrainProfile.Length; i++)
            {
                // The terrain data starts at index 2 because of the metadata at the front of the array. So, we
                // offset by two to get the actual distance away
                double distanceAwayM = (i - 2) * terrainSpacingm;

                Console.WriteLine("Height at {0}m away from transmitter is {1}m", distanceAwayM, terrainProfile[i]);
            }

            Console.ReadKey();
        }
    }
    
}
