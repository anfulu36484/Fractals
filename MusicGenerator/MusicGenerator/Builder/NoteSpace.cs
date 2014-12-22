using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicGenerator.Builder
{

    public class NoteSpace
    {
        /// <summary>
        ///Нотное пространство
        ///Значение false - пустая часть нотного пространства
        /// true - заполненная часть
        /// </summary>
        private bool[] _partsOfTheSpaceNote;

        /// <summary>
        /// Нотное пространство
        ///Значение false - пустая часть нотного пространства
        /// true - заполненная часть
        /// </summary>
        public bool[] PartsOfTheSpaceNote
        {
            get { return _partsOfTheSpaceNote; }
        }

        private Random _random;

        public NoteSpace(UInt16 playingTime, Random random)
        {
            //Создаем пустое нотное пространство
            _partsOfTheSpaceNote = new bool[playingTime];

            //Создаем массив разделителей нотного пространства
            _dividers = new bool[playingTime];
            _random = random;
        }

        /// <summary>
        /// Проверка заполненности нотного пространства
        /// </summary>
        /// <returns></returns>
        public NoteSpaceStatus CheckingFilledSpace()
        {
            for (int i = 0; i < _partsOfTheSpaceNote.Length; i++)
            {
                if (_partsOfTheSpaceNote[i])
                    return NoteSpaceStatus.Filled;
            }
            return NoteSpaceStatus.Empty;
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
                _partsOfTheSpaceNote[i] = true;
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
            int endPart = _partsOfTheSpaceNote.Length;
            Filled(startPart, endPart, maxNumberOfAvailableParts);
        }


        #endregion


        #region Пространство ноты частично заполненное

        /// <summary>
        /// Выбор максимального пустого пространства между заполненными часями нотного пространства
        /// </summary>
        /// <param name="startPart">Начальная точка пустого пространства</param>
        /// <param name="endPart">Конечная точка пустого пространства</param>
        private void SelectionOfTheMaxSpaceBetweenFilledPartOfSpace(out int startPart, out int endPart)
        {
            int sizeOfEmptySpaceTemp;
            int startPartTemp = -1;
            int endPartTemp;

            startPart = -1;
            endPart = -1;

            int sizeOfEmptySpace = 0;


            if (_partsOfTheSpaceNote[0])
            {
                startPartTemp = 0;
                startPart = 0;
            }


            for (int i = 1; i < _partsOfTheSpaceNote.Length; i++)
            {
                if (_partsOfTheSpaceNote[i - 1] == false & _partsOfTheSpaceNote[i])
                {
                    startPartTemp = i;
                    continue;
                }
                if (_partsOfTheSpaceNote[i - 1] & _partsOfTheSpaceNote[i] == false)
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
            SelectionOfTheMaxSpaceBetweenFilledPartOfSpace(out startPart, out endPart);
            Filled(startPart, endPart, maxNumberOfAvailableParts);
        }


        #endregion

        /// <summary>
        /// Заполнить нотное пространство случайным образом
        /// </summary>
        /// <param name="maxNumberOfAvailableParts"></param>
        public void FilledNoteSpace(int maxNumberOfAvailableParts)
        {
            if (CheckingFilledSpace() == NoteSpaceStatus.Empty)
                FilledEmptyNoteSpace(maxNumberOfAvailableParts);
            else
                FilledPartiallyFilledNoteSpace(maxNumberOfAvailableParts);
        }


        //Увеличить содержание за счет приращения заполненного нотного пространства 




        #region Деление крупных частей заполненного нотного пространства на блоки

        /// <summary>
        ///Массив разделителей заполненного нотного пространства
        ///Значение _dividers[i] = false указывает на отсутствие разделителя между частями пространства
        /// _dividers[i] = true указывает на присутсвие разделителя между _partsOfTheSpaceNote[i] и _partsOfTheSpaceNote[i+1]
        /// </summary>
        private bool[] _dividers;

        /// <summary>
        ///Массив разделителей заполненного нотного пространства
        ///Значение _dividers[i] = false указывает на отсутствие разделителя между частями пространства
        /// _dividers[i] = true указывает на присутсвие разделителя между _partsOfTheSpaceNote[i] и _partsOfTheSpaceNote[i+1]
        /// </summary>
        public bool[] Dividers
        {
            get { return _dividers; } 
        }


        /// <summary>
        /// Найти наиболее большой участок заполненного нотного пространства
        /// </summary>
        /// <param name="startPlay">Начальная точка проигрывания наибольшего участка</param>
        /// <param name="endPlay">Окончание проигрывания наибольшего участка</param>
        /*void FindTheBiggestSiteOfFilledNoteSpace(out int startPlay, out int endPlay)
        {
                
        }*/

        public void DivideTheFilledMusicalSpace()
        {
                
        }

        #endregion
    }
}
