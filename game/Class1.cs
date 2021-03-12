using System;
using System.Drawing;
using System.Windows.Forms;
namespace game
{
    public class RectMov : Control
    {
        public RectMov()
        {
            Size = new Size(400,400);
            CreateData();
        }
        protected int _Step;
        protected int[,] _Data;
        protected bool _FlagFirstCell = false;
        protected bool _FlagTwoCell = false;
        private Point PointOne = new Point(0, 0);
        private Point PointTwo = new Point(0, 0);
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _Step = Size.Height / 4;
            Size = new Size(Size.Height, Size.Height);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.White);
            SolidBrush brush;
            for (int row = 0; row < _Data.GetLength(0); row++)
                for (int col = 0; col < _Data.GetLength(1); col++) 
                {
                    if (_Data[col, row] > 0)
                        brush = new SolidBrush(Color.Green);
                    else
                        brush = new SolidBrush(Color.White);
                    graphics.DrawLine(new Pen(Color.Red), col * _Step, 0, col * _Step, Size.Height);
                    graphics.DrawLine(new Pen(Color.Red), 0, col * _Step, Size.Width, col * _Step);
                    graphics.FillRectangle(brush, row * _Step + 1, col * _Step + 1, _Step - 1, _Step - 1);
                    graphics.DrawString(_Data[col, row].ToString(), new Font("Arial", 35), new SolidBrush(Color.Red), row * _Step, col * _Step);
                }
            graphics.DrawLine(new Pen(Color.Red), Size.Width-1, 0, Size.Width-1, Size.Height-1);
            graphics.DrawLine(new Pen(Color.Red), 0, Size.Height-1, Size.Width-1, Size.Height-1);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Point Cell =new Point( e.X / _Step,e.Y / _Step);
            if (_FlagFirstCell && _FlagTwoCell == false && _Data[Cell.Y, Cell.X] == 0 && (
               (Cell.Y - PointOne.Y == 0 && Math.Abs(Cell.X - PointOne.X) == 1)||
               (Cell.X - PointOne.X == 0 && Math.Abs(Cell.Y - PointOne.Y) == 1)))
            {
                _FlagTwoCell = true;
                PointTwo = Cell;
            }
            if (!_FlagFirstCell && _Data[Cell.Y, Cell.X] != 0)
            {
                _FlagFirstCell = true;
                PointOne = Cell;
            }else
                if(Cell == PointOne)
                    _FlagFirstCell = false;
            if(_FlagTwoCell && _FlagFirstCell)
            {
                (_Data[PointOne.Y, PointOne.X], _Data[PointTwo.Y, PointTwo.X]) = (_Data[PointTwo.Y, PointTwo.X], _Data[PointOne.Y, PointOne.X]);
                _FlagFirstCell = false;
                _FlagTwoCell = false;
                Invalidate();
            }
        }
        public virtual int this[int row, int col]
        {
            get => _Data[row, col];
            set => _Data[row, col] = value;
        }
        protected void CreateData()
        {
            if (_Data == null)
                _Data = new int[4, 4];
            int cnt = 1;
            for (int row = 0; row < _Data.GetLength(0); row++)
                for (int col = 0; col < _Data.GetLength(1); col++)
                    _Data[row, col] = cnt++;
            _Data[3, 1] = 15;
            _Data[3, 2] = 14;
            _Data[3, 3] = 0;
        }
    }
}