namespace Viewer
{
    public class Drawing
    {
        private Surface[] surfaces;

        public Drawing(Surface[] surfaces)
        {
            this.surfaces = surfaces;
        }

        public Surface[] Surfaces => surfaces;
    }
}