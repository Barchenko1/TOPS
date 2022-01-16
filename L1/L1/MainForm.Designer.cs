
namespace L1
{
    partial class MainForm
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
            this.m_run = new System.Windows.Forms.Button();
            this.m_list = new System.Windows.Forms.ListView();
            this.m_columnNum = new System.Windows.Forms.ColumnHeader();
            this.m_columnDimension = new System.Windows.Forms.ColumnHeader();
            this.m_columnCalculationPeriod = new System.Windows.Forms.ColumnHeader();
            this.m_columnCalculationSteps = new System.Windows.Forms.ColumnHeader();
            this.m_columnMinF = new System.Windows.Forms.ColumnHeader();
            this.m_columnMinX = new System.Windows.Forms.ColumnHeader();
            this.m_columnValid = new System.Windows.Forms.ColumnHeader();
            this.m_graphBox = new System.Windows.Forms.PictureBox();
            this.m_approxList = new System.Windows.Forms.ListView();
            this.m_approxColumnNumber = new System.Windows.Forms.ColumnHeader();
            this.m_approxColumnA = new System.Windows.Forms.ColumnHeader();
            this.m_litsPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.m_graphBox)).BeginInit();
            this.m_litsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_run
            // 
            this.m_run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_run.Location = new System.Drawing.Point(694, 452);
            this.m_run.Name = "m_run";
            this.m_run.Size = new System.Drawing.Size(94, 29);
            this.m_run.TabIndex = 0;
            this.m_run.Text = "Run";
            this.m_run.UseVisualStyleBackColor = true;
            this.m_run.Click += new System.EventHandler(this.OnRun);
            // 
            // m_list
            // 
            this.m_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_columnNum,
            this.m_columnDimension,
            this.m_columnCalculationPeriod,
            this.m_columnCalculationSteps,
            this.m_columnMinF,
            this.m_columnMinX,
            this.m_columnValid});
            this.m_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_list.HideSelection = false;
            this.m_list.Location = new System.Drawing.Point(3, 3);
            this.m_list.Name = "m_list";
            this.m_list.Size = new System.Drawing.Size(467, 157);
            this.m_list.TabIndex = 1;
            this.m_list.UseCompatibleStateImageBehavior = false;
            this.m_list.View = System.Windows.Forms.View.Details;
            // 
            // m_columnNum
            // 
            this.m_columnNum.Text = "#";
            this.m_columnNum.Width = 31;
            // 
            // m_columnDimension
            // 
            this.m_columnDimension.Text = "Dimension";
            // 
            // m_columnCalculationPeriod
            // 
            this.m_columnCalculationPeriod.Text = "Time (ms)";
            // 
            // m_columnCalculationSteps
            // 
            this.m_columnCalculationSteps.Text = "Steps";
            // 
            // m_columnMinF
            // 
            this.m_columnMinF.Text = "F(min)";
            // 
            // m_columnMinX
            // 
            this.m_columnMinX.DisplayIndex = 6;
            this.m_columnMinX.Text = "X(min)";
            // 
            // m_columnValid
            // 
            this.m_columnValid.DisplayIndex = 5;
            this.m_columnValid.Text = "Succeeded";
            // 
            // m_graphBox
            // 
            this.m_graphBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_graphBox.Location = new System.Drawing.Point(12, 12);
            this.m_graphBox.Name = "m_graphBox";
            this.m_graphBox.Size = new System.Drawing.Size(776, 300);
            this.m_graphBox.TabIndex = 2;
            this.m_graphBox.TabStop = false;
            this.m_graphBox.SizeChanged += new System.EventHandler(this.OnGraphSize);
            // 
            // m_approxList
            // 
            this.m_approxList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_approxColumnNumber,
            this.m_approxColumnA});
            this.m_approxList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_approxList.HideSelection = false;
            this.m_approxList.Location = new System.Drawing.Point(476, 3);
            this.m_approxList.Name = "m_approxList";
            this.m_approxList.Size = new System.Drawing.Size(197, 157);
            this.m_approxList.TabIndex = 3;
            this.m_approxList.UseCompatibleStateImageBehavior = false;
            this.m_approxList.View = System.Windows.Forms.View.Details;
            // 
            // m_approxColumnNumber
            // 
            this.m_approxColumnNumber.Text = "#";
            // 
            // m_approxColumnA
            // 
            this.m_approxColumnA.Text = "a(i)";
            // 
            // m_litsPanel
            // 
            this.m_litsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_litsPanel.ColumnCount = 2;
            this.m_litsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.m_litsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.m_litsPanel.Controls.Add(this.m_list, 0, 0);
            this.m_litsPanel.Controls.Add(this.m_approxList, 1, 0);
            this.m_litsPanel.Location = new System.Drawing.Point(12, 318);
            this.m_litsPanel.Name = "m_litsPanel";
            this.m_litsPanel.RowCount = 1;
            this.m_litsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_litsPanel.Size = new System.Drawing.Size(676, 163);
            this.m_litsPanel.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 493);
            this.Controls.Add(this.m_litsPanel);
            this.Controls.Add(this.m_graphBox);
            this.Controls.Add(this.m_run);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.Text = "Approx";
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.m_graphBox)).EndInit();
            this.m_litsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_run;
        private System.Windows.Forms.ListView m_list;
        private System.Windows.Forms.ColumnHeader m_columnNum;
        private System.Windows.Forms.ColumnHeader m_columnDimension;
        private System.Windows.Forms.ColumnHeader m_columnCalculationPeriod;
        private System.Windows.Forms.PictureBox m_graphBox;
        private System.Windows.Forms.ColumnHeader m_columnCalculationSteps;
        private System.Windows.Forms.ColumnHeader m_columnMinF;
        private System.Windows.Forms.ColumnHeader m_columnValid;
        private System.Windows.Forms.ListView m_approxList;
        private System.Windows.Forms.TableLayoutPanel m_litsPanel;
        private System.Windows.Forms.ColumnHeader m_approxColumnNumber;
        private System.Windows.Forms.ColumnHeader m_approxColumnA;
        private System.Windows.Forms.ColumnHeader m_columnMinX;
    }
}

