using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMaze.Core.Enums;

namespace TheMaze.WinForms.Mappers
{
    public static class WinImageMapper
    {
        public static Image GetCellImageByType(FieldTypes fieldType)
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            Image image = null;
            switch (fieldType)
            {
                case FieldTypes.Wall:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/wall.png");
                    break;
                case FieldTypes.ClosedDoor:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/closeddoor.png");
                    break;
                case FieldTypes.OpenedDoor:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/opendoor.png");
                    break;
                case FieldTypes.Route:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/route.png");
                    break;
                case FieldTypes.Coin:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/coin.png");
                    break;
                case FieldTypes.Key:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/key.png");
                    break;
                case FieldTypes.Trap:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/trap.png");
                    break;
                case FieldTypes.DeadlyTrap:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/trap.png");
                    break;
                case FieldTypes.Portal:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/portal.png");
                    break;
                case FieldTypes.Prize:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/prize.png");
                    break;
                case FieldTypes.Crystal:
                    image = Image.FromFile($"{projectDirectory}/Resources/Images/crystal.png");
                    break;
            }

            return image;
        }
    }
}
