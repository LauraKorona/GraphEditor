using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace WinFormsLab
{
    public partial class Form1 : Form
    {
        private Bitmap drawArea;
        private const int radius = 20;
        private Pen pen;
        private Pen dashedPen;
        private bool VertexMoving = false;

        //Xml Serialization
        public void XmlSerialize(Type dataType,object data,string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(dataType);
            if (File.Exists(filePath)) File.Delete(filePath);
            TextWriter writer = new StreamWriter(filePath);
            xmlSerializer.Serialize(writer, data);
            writer.Close();
        }
        public object? XmlDeserialize(Type dataType, string filePath)
        {
            object? obj = null;
            XmlSerializer xmlSerializer = new XmlSerializer(dataType);
            if (File.Exists(filePath))
            {
                TextReader reader = new StreamReader(filePath);
                obj = xmlSerializer.Deserialize(reader);
                reader.Close();
            }
            return obj;
        }
        [Serializable]
        public class Graph
        {
            public List<Vertex>? vertices;
            public List<List<Vertex>>? edges;
            public Graph() { }
            public Graph(List<Vertex> v, List<List<Vertex>> e)
            {
                vertices = v;
                edges = e;
            }
        }

        [Serializable]
        public class Vertex
        {
            public int x { get; set; }
            public int y { get; set; }
            public Color color { get; set; }
            public int colorIndex
            {
                get => color.ToArgb();
                set => color = Color.FromArgb(value);
            }
            public Vertex(Point p, Color c)
            {
                x = p.X;
                y = p.Y;
                color = c;
            }
            public Vertex() { }
            public bool IsEqual(Vertex w)
            {
                return w!=null && this.x==w.x && this.y==w.y;
            }
        }
        private Vertex? chosenVertex = null;
        private List<Vertex> vertices;
        private List<List<Vertex>> edges;
        public Form1()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            InitializeComponent();
            this.KeyPreview = true;
            vertices = new List<Vertex>();
            edges = new List<List<Vertex>>();
            pen = new Pen(Brushes.Black, 3);
            dashedPen = new Pen(Brushes.Black, 3);
            dashedPen.DashPattern = new float[] { 2, 1 };
            drawArea = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            Canvas.Image = drawArea;
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.White);
            }
        }

        private int GetVertexIndex(Vertex v)
        {
            for(int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].IsEqual(v) == true) return i;
            }
            return -1; //no such vertex
        }

        private bool VerticesCollide(Point m1, Point m2) //checks for collision
        {
            return Math.Abs(m1.X-m2.X) < radius*2 && Math.Abs(m1.Y - m2.Y) < radius * 2;
        }

        private bool EdgeExists(Vertex a, Vertex b)
        {
            for(int i=0;i<edges[GetVertexIndex(a)].Count;i++)
            {
                if (edges[GetVertexIndex(a)][i] == b) return true;
            }
            for (int i = 0; i < edges[GetVertexIndex(b)].Count; i++)
            {
                if (edges[GetVertexIndex(b)][i] == a) return true;
            }
            return false;
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (vertices.Count > 0)
                {
                    //check if new potential vertex doesnt collide with the others
                    bool Collides = false;
                    int tmp = -1;
                    for (int i = 0; i < vertices.Count; i++)
                    {
                        if (VerticesCollide(new Point(e.X, e.Y), new Point(vertices[i].x,vertices[i].y)) == true)
                        {
                            Collides = true;
                            tmp = i; //indeks kliknietego wierzcholka
                        }
                    }
                    if (Collides == true) //if user clicked on a vertex
                    {
                        if(chosenVertex != null && !chosenVertex.IsEqual(vertices[tmp]))//adds edge
                        {
                            int indexChosen = GetVertexIndex(chosenVertex);
                            if (!EdgeExists(chosenVertex, vertices[tmp])) //add edge
                            {
                                edges[indexChosen].Add(vertices[tmp]);
                                edges[tmp].Add(chosenVertex);
                                

                                Canvas.Invalidate();
                                Canvas.Update();
                                Canvas.Refresh();
                            }
                            else //remove edge
                            {
                                if(edges[tmp].Contains(chosenVertex)) edges[tmp].Remove(chosenVertex);
                                if (edges[indexChosen].Contains(vertices[tmp])) edges[indexChosen].Remove(vertices[tmp]);
                                

                                Canvas.Invalidate();
                                Canvas.Update();
                                Canvas.Refresh();
                            }
                        }
                        return;
                    }
                }
                vertices.Add(new Vertex(new Point(e.X, e.Y),pen.Color)); //adds new vertex
                edges.Add(new List<Vertex>());
                using(Graphics g = Graphics.FromImage(drawArea))
                {
                    //source : https://stackoverflow.com/questions/52745317/drawing-a-text-in-the-middle-of-an-ellipse
                    g.FillEllipse(new SolidBrush(Color.White), e.X - radius, e.Y - radius, 2 * radius, 2 * radius);
                    g.DrawEllipse(pen, e.X-radius, e.Y-radius, radius*2, radius*2);
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString($"{vertices.Count}", this.Font, new SolidBrush(pen.Color),
                                            e.X, e.Y, sf);
                    }
                }
                Canvas.Refresh();
            }
            else if(e.Button == MouseButtons.Right)
            {
                if (vertices.Count > 0)
                {
                    //check if chosen point collides with a vertex
                    bool collides = false;
                    for (int i = 0; i < vertices.Count; i++)
                    {
                        if (VerticesCollide(new Point(e.X, e.Y), new Point(vertices[i].x,vertices[i].y)) == true)
                        {
                            chosenVertex = vertices[i];
                            collides = true;
                            buttonDeleteVertex.Enabled = true;
                        }      
                    }
                    if(collides==false) //clicked on background
                    {
                        chosenVertex=null;
                        buttonDeleteVertex.Enabled = false;
                        
                    }
                    drawArea.Dispose();
                    drawArea = new Bitmap(Canvas.Parent.Width, Canvas.Parent.Height);
                    Canvas.Image = drawArea;
                    using (Graphics g = Graphics.FromImage(drawArea))
                    {
                        g.Clear(Color.White);
                    }
                    for (int i = 0; i < vertices.Count; i++)
                    {
                        using (Graphics g = Graphics.FromImage(drawArea))
                        {
                            Pen tmp = new Pen(new SolidBrush(vertices[i].color), 3);
                            if (chosenVertex!=null && vertices[i].IsEqual(chosenVertex) == true)
                            {
                                tmp.DashPattern = new float[] { 2, 1 };
                            }
                            g.FillEllipse(new SolidBrush(Color.White), vertices[i].x - radius, vertices[i].y - radius, 2 * radius, 2 * radius);
                            g.DrawEllipse(tmp, vertices[i].x - radius, vertices[i].y - radius, radius * 2, radius * 2);
                            using (StringFormat sf = new StringFormat())
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;

                                g.DrawString($"{i + 1}", this.Font, new SolidBrush(vertices[i].color),
                                                    vertices[i].x, vertices[i].y, sf);
                            }
                        }
                        Canvas.Refresh();
                    }

                }
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                dashedPen.Color = colorDialog1.Color;
                colorBox.BackColor = pen.Color;
                drawArea.Dispose();
                drawArea = new Bitmap(Canvas.Parent.Width, Canvas.Parent.Height);
                Canvas.Image = drawArea;
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    g.Clear(Color.White);
                }
                for (int i = 0; i < vertices.Count; i++)
                {
                    using (Graphics g = Graphics.FromImage(drawArea))
                    {
                        //source : https://stackoverflow.com/questions/52745317/drawing-a-text-in-the-middle-of-an-ellipse
                        Pen tmp = new Pen(new SolidBrush(vertices[i].color), 3);
                        if (chosenVertex != null)
                        {
                            if (vertices[i].IsEqual(chosenVertex)) tmp.Color = colorDialog1.Color;
                            chosenVertex.color = colorDialog1.Color;
                        }
                        g.FillEllipse(new SolidBrush(Color.White), vertices[i].x - radius, vertices[i].y - radius, 2 * radius, 2 * radius);
                        g.DrawEllipse(tmp, vertices[i].x - radius, vertices[i].y - radius, radius * 2, radius * 2);
                        using (StringFormat sf = new StringFormat())
                        {
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;

                            g.DrawString($"{i + 1}", this.Font, new SolidBrush(tmp.Color),
                                                vertices[i].x, vertices[i].y, sf);
                        }
                    }
                    Canvas.Refresh();
                }
            }
        }

        private void colorBox_Paint(object sender, PaintEventArgs e)
        {
            colorBox.BackColor = pen.Color;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //deletes bitmap and creates new one with proper size
            drawArea.Dispose();
            drawArea = new Bitmap(Canvas.Parent.Width,Canvas.Parent.Height);
            Canvas.Image = drawArea;

            Canvas.Invalidate();
            Canvas.Update();
            Canvas.Refresh();
        }
        private void AngielskiButton_Click(object sender, EventArgs e)
        {
            //source: https://stackoverflow.com/questions/7556367/how-do-i-change-the-culture-of-a-winforms-application-at-runtime
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyAllResources(resources, this.Controls);

        }
        private void applyAllResources(ComponentResourceManager resources, Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                resources.ApplyResources(ctl, ctl.Name);
                applyAllResources(resources, ctl.Controls);
            }
        }

        private void PolskiButton_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
            resources.ApplyResources(this, "$this");
            applyAllResources(resources, this.Controls);
        }

        private void buttonDeleteVertex_Click(object sender, EventArgs e)
        {
            if (chosenVertex != null)
            {
                int k = 0;
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (vertices[i].IsEqual(chosenVertex) == true) k = i;
                }
                DeleteVertex(k);
                chosenVertex = null;
                buttonDeleteVertex.Enabled = false;

                Canvas.Invalidate();
                Canvas.Update();
                Canvas.Refresh();
            }
        }

        private void DeleteVertex(int k)
        {
            edges.Remove(edges[k]);
            for(int i=0;i<edges.Count;i++)
            {
                if(edges[i].Contains(vertices[k])) edges[i].Remove(vertices[k]);
            }
            vertices.Remove(vertices[k]);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.White);
            }

            for (int i=0;i<edges.Count;i++)
            {
                for (int j = 0; j < edges[i].Count; j++)
                {
                    using (Graphics g = Graphics.FromImage(drawArea))
                    {
                        Pen blackPen = new Pen(Color.Black, 3);
                        e.Graphics.DrawLine(blackPen, new Point(vertices[i].x,vertices[i].y), new Point(edges[i][j].x,edges[i][j].y));
                    }
                }
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    Pen tmp = new Pen(new SolidBrush(vertices[i].color), 3);
                    if (chosenVertex != null && vertices[i].IsEqual(chosenVertex) == true)
                    {
                        tmp.DashPattern = new float[] { 2, 1 };
                    }
                    g.DrawEllipse(tmp, vertices[i].x - radius, vertices[i].y - radius, radius * 2, radius * 2);
                    g.FillEllipse(new SolidBrush(Color.White), vertices[i].x-radius, vertices[i].y-radius, 2 * radius, 2 * radius);
                    //source : https://stackoverflow.com/questions/52745317/drawing-a-text-in-the-middle-of-an-ellipse
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString($"{i + 1}", this.Font, new SolidBrush(vertices[i].color),
                                            vertices[i].x, vertices[i].y, sf);
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (chosenVertex != null && e.KeyCode == Keys.Delete)
            {
                int k = 0;
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (vertices[i].IsEqual(chosenVertex) == true) k = i;
                }
                DeleteVertex(k);
                chosenVertex = null;
                buttonDeleteVertex.Enabled = false;

                Canvas.Invalidate();
                Canvas.Update();
                Canvas.Refresh();
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Middle)
            {
                VertexMoving = true;
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                VertexMoving = false;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(chosenVertex != null && VertexMoving==true)
            {
                vertices[GetVertexIndex(chosenVertex)].x = e.X;
                vertices[GetVertexIndex(chosenVertex)].y = e.Y;

                Canvas.Invalidate();
                Canvas.Update();
                Canvas.Refresh();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Title = "Save graph";
                opf.DefaultExt = "graph";
                opf.RestoreDirectory = true;
                opf.CheckFileExists = false;
                opf.CheckPathExists = true;
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    //serialization
                    try
                    {
                        Graph g = new Graph(vertices, edges);
                        g.vertices = vertices;
                        g.edges = edges;
                        XmlSerialize(g.GetType(), g, opf.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("An error has occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void WczytajButton_Click(object sender, EventArgs e)
        {
            var opf = new OpenFileDialog();
            opf.Filter = "Graph files (*.graph) | *.graph";
            if(opf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Graph? graph = new Graph();
                    graph = (Graph?)XmlDeserialize(graph.GetType(), opf.FileName);
                    if (graph != null)
                    {
                        vertices = graph.vertices;
                        edges = graph.edges;
                    }

                }
                catch
                {
                    MessageBox.Show("ErrorLoadMessage","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                Canvas.Invalidate();
                Canvas.Update();
                Canvas.Refresh();
            }
        }
    }
}