using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Японский_кроссворд
{
    class Automate
    {
        public class State
        {
            public State nextTrue;
            public State nextFalse;

            //Возвращает ссылку на следующее состояние по значению (1 или 0)
            public State get(bool value)
            {
                return value ? nextTrue : nextFalse;
            }
        }
        State start;//начальное состояние
        State end;//конечное состояние

        //Конструктор создает автомат по строке(или столбцу)
        public Automate(List<int> data, int maxSize)
        {
            int sum = -1;//Считает минимальную длинну строки
            for (int i = 0; i < data.Count; i++)
                sum += data[i] + 1;

            start = new State();
            end = start;
            State st;
            for (int i = 0; i < data.Count(); i++)
            {
                if (i != 0)
                {
                    st = new State();//Состояние с нулем 
                    st.nextFalse = st;
                    end.nextFalse = st;
                    end = st;
                }

                //Создаем столько состояний, сколько закрашенных клеток в группе
                for (int j = 0; j < data[i]; j++)
                {
                    st = new State();
                    end.nextTrue = st;
                    end = st;
                }
            }

            //Если строка из условия data не может полностью заполнить строку кроссворда, то вначале и конце нужно ставить петельки
            if (sum != maxSize)
            {
                start.nextFalse = start;
                end.nextFalse = end;
            }
        }
        //Проверка строки на существование решения
        public bool IsValid(List<StatusObject> row)
        {

            Stack<State> next, currents = new Stack<State>();
            currents.Push(start);

            for (int s = 0; s < row.Count; s++)
            {
                next = new Stack<State>();
                while (currents.Count != 0)
                {
                    if (row[s].isNone())
                    {
                        State st = currents.Pop();//Снимаем очередное состояние
                        if (st.nextFalse != null)//Если оно может перейти по ветке с нулем, то запишем это состояние
                            next.Push(st.nextFalse);
                        if (st.nextTrue != null)//Если оно может перейти по ветке с единицей, то запишем это состояние
                            next.Push(st.nextTrue);
                    }
                    else
                    {
                        //Иначе (если ячейка либо черная, либо белая)
                        State st = currents.Pop().get(row[s].isWhite());
                        if (st != null)//Если такое существует, то запишем это состояние
                            next.Push(st);
                    }
                }
                currents = next;
            }

            while (currents.Count != 0)
            {

                if (currents.Pop() == end)
                    return true;//строка соответствует условию автомата
            }

            return false;//строка не соответствует условию автомата
        }
    }
}
