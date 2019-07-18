namespace OpenGLPlatform.Inspector
{
    partial class InspectorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ObjectInspector = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // ObjectInspector
            // 
            this.ObjectInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ObjectInspector.Location = new System.Drawing.Point(0, 0);
            this.ObjectInspector.Name = "ObjectInspector";
            this.ObjectInspector.Size = new System.Drawing.Size(800, 450);
            this.ObjectInspector.TabIndex = 0;
            // 
            // InspectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ObjectInspector);
            this.Name = "InspectorForm";
            this.Text = "InspectorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid ObjectInspector;
    }
}