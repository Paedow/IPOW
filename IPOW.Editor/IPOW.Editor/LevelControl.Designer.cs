namespace IPOW.Editor
{
    partial class LevelControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.xScroll = new System.Windows.Forms.HScrollBar();
            this.yScroll = new System.Windows.Forms.VScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.container = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xScroll
            // 
            this.xScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xScroll.Location = new System.Drawing.Point(0, 0);
            this.xScroll.Name = "xScroll";
            this.xScroll.Size = new System.Drawing.Size(685, 24);
            this.xScroll.TabIndex = 0;
            // 
            // yScroll
            // 
            this.yScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.yScroll.Location = new System.Drawing.Point(685, 0);
            this.yScroll.Name = "yScroll";
            this.yScroll.Size = new System.Drawing.Size(24, 423);
            this.yScroll.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xScroll);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 24);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(685, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(24, 24);
            this.panel2.TabIndex = 3;
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(685, 423);
            this.container.TabIndex = 3;
            // 
            // LevelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.container);
            this.Controls.Add(this.yScroll);
            this.Controls.Add(this.panel1);
            this.Name = "LevelControl";
            this.Size = new System.Drawing.Size(709, 447);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar xScroll;
        private System.Windows.Forms.VScrollBar yScroll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel container;
    }
}
