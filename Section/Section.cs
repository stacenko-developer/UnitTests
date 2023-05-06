using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnitTests
{
    /// <summary>
    /// Раздел.
    /// </summary>
    public class Section
    {
        #region Поля.
        /// <summary>
        /// Заголовок раздела.
        /// </summary> 
        private string _name;

        /// <summary>
        /// Код раздела (на английском языке).
        /// </summary>
        private string _code;

        /// <summary>
        /// Рабочее место, в котором будет располагаться раздел.
        /// </summary>
        private string _workspace;

        /// <summary>
        /// Поля раздела.
        /// </summary>
        private List<Field> _fields;

        /// <summary>
        /// Корректный префикс в кодах раздела и полей.
        /// </summary>
        private string _correctPrefix = "Nrb";

        /// <summary>
        /// Рабочее место по умолчанию.
        /// </summary>
        private static string DefaultWorkspace = "Рабочее место";

        /// <summary>
        /// Допустимые типы полей.
        /// </summary>
        private List<Type> _correctTypes = new List<Type>
        {
            typeof(int), typeof(double), typeof(DateTime), typeof(bool), typeof(string)
        };
        #endregion

        #region Свойства.
        /// <summary>
        /// Получение количества полей в разделе.
        /// </summary>
        public int FieldsCount => _fields.Count;

        /// <summary>
        /// Получение заголовка раздела.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Получение кода раздела.
        /// </summary>
        public string Code => _code;

        /// <summary>
        /// Получение названия рабочего места, в котором расположен раздел.
        /// </summary>
        public string Workspace => _workspace;

        /// <summary>
        /// Получение значения раздела по умолчанию.
        /// </summary>
        public string DefaultSectionName => "Название раздела по умолчанию";
        #endregion

        #region Конструкторы.
        /// <summary>
        /// Создает раздел с помощью указанных данных.
        /// </summary>
        /// <param name="name">Заголовок раздела.</param>
        /// <param name="code">Код раздела.</param>
        /// <exception cref="ArgumentNullException">Текст не содержит символов 
        /// или равен null.</exception>
        /// <exception cref="FormatException">Некорректный формат введенных данных.</exception>
        public Section(string name, string code)
            : this(name, code, DefaultWorkspace)
        {
        }

        /// <summary>
        /// Создает раздел с помощью указанных данных.
        /// </summary>
        /// <param name="name">Заголовок раздела.</param>
        /// <param name="code">Код раздела.</param>
        /// <param name="workspace">Рабочее место.</param>
        /// <exception cref="ArgumentNullException">Текст не содержит символов 
        /// или равен null.</exception>
        /// <exception cref="FormatException">Некорректный формат введенных данных.</exception>
        public Section(string name, string code, string workspace)
        {
            ValidateName(name);
            ValidateCode(code);
            ValidateName(workspace);

            _name = name;
            _code = code;
            _workspace = workspace;
            _fields = new List<Field>();
        }
        #endregion

        #region Методы.

        #region Валидация.
        /// <summary>
        /// Проверка наличия кода поля в разделе.
        /// </summary>
        /// <param name="code">Код поля, наличие которого необходимо проверить.</param>
        /// <returns>Результат проверки.</returns>
        public virtual bool IsFieldExistence(string code) => _fields
            .FirstOrDefault(field => field.Code == code) != null;

        /// <summary>
        /// Проверка корректности заголовка.
        /// </summary>
        /// <param name="name">Заголовок, корректность которого необходимо проверить.</param>
        /// <exception cref="FormatException">Заголовок не соответствует регламенту!</exception>
        private void ValidateName(string name)
        {
            Validator.ValidateStringText(name);

            if (!Regex.IsMatch(name, "^[а-яА-Я ]+$") || !char.IsUpper(name[0]))
            {
                throw new FormatException("Заголовок не соответствует регламенту!");
            }
        }

        /// <summary>
        /// Проверка корректности кода.
        /// </summary>
        /// <param name="code">Код, корректность которого необходимо проверить.</param>
        /// <exception cref="FormatException">Код не соответствует регламенту!</exception>
        private void ValidateCode(string code)
        {
            Validator.ValidateStringText(code);

            if (!Regex.IsMatch(code, "^[a-zA-Z ]+$")
                || !code.StartsWith(_correctPrefix))
            {
                throw new FormatException("Код не соответствует регламенту!");
            }
        }

        /// <summary>
        /// Проверка свободности кода поля.
        /// </summary>
        /// <param name="code">Код, свободность которого необходимо проверить.</param>
        /// <exception cref="ArgumentOutOfRangeException">Указанный код уже занят!</exception>
        private void ValidateFieldCodeNotExistence(string code)
        {
            ValidateCode(code);

            if (IsFieldExistence(code))
            {
                throw new ArgumentOutOfRangeException(nameof(code), "Указанный код поля уже занят!");
            }
        }

        /// <summary>
        /// Проверка существования поля по коду.
        /// </summary>
        /// <param name="code">Код поля, существование которого необходимо проверить.</param>
        /// <exception cref="ArgumentNullException">Указанный код не найден!</exception>
        private void ValidateFieldCodeExistence(string code)
        {
            ValidateCode(code);

            if (!IsFieldExistence(code))
            {
                throw new ArgumentNullException(nameof(code), "Указанный код не найден!");
            }
        }

        /// <summary>
        /// Проверка наличия типа в списке допустимых.
        /// </summary>
        /// <param name="type">Тип, который необходимо проверить.</param>
        /// <exception cref="ArgumentNullException">Значение типа равно null!</exception>
        /// <exception cref="ArgumentOutOfRangeException">Указанный тип не входит в список допустимых!</exception>
        private void ValidateType(Type type)
        {
            Validator.ValidateType(type);

            if (!_correctTypes.Contains(type))
            {
                throw new ArgumentOutOfRangeException(nameof(type),
                    "Указанный тип не входит в список допустимых!");
            }
        }

        /// <summary>
        /// Проверка корректности значения по ее типу.
        /// </summary>
        /// <param name="type">Тип значения.</param>
        /// <param name="value">Значение, которое необходимо проверить.</param>
        /// <exception cref="FormatException">Некорректное значение поля!</exception>
        private void ValidateValueByType(Type type, string value)
        {
            Validator.ValidateStringText(value);
            ValidateType(type);

            if (type == typeof(int) && !int.TryParse(value, out _))
            {
                throw new FormatException("Значение поля не соответствует " +
                    "целочисленному типу данных!");
            }

            if (type == typeof(double) && !double.TryParse(value, out _))
            {
                throw new FormatException("Значение поля не соответствует " +
                    "вещественному типу данных!");
            }

            if (type == typeof(DateTime) && !DateTime.TryParse(value, out _))
            {
                throw new FormatException("Значение поля не соответствует " +
                    "типу данных даты!");
            }

            if (type == typeof(bool) && !bool.TryParse(value, out _))
            {
                throw new FormatException("Значение поля не соответствует " +
                    "логическому типу данных!");
            }
        }
        #endregion

        /// <summary>
        /// Получение поля по его коду.
        /// </summary>
        /// <param name="code">Код поля.</param>
        /// <returns>Поле.</returns>
        public virtual Field GetField(string code)
        {
            ValidateFieldCodeExistence(code);

            return _fields.FirstOrDefault(field => field.Code == code);
        }

        /// <summary>
        /// Добавляет поле.
        /// </summary>
        /// <param name="name">Заголовок поля.</param>
        /// <param name="code">Код поля.</param>
        /// <param name="type">Тип поля.</param>
        /// <param name="isRequiredFlag">Обязательность поля.</param>
        /// <param name="value">Значение поля.</param>
        public void AddField(string name, string code, Type type,
            bool isRequiredFlag, string value)
        {
            ValidateName(name);
            ValidateFieldCodeNotExistence(code);
            ValidateValueByType(type, value);

            _fields.Add(new Field
            {
                Name = name,
                Code = code,
                Type = type,
                IsRequired = isRequiredFlag,
                Value = value
            });
        }

        /// <summary>
        /// Удаление поля по его коду.
        /// </summary>
        /// <param name="code">Код поля, которое необходимо удалить.</param>
        /// <exception cref="ArgumentNullException">Поле с указанным кодом не найдено!</exception>
        public void DeleteField(string code)
        {
            Validator.ValidateStringText(code);

            if (!IsFieldExistence(code))
            {
                throw new ArgumentNullException(nameof(code), "Поле с указанным кодом не найдено!");
            }

            _fields.Remove(GetField(code));
        }

        /// <summary>
        /// Обновление данных поля по его коду.
        /// </summary>
        /// <param name="code">Код поля, данные которого необходимо обновить.</param>
        /// <param name="newCode">Новый код поля.</param>
        /// <param name="newName">Новый заголовок поля.</param>
        /// <param name="newType">Новый тип поля.</param>
        /// <param name="newIsRequiredFlag">Новый флаг обязательности заполнения поля.</param>
        /// <param name="newValue">Новое значение поля.</param>
        public void UpdateField(string code, string newCode, string newName, Type newType,
            bool newIsRequiredFlag, string newValue)
        {
            ValidateFieldCodeExistence(code);
            ValidateFieldCodeNotExistence(newCode);
            ValidateName(newName);
            ValidateValueByType(newType, newValue);

            var entity = GetField(code);

            entity.Code = newCode;
            entity.Name = newName;
            entity.Type = newType;
            entity.IsRequired = newIsRequiredFlag;
            entity.Value = newValue;
        }

        /// <summary>
        /// Изменение значение поля.
        /// </summary>
        /// <param name="code">Код поля, значение в котором необходимо изменить.</param>
        /// <param name="newValue">Новое значение поля.</param>
        public void ChangeValueInField(string code, string newValue)
        {
            ValidateFieldCodeExistence(code);
            ValidateValueByType(_fields.First(field => field.Code == code).Type,
                newValue);

            var entity = GetField(code);

            entity.Value = newValue;
        }

        /// <summary>
        /// Получение значения поля по коду.
        /// </summary>
        /// <param name="code">Код поля, значение которого необходимо получить.</param>
        /// <returns>Значение поля.</returns>
        public string GetValueInField(string code)
        {
            ValidateFieldCodeExistence(code);

            return _fields.First(field => field.Code == code).Value;
        }

        /// <summary>
        /// Очистка раздела.
        /// </summary>
        public void ClearSection()
        {
            _fields.Clear();
        }
        #endregion
    }
}