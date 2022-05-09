namespace WinFormsLab
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.editionGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonClearGraph = new System.Windows.Forms.Button();
            this.buttonDeleteVertex = new System.Windows.Forms.Button();
            this.colorBox = new System.Windows.Forms.PictureBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.langGroupBox = new System.Windows.Forms.GroupBox();
            this.AngielskiButton = new System.Windows.Forms.Button();
            this.PolskiButton = new System.Windows.Forms.Button();
            this.ImpExpGroupBox = new System.Windows.Forms.GroupBox();
            this.WczytajButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.editionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
            this.langGroupBox.SuspendLayout();
            this.ImpExpGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Canvas, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.editionGroupBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.langGroupBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ImpExpGroupBox, 0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // editionGroupBox
            // 
            this.editionGroupBox.Controls.Add(this.buttonClearGraph);
            this.editionGroupBox.Controls.Add(this.buttonDeleteVertex);
            this.editionGroupBox.Controls.Add(this.colorBox);
            this.editionGroupBox.Controls.Add(this.colorButton);
            resources.ApplyResources(this.editionGroupBox, "editionGroupBox");
            this.editionGroupBox.Name = "editionGroupBox";
            this.editionGroupBox.TabStop = false;
            // 
            // buttonClearGraph
            // 
            resources.ApplyResources(this.buttonClearGraph, "buttonClearGraph");
            this.buttonClearGraph.Name = "buttonClearGraph";
            this.buttonClearGraph.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteVertex
            // 
            resources.ApplyResources(this.buttonDeleteVertex, "buttonDeleteVertex");
            this.buttonDeleteVertex.Name = "buttonDeleteVertex";
            this.buttonDeleteVertex.UseVisualStyleBackColor = true;
            this.buttonDeleteVertex.Click += new System.EventHandler(this.buttonDeleteVertex_Click);
            // 
            // colorBox
            // 
            resources.ApplyResources(this.colorBox, "colorBox");
            this.colorBox.Name = "colorBox";
            this.colorBox.TabStop = false;
            this.colorBox.Paint += new System.Windows.Forms.PaintEventHandler(this.colorBox_Paint);
            // 
            // colorButton
            // 
            resources.ApplyResources(this.colorButton, "colorButton");
            this.colorButton.Name = "colorButton";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // langGroupBox
            // 
            this.langGroupBox.Controls.Add(this.AngielskiButton);
            this.langGroupBox.Controls.Add(this.PolskiButton);
            resources.ApplyResources(this.langGroupBox, "langGroupBox");
            this.langGroupBox.Name = "langGroupBox";
            this.langGroupBox.TabStop = false;
            // 
            // AngielskiButton
            // 
            resources.ApplyResources(this.AngielskiButton, "AngielskiButton");
            this.AngielskiButton.Name = "AngielskiButton";
            this.AngielskiButton.UseVisualStyleBackColor = true;
            this.AngielskiButton.Click += new System.EventHandler(this.AngielskiButton_Click);
            // 
            // PolskiButton
            // 
            resources.ApplyResources(this.PolskiButton, "PolskiButton");
            this.PolskiButton.Name = "PolskiButton";
            this.PolskiButton.UseVisualStyleBackColor = true;
            this.PolskiButton.Click += new System.EventHandler(this.PolskiButton_Click);
            // 
            // ImpExpGroupBox
            // 
            this.ImpExpGroupBox.Controls.Add(this.WczytajButton);
            this.ImpExpGroupBox.Controls.Add(this.SaveButton);
            resources.ApplyResources(this.ImpExpGroupBox, "ImpExpGroupBox");
            this.ImpExpGroupBox.Name = "ImpExpGroupBox";
            this.ImpExpGroupBox.TabStop = false;
            // 
            // WczytajButton
            // 
            resources.ApplyResources(this.WczytajButton, "WczytajButton");
            this.WczytajButton.Name = "WczytajButton";
            this.WczytajButton.UseVisualStyleBackColor = true;
            this.WczytajButton.Click += new System.EventHandler(this.WczytajButton_Click);
            // 
            // SaveButton
            // 
            resources.ApplyResources(this.SaveButton, "SaveButton");
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Canvas
            // 
            resources.ApplyResources(this.Canvas, "Canvas");
            this.Canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.Canvas.Name = "Canvas";
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.editionGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
            this.langGroupBox.ResumeLayout(false);
            this.ImpExpGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox editionGroupBox;
        private GroupBox langGroupBox;
        private Button AngielskiButton;
        private Button PolskiButton;
        private GroupBox ImpExpGroupBox;
        private Button WczytajButton;
        private Button SaveButton;
        private PictureBox Canvas;
        private Button colorButton;
        private PictureBox colorBox;
        private Button buttonClearGraph;
        private Button buttonDeleteVertex;
    }
}