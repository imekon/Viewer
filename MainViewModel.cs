using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using MoonSharp.Interpreter;

namespace Viewer
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DelegateCommand openCommand;
        private readonly DelegateCommand exitCommand;
        private readonly DelegateCommand aboutCommand;

        private readonly Script script;

        private List<Drawing> drawings;

        private readonly Model3DGroup model;

        public Model3DGroup Model => model;

        public DelegateCommand OpenCommand => openCommand;
        public DelegateCommand ExitCommand => exitCommand;
        public DelegateCommand AboutCommand => aboutCommand;

        public MainViewModel()
        {
            openCommand = new DelegateCommand(OnFileOpen);
            exitCommand = new DelegateCommand(OnFileExit);
            aboutCommand = new DelegateCommand(OnHelpAbout);

            script = new Script();
            script.Globals["ClearDrawings"] = (Action)ClearDrawings;
            script.Globals["CreateDrawing"] = (Action<List<List<List<double>>>>)CreateDrawing;

            drawings = new List<Drawing>();

            model = new Model3DGroup();
        }

        private void ClearDrawings()
        {
            drawings.Clear();
            BuildPolygons();
        }

        private void CreateDrawing(List<List<List<double>>> table)
        {
            int count = 1;

            var surfaces = new List<Surface>();

            foreach (var surface in table)
            {
                var points = new List<Point3D>();
                foreach (var point in surface)
                {
                    var coords = new List<double>();

                    foreach (var coord in point)
                    {
                        coords.Add(coord);
                    }

                    var pt = new Point3D(coords[0], coords[1], coords[2]);
                    points.Add(pt);
                }

                var surf = new Surface(count, points.ToArray());
                surfaces.Add(surf);

                count++;
            }

            var drawing = new Drawing(surfaces.ToArray());

            drawings.Add(drawing);
            BuildPolygons();
        }

        private void BuildPolygons()
        {
            model.Children.Clear();

            var redMaterial = MaterialHelper.CreateMaterial(Colors.Red);
            var yellowMaterial = MaterialHelper.CreateMaterial(Colors.Yellow);

            foreach (var drawing in drawings)
            {
                var meshBuilder = new MeshBuilder(false, false);

                foreach (var surface in drawing.Surfaces)
                {
                    meshBuilder.AddPolygon(surface.Points);
                }

                var mesh = meshBuilder.ToMesh(true);

                Model.Children.Add(new GeometryModel3D
                {
                    Geometry = mesh,
                    Material = redMaterial,
                    BackMaterial = yellowMaterial
                });
            }
        }

        private void OnFileOpen(object obj)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "LUA script (*.lua)|*.lua"
            };

            if (dialog.ShowDialog() == true)
            {
                var text = File.ReadAllText(dialog.FileName);
                script.DoString(text);
            }
        }

        private void OnFileExit(object obj)
        {
            Application.Current.Shutdown();
        }

        private void OnHelpAbout(object obj)
        {
            var dialog = new AboutWindow();
            dialog.ShowDialog();
        }
    }
}