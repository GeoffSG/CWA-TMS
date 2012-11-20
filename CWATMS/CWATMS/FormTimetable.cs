﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CWATMS.Controls;


namespace CWATMS
{
    public partial class FormTimetable : Form
    {
        private Timetable m_timetable;

        public FormTimetable(Timetable timetable)
        {
            m_timetable = timetable;
            InitializeComponent();
            AddGrids();
        }

        public FormTimetable()
        {
            InitializeComponent();
            AddGrids();
        }

        private void FormTimetable_Load(object sender, EventArgs e)
        {
        }

        private void AddGrids()
        {
            //  Create Grids
            int index = 0;
            for (int i = 0; i < this.tlpTimetable.RowCount; i++)
            {
                for (int j = 0; j < this.tlpTimetable.ColumnCount; j++)
                {
                    GridPanel g = new GridPanel(i, j, Color.Empty);
                    g.Name = "G" + i.ToString() + j.ToString();
                    g.Text = g.Name;
                    g.DragOver += Panel_DragOver;
                    g.DragDrop += Panel_DragDrop;
                    this.tlpTimetable.Controls.Add(g, j, i);
                    index++;
                }
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (sender is GridPanel)
                {
                    GridPanel curLesson = sender as GridPanel;
                    curLesson.DoDragDrop(sender, DragDropEffects.Copy);
                }
            }
        }

        private void Panel_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            GridPanel gPanel = (GridPanel)sender;
            if (e.Data.GetData(typeof(Lecturer)) is Lecturer)
            {
                Lecturer lect = (Lecturer)e.Data.GetData(typeof(Lecturer));
                gPanel.LessonData.Lecturer = lect;
                gPanel.BackColor = lect.Colour;
            }
            else if (e.Data.GetData(typeof(Module)) is Module)
            {
                Module mod = (Module)e.Data.GetData(typeof(Module));
                gPanel.LessonData.Module = mod;
                //gPanel.BackColor = mod.Colour;
            }
            gPanel.UpdateText();
        }

        private void ChangeColours(int i)
        {
            switch (i)
            {
                case 0:
                    foreach (Control c in tlpTimetable.Controls)
                    {
                        if (c is GridPanel)
                        {
                            GridPanel gp = (GridPanel)c;
                            if(gp.LessonData.Lecturer != null)
                                gp.BackColor = gp.LessonData.Lecturer.Colour;
                            else
                                gp.BackColor = Color.Empty;
                        }
                    }
                    break;
                case 1:
                    foreach (Control c in tlpTimetable.Controls)
                    {
                        if (c is GridPanel)
                        {
                            GridPanel gp = (GridPanel)c;
                            if (gp.LessonData.Module != null)
                                gp.BackColor = gp.LessonData.Module.Colour;
                            else
                                gp.BackColor = Color.Empty;

                        }
                    }
                    break;
                case 2:
                    foreach (Control c in tlpTimetable.Controls)
                    {
                        if (c is GridPanel)
                        {
                            GridPanel gp = (GridPanel)c;
                            if (gp.LessonData.Room != null)
                                gp.BackColor = gp.LessonData.Room.Colour;
                            else
                                gp.BackColor = Color.Empty;
                        }
                    }
                    break;
                case 3:
                    foreach (Control c in tlpTimetable.Controls)
                    {
                        if (c is GridPanel)
                        {
                            GridPanel gp = (GridPanel)c;
                            if (gp.LessonData.Group != null)
                                gp.BackColor = gp.LessonData.Group.Colour;
                            else
                                gp.BackColor = Color.Empty;
                        }
                    }
                    break;
                default:
                    foreach (Control c in tlpTimetable.Controls)
                    {
                        if (c is GridPanel)
                        {
                            GridPanel gp = (GridPanel)c;
                            gp.BackColor = Color.Empty;
                        }
                    }
                    break;

            }
        }

        private void btnLecturer_Click(object sender, EventArgs e)
        {
            ChangeColours(0);
        }

        private void btnModule_Click(object sender, EventArgs e)
        {
            ChangeColours(1);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            ChangeColours(2);
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            ChangeColours(3);
        }

    }

}