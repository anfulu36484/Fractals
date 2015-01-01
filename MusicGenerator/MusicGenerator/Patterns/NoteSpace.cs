using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicGenerator.Builder;

namespace MusicGenerator.Patterns
{
    class NoteSpace
    {
        /// <summary>
        /// Нотное пространство
        ///Значение false - пустая часть нотного пространства
        /// true - заполненная часть
        /// </summary>
        public bool[] PartsOfTheSpaceNote { get; set; }

        /// <summary>
        ///Массив разделителей заполненного нотного пространства
        ///Значение _dividers[i] = false указывает на отсутствие разделителя между частями пространства
        /// _dividers[i] = true указывает на присутсвие разделителя между _partsOfTheSpaceNote[i] и _partsOfTheSpaceNote[i+1]
        /// </summary>
        public bool[] Dividers { get; set; }


        public NoteSpace(UInt16 playingTime)
        {
            //Создаем пустое нотное пространство
            PartsOfTheSpaceNote = new bool[playingTime];

            //Создаем массив разделителей нотного пространства
            Dividers = new bool[playingTime];
        }

        /// <summary>
        /// Проверка заполненности нотного пространства
        /// </summary>
        /// <returns></returns>
        public NoteSpaceStatus CheckingFilledSpace()
        {
            for (int i = 0; i < PartsOfTheSpaceNote.Length; i++)
            {
                if (PartsOfTheSpaceNote[i])
                    return NoteSpaceStatus.Filled;
            }
            return NoteSpaceStatus.Empty;
        }
    }
}
