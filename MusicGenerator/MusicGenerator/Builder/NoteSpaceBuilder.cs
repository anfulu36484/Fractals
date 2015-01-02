using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicGenerator.Patterns;

namespace MusicGenerator.Builder
{

    internal class NoteSpaceBuilder
    {
        private NoteSpace _noteSpace;

        private Random _random;

        public NoteSpaceBuilder(Random random)
        {
            _random = random;
        }

        #region Заполнение нотного пространства

        /// <summary>
        /// Определение случайной точки начала проигрывания
        /// </summary>
        /// <returns></returns>
        private int DetermineRandomOfPlayingTime(int startPart, int endPart)
        {
            return _random.Next(startPart, endPart);
        }

        private enum Direction
        {
            Right = 1,
            Left = -1
        }

        /// <summary>
        /// Определить случайное направление относительно точки начала проигрывания
        /// </summary>
        /// <returns></returns>
        private Direction DeterminRandomDirection()
        {
            return _random.Next(0, 1) == 0 ? Direction.Left : Direction.Right;
        }

        /// <summary>
        /// Определить число частей нотного пространства доступное для заполнения в выбранном направлении
        /// </summary>
        /// <returns></returns>
        private int DetermineNumberOfAvailableParts(int startPart, int endPart, int startPointPlay,
            Direction direction)
        {
            if (direction == Direction.Left)
                return startPointPlay + 1;
            int sizeOfEmptySpace = startPart - endPart + 1;
            return sizeOfEmptySpace - startPointPlay;
        }


        /// <summary>
        /// Определить число частей нотного пространсва, которое будет заполнено фактически
        /// </summary>
        private int DetermineNumberOfPartsWillBeFilled(int startPart, int endPart, int startPointPlay,
            Direction direction,
            int maxNumberOfAvailableParts)
        {
            int numberOfAvailableParts = DetermineNumberOfAvailableParts(startPart, endPart, startPointPlay,
                direction);
            if (maxNumberOfAvailableParts < numberOfAvailableParts)
                return maxNumberOfAvailableParts;
            return numberOfAvailableParts;
        }

        /// <summary>
        /// Определить часть на которой закончится заполнение нотного пространства
        /// </summary>
        private int DefinePartOnWhichToEndFilling(int startPart, int endPart, int startPointPlay,
            Direction direction,
            int maxNumberOfAvailableParts)
        {
            int numberOfPartsWillBeFilled = DetermineNumberOfPartsWillBeFilled(startPart, endPart, startPointPlay,
                direction,
                maxNumberOfAvailableParts);
            if (direction == Direction.Left)
                return startPointPlay - numberOfPartsWillBeFilled;
            return startPointPlay + numberOfPartsWillBeFilled;
        }

