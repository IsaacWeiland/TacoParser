namespace LoggingKata
{
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");
            var cells = line.Split(',');
            if (cells.Length < 3)
            {
                return null; 
            }
            var lati = double.Parse(cells[0]);
            var longi = double.Parse(cells[1]);
            var bellName = cells[2];
            var locationPoint = new Point()
            {
                Latitude = lati,
                Longitude = longi
            };
            var tacoBell = new TacoBell()
            {
                Location = locationPoint,
                Name = bellName
            };
            return tacoBell;
        }
    }
}
