using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Common.Enums
{
    public enum InstAdErrors
    {
        [Description("Ойой, произошла неизвестная ошибка.")]
        UnknownError = 0,
        [Description("Пожалуйста, укажите тэги или локации во вкладках \"Рейтинг тэгов\", \"Поиск локаций\"")]
        NoTagsOrLocationsSpecified = 1,
        [Description("Операция была успешно отменена!!!")]
        OperationCancelled = 2
    }
}
