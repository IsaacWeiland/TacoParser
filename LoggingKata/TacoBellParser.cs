using System.IO;
using System.Linq;
using GeoCoordinatePortable;

namespace LoggingKata;

public class TacoBellParser
{
    static readonly ILog logger = new TacoLogger();
    const string csvPath = "TacoBell-US-AL.csv";

    public static void Run()
    {
        logger.LogInfo("Log initialized");
        var lines = File.ReadAllLines(csvPath);
        logger.LogInfo($"Lines: {lines[0]}");
        var parser = new TacoParser();
        var locations = lines.Select(parser.Parse).ToArray();
        ITrackable farBell1 = null;
        ITrackable farBell2 = null;
        double distance = 0;
        for (int i = 0; i < locations.Length; i++)
        {
            var locA = locations[i];
            var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
            for (int j = 0; j < locations.Length; j++)
            {
                var locB = locations[j];
                var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                double distanceCheck = corA.GetDistanceTo(corB);
                if (distanceCheck > distance)
                {
                    distance = distanceCheck;
                    farBell1 = locA;
                    farBell2 = locB;
                    logger.LogInfo("Updated distance.");
                }
            }
            logger.LogInfo($"{i+1} checks completed.");
        }
        logger.LogInfo(
            $"The two Taco Bells that are farthest away from each other are:\n{farBell1.Name} at {farBell1.Location.Latitude}, {farBell1.Location.Longitude}" +
            $"\n{farBell2.Name} at {farBell2.Location.Latitude}, {farBell2.Location.Longitude}");
    }
}