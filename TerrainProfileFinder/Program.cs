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
            double TxLocationLatitude = 42.99802;
            double TxLocationLongitude = -79.03379;
            double RxLocationLatitude = 42.99532;
            double RxLocationLongtidue = -79.0095;

            double terrainSpacingm = 90;

            double[] terrainProfile = Heights(TxLocationLatitude, TxLocationLongitude, RxLocationLatitude, RxLocationLongtidue, terrainSpacingm);

            StreamWriter sw = new StreamWriter(@"TestTerrain.csv");

            foreach (double elevation in terrainProfile)
            {
                Console.WriteLine(elevation);
                sw.WriteLine(elevation);
            }
            sw.Close();
        }
        public static double[] Heights(double TxLocationLatitude, double TxLocationLongitude, double RxLocationLatitude, double RxLocationLongtiude, double spacingm)
        {
            TerrainPcs.Terrain terrain = new TerrainPcs.USGS();
            terrain.TerrainDataPath = @"C:\USGS\";

            TerrainPcs.Geolocation txLocation = new TerrainPcs.Geolocation();
            TerrainPcs.Geolocation rxLocation = new TerrainPcs.Geolocation();

            txLocation.Lat = TxLocationLatitude;
            txLocation.Lon = TxLocationLongitude;

            rxLocation.Lat = RxLocationLatitude;
            rxLocation.Lon = RxLocationLongtiude;

            double[] heights = terrain.GetPathElevationITM(rxLocation, txLocation, spacingm, false);

            return heights;
        }
    }
    
}
