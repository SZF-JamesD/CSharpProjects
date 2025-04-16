using System;


namespace Ex0901.Helpers
{
    public class MapTileHelper
    {
        public static (int x, int y) LatLonToTile(double lat, double lon, int zoom)
        {
            var x_tile = (int)(Math.Floor((lon + 180.0) / 360.0 * (1 << zoom)));

            var latRad = lat / 180 * Math.PI;
            var y_tile = (int)Math.Floor((1 - Math.Log(Math.Tan(latRad) + 1 / Math.Cos(latRad)) / Math.PI) / 2 * (1 << zoom));
            return (x_tile, y_tile);
        }                
    }
}
