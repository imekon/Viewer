namespace Viewer
{
    public class Drawing
    {
        private Surface[] surfaces;
        private int group, number;

        public Drawing(int group, int number, Surface[] surfaces)
        {
            this.group = group;
            this.number = number;
            this.surfaces = surfaces;
        }

        public Surface[] Surfaces => surfaces;
    }
}