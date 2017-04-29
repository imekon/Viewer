using System.Windows.Media.Media3D;

namespace Viewer
{
    public class Surface
    {
        private int id;
        private Point3D[] points;

        public Surface(int id, Point3D[] points)
        {
            this.id = id;
            this.points = points;
        }

        public int Id => id;
        public Point3D[] Points => points;
    }
}