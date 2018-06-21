using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Японский_кроссворд
{
    class StatusObject
    {
        private short value = 0;

        //Установка состояния
        public StatusObject setNone()
        {
            value = 0;
            return this;
        }

        //Установка состояния
        public StatusObject setBalck()
        {
            value = 1;
            return this;
        }


        //Установка состояния
        public StatusObject setWhite()
        {
            value = 2;
            return this;
        }

        //Установленна ли структура в состояние None
        public bool isNone()
        {
            return value == 0;
        }

        //Установленна ли структура в состояние Black
        public bool isBlack()
        {
            return value == 1;
        }

        //Установленна ли структура в состояние White
        public bool isWhite()
        {
            return value == 2;
        }

    }
}