        /// <summary>
        /// Заполнить нотное пространство в выбранном направлении
        /// </summary>
        private void FilledNoteSpaceinInTheSelectedDirection(int startPart, int endPart, int startPointPlay,
            Direction direction,
            int maxNumberOfAvailableParts)
        {
            int partOnWhichToEndFilling = DefinePartOnWhichToEndFilling(startPart, endPart, startPointPlay,
                direction,
                maxNumberOfAvailableParts);

            int iDirection = (int) direction;
            for (int i = startPointPlay; i != partOnWhichToEndFilling; i += iDirection)
            {
                _noteSpace.PartsOfTheSpaceNote[i] = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPart">Точка начала заполнения</param>
        /// <param name="endPart">Точка окончания заполнения</param>
        /// <param name="maxNumberOfAvailableParts">Максимальное число доступных частей нотного пространства</param>
        private void Filled(int startPart, int endPart, int maxNumberOfAvailableParts)
        {
            int startPointPlay = DetermineRandomOfPlayingTime(startPart, endPart);
            Direction direction = DeterminRandomDirection();
            FilledNoteSpaceinInTheSelectedDirection(startPart, endPart, startPointPlay, direction,
                maxNumberOfAvailableParts);
        }

        #endregion


        #region Пространство ноты пустое

        /// <summary>
        /// Заполнить нотное пространство случайным образом в случае, если нотное пространство пустое
        /// </summary>
        /// <param name="maxNumberOfAvailableParts">Максимальное число частей нотного пространство, которое можно заполнить</param>
        private void FilledEmptyNoteSpace(int maxNumberOfAvailableParts)
        {
            int startPart = 0;
            int endPart = _noteSpace.PartsOfTheSpaceNote.Length;
            Filled(startPart, endPart, maxNumberOfAvailableParts);
        }


        #endregion


        #region Пространство ноты частично заполненное





        private enum TypeOfSpace
        {
            Filled = 1,
            Unfilled = 0
        }

        /// <summary>
        /// Выбор наибольшего участока пустого нотного пространства
        /// либо наибольшего участока заполненного нотного пространства
        /// </summary>
        /// <param name="startPart">Начальная точка пустого|заполненного пространства </param>
        /// <param name="endPart">Конечная точка пустого|заполненного пространства</param>
        /// <param name="typeOfSpace">Тип нотного пространства</param>
        private void SelectionOfTheMaxSpace(out int startPart, out int endPart, TypeOfSpace typeOfSpace)
        {
            int sizeOfEmptySpaceTemp;
            int startPartTemp = -1;
            int endPartTemp;

            startPart = -1;
            endPart = -1;

            int sizeOfEmptySpace = 0;

            bool typeOFSpace = Convert.ToBoolean((int) typeOfSpace);

            if (_noteSpace.PartsOfTheSpaceNote[0] == !typeOFSpace)
            {
                startPartTemp = 0;
                startPart = 0;
            }


            for (int i = 1; i < _noteSpace.PartsOfTheSpaceNote.Length; i++)
            {
                if (_noteSpace.PartsOfTheSpaceNote[i - 1] == typeOFSpace &
                    _noteSpace.PartsOfTheSpaceNote[i] == !typeOFSpace)
                {
                    startPartTemp = i;
                    continue;
                }
                if (_noteSpace.PartsOfTheSpaceNote[i - 1] & _noteSpace.PartsOfTheSpaceNote[i] == typeOFSpace)
                {
                    endPartTemp = i - 1;
                    sizeOfEmptySpaceTemp = startPartTemp - endPartTemp + 1;
                    if (sizeOfEmptySpaceTemp > sizeOfEmptySpace)
                    {
                        startPart = startPartTemp;
                        endPart = endPartTemp;
                        sizeOfEmptySpace = sizeOfEmptySpaceTemp;
                    }
                }
            }
            if (startPart == -1 && endPart == -1)
                throw new Exception("В нотном пространстве отсутствуют пустые части");
        }



        /// <summary>
        /// Заполнить нотное пространство случайным образом в случае, если нотное пространство частично заполнено
        /// </summary>
        /// <param name="maxNumberOfAvailableParts">Максимальное число частей нотного пространство, которое можно заполнить</param>
        private void FilledPartiallyFilledNoteSpace(int maxNumberOfAvailableParts)
        {
            int startPart;
            int endPart;
            SelectionOfTheMaxSpace(out startPart, out endPart, TypeOfSpace.Unfilled);
            Filled(startPart, endPart, maxNumberOfAvailableParts);
        }


        #endregion

        /// <summary>
        /// Заполнить нотное пространство случайным образом
        /// </summary>
        /// <param name="maxNumberOfAvailableParts"></param>
        public void FilledNoteSpace(NoteSpace noteSpace, int maxNumberOfAvailableParts)
        {
            _noteSpace = noteSpace;
            if (_noteSpace.CheckingFilledSpace() == NoteSpaceStatus.Empty)
                FilledEmptyNoteSpace(maxNumberOfAvailableParts);
            else
                FilledPartiallyFilledNoteSpace(maxNumberOfAvailableParts);
        }


        #region Деление крупных частей заполненного нотного пространства на блоки


        /// <summary>
        /// Определить случайную точку разрыва
        /// </summary>
        private int DefinitionOfDividePoint(int startPart, int endPart)
        {
            return _random.Next(startPart + 1, endPart - 1);
        }

        private void DivideSpace(int dividePoint)
        {
            _noteSpace.Dividers[dividePoint] = true;
            if (DeterminRandomDirection() == Direction.Right)
                _noteSpace.Dividers[dividePoint + 1] = true;
            else
                _noteSpace.Dividers[dividePoint - 1] = true;
        }

        /// <summary>
        /// Разделить заполненное нотное пространство
        /// </summary>
        /// <remarks>
        /// Пример:
        /// file:///D:/%D0%A1_2013/Fractals/MusicGenerator/MusicGenerator/Builder/images/NoteSpaceBuilder/DivideTheFilledNoteSpace.png
        /// </remarks>
        public void DivideTheFilledNoteSpace()
        {
            //Нахождение наиболее заполненного участока нотного пространства
            int startPart;
            int endPart;
            SelectionOfTheMaxSpace(out startPart, out endPart, TypeOfSpace.Filled);

            if (startPart - endPart < 2)
                throw new Exception(
                    string.Format(
                        "Невозможно разбить заполненное нотное пространство на части. startPart = {0} endPart = {1}",
                        startPart, endPart));

            DivideSpace(DefinitionOfDividePoint(startPart, endPart));

        }

        #endregion

        #region Увеличить содержание за счет приращения заполненного нотного пространства

       

        bool IsPossibleToIncrement()
        {
            for (int i = 0; i < _noteSpace.PartsOfTheSpaceNote.Length; i++)
            {
                if (_noteSpace.PartsOfTheSpaceNote[i] == false)
                    return true;
            }
            return false;
        }


        int FindEmptyPart(int part1, int part2)
        {
            return _noteSpace.PartsOfTheSpaceNote[part1] == false ? part1 : part2;
        }

        /// <summary>
        /// Найти части за счет которых возможно прирастить заполненные блоки частей
        /// </summary>
        /// <returns></returns>
        List<int> FindCapableOfIncrementParts()
        {
            List<int> partsCapableOfIncrement = new List<int>();

            for (int i = 1; i < _noteSpace.PartsOfTheSpaceNote.Length; i++)
            {
                if (_noteSpace.PartsOfTheSpaceNote[i - 1] != _noteSpace.PartsOfTheSpaceNote[i])
                {
                    int emptyPart = FindEmptyPart(i - 1, i);
                    if(emptyPart>partsCapableOfIncrement.Last())
                        partsCapableOfIncrement.Add(emptyPart);
                }
            }
            return partsCapableOfIncrement;
        }

        /// <summary>
        /// Выбрать одну пустую часть из массива кандидатов на приращение
        /// </summary>
        int SelectOfRandomCapableOfIncrementPart(List<int> partsCapableOfIncrement)
        {
            return _random.Next(0, partsCapableOfIncrement.Count - 1);
        }


        /// <summary>
        /// Прирастить случайный блок заполненного нотного пространства на одну часть
        /// </summary>
        public void IncrementPerUnit()
        {
            //Проверка: возможно ли вообще совершить приращение
            if(!IsPossibleToIncrement())
                throw new Exception("Приращение совершить невозможно поскольку нет пустных частей нотного пространства.");

            int capableOfIncrementPart = SelectOfRandomCapableOfIncrementPart(FindCapableOfIncrementParts());
            
            _noteSpace.PartsOfTheSpaceNote[capableOfIncrementPart] = true;//"Прирастить"

            //В случае, если произошло объедиенение двух заполненных блоков частей нотного пространства в результате приращения
            //установить разделитель блоков

            if (_noteSpace.PartsOfTheSpaceNote[capableOfIncrementPart - 1] &
                _noteSpace.PartsOfTheSpaceNote[capableOfIncrementPart + 1])
                DivideSpace(capableOfIncrementPart);
        }


        #endregion
    }
}
